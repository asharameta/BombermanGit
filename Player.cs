using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomberman
{
    public delegate void deAddBonus(Prize p); 
    enum Arrows   //стрелки управление
    {
        left,
        right,
        up,
        down
    }
    class Player  // класс игрока 
    {
        PictureBox player;
        PictureBox[,] mapPic;  // для нахождение игрока по боксам картинак 
        Sost[,] map; // состояние карты что бы враг не ходил на блоки 
        int step; // длина шага
        MovingClass moving; // класс для движение игрока, содержит функций движение игрока
        public List<Bomb> bombs { get; private set; } // массив бомб
        int NumOfBomb; // количество бомб
        public int leftFire { get; private set; } // сделали публичную переменную, НО получить можно, но нельзя изменить (сила огня) 
        Label score;  // статистика (на панели) 

        public Player(PictureBox _player, PictureBox[,] _mapPic, Sost[,] _map, Label lbScore) // конструктор 
        {
            player = _player;
            step = 5;
            NumOfBomb = 3;
            leftFire = 3;
            score = lbScore;
            bombs = new List<Bomb>();
            moving = new MovingClass(_player, _mapPic, _map, AddBonus); // создали движение игрока 
            ChageScore();
        }

        public void MovePlayer(Arrows arrow) // движение игрока 
        {
            switch (arrow) // передвижение игрока (картинки его) спомощью функиця мув из класса МувингКласс
            {
                case Arrows.left:
                    player.Image = Properties.Resources.playerLeft; // смена картинки игрока бежит лево
                    moving.Move(-step, 0);
                    break;
                case Arrows.right:
                    player.Image = Properties.Resources.playerRight;
                   moving.Move(step, 0);
                    break;
                case Arrows.up:
                    player.Image = Properties.Resources.playerUp;
                   moving.Move(0, -step);
                    break;
                case Arrows.down:
                    player.Image = Properties.Resources.playerDown;
                   moving.Move(0, step);
                    break;
                default:
                    break;
            }
        }
        public Point MyNowPoint() // позиция игрока, из класса мовинг 
        {
            return moving.MyNowPoint();
        }
        public bool PutBomb(PictureBox[,] mapPic, deBlow bb)// можно ли ставить бомбу или нет 
        {
            if (bombs.Count() >= NumOfBomb) return false; // нету бомб

            Bomb bomb = new Bomb(mapPic, MyNowPoint(), bb); // создание бомбы 
            player.Image = Properties.Resources.player;
           bombs.Add(bomb); // добаваление бомбы 
            return true;
        }
        public void RemoveBomb(Bomb bomb) // удаление бомб 
        {
            bombs.Remove(bomb);
        }
        private void ChageScore(string bonus="") //статистика (на панели) 
        {
            if (score == null) return;
            score.Text = "Speed: " + step + ", fire: " + leftFire +"Action bonus: " + bonus; 
        }

        private void AddBonus(Prize prize) // добавление бонусов 
        {
            switch (prize)
            {
                case Prize.empty:
                   // leftFire++; 
                    break;
                case Prize.firePlus:
                    leftFire++;
                    ChageScore(" +1 fire");
                    break;
                case Prize.fireMinus:
                    leftFire = leftFire == 1 ? 1 : leftFire--;
                    ChageScore (" -1 fire");
                    break;
                case Prize.speedPlus:
                    step ++;
                    ChageScore(" +1 speed");
                    break;
                case Prize.speedMinus:
                    step = step <= 3 ? 3 : step --;
                    ChageScore(" -1 speed");
                    break;
                default:
                    break;
            }
          
        }
    }
}
