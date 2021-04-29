using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomberman
{
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
        public Player(PictureBox _player, PictureBox[,] _mapPic, Sost[,] _map) // конструктор 
        {
            player = _player;
            step = 5;
            NumOfBomb = 3;
            leftFire = 3;
            bombs = new List<Bomb>();
            moving = new MovingClass(_player, _mapPic, _map); // создали движение игрока 
        }

        public void MovePlayer(Arrows arrow) // движение игрока 
        {
            switch (arrow) // передвижение игрока (картинки его) спомощью функиця мув из класса МувингКласс
            {
                case Arrows.left:
                    moving.Move(-step, 0);
                    break;
                case Arrows.right:
                    moving.Move(step, 0);
                    break;
                case Arrows.up:
                    moving.Move(0, -step);
                    break;
                case Arrows.down:
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
            bombs.Add(bomb); // добаваление бомбы 
            return true;
        }
        public void RemoveBomb(Bomb bomb) // удаление бомб 
        {
            bombs.Remove(bomb);
        }
    }
}
