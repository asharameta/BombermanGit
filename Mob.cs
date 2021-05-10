using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomberman
{
    /// <summary>
    /// 1. уровень сложности - выберает доступную точку и бежит к ней 
    /// 2. уровень сложности - выберает доступную точку и бежит к ней, если видит бомку или огонь - убегает
    /// 3. уровень сложности - бегает от точки к точку, если доступен человек бежит к нему, если встретил бомбу убегает
    /// </summary> 

    class Mob
    {
        int level = 1; // уровень сложный 
       public PictureBox mob { get; private set; } // создание картинки моба 
        Timer timer; // таймер отвечает за движение моба 
        Point destinePlace; // поинт точка до которой двигается моб 
        Point MobPlace;  // поинт что бы добрался враг 
        MovingClass moving; // класс движение 
        int step=5; // шаг врага
        Sost[,] map; // состоянние карты
        int[,] fmap; //заполняеться числами что бы враг знал куда идти  
        int paths;// длина пути 
        Point[] path;// путь 
        int pathStep; // текущий шаг 
        static Random rand = new Random(); // рандомное значение для нового пути куда должен идти
        Player player; // igrok 

        public Mob(PictureBox picture, PictureBox[,] picM, Sost[,] _map, Player _player) // конструктор 
        {
            mob = picture;// картинка врага
            map = _map; // карта
            player = _player;
            fmap = new int[map.GetLength(0), map.GetLength(1)]; // длина карты
            path = new Point[map.GetLength(0)* map.GetLength(1)]; //максимальный путь
            moving = new MovingClass(picture, picM, _map, AddBonus); // класс движение 
            MobPlace = moving.MyNowPoint(); // получаем поинты врага 
            destinePlace = MobPlace; // кардинаты движение моба , куда должен идти моб
            CreateTimer(); // запуск таймера для движение игрока 
            timer.Enabled = true;  // вкл таймера 
        }

        private void CreateTimer() // создание таймера для движение врага 
        {
            timer = new Timer(); // создание таймера
            timer.Interval = 50; // х с . через заданное время двигается враг                           скорость игрока
            timer.Tick += Timer_Tick; // счетчик 
        }

        private void Timer_Tick(object sender, EventArgs e) // движене врага 
        {
            if (MobPlace == destinePlace) GetNewPlace(); // дошел до точки которая указан , новый маршрут строим
            if (path[0].X == 0 && path[0].Y == 0) // если такого пути нету 
                if (!FindPath()) return; // нету пути
            if (pathStep > paths) return; // дошли до пути 
            if (path[pathStep] == MobPlace) // дошли до пути двигаем шаг слудующий 
                pathStep++;
            else
                MoveMob(path[pathStep]); // движение до указаной точки 
        }
        private void MoveMob(Point newPlace) // передвижение врага 
        {
            int sx = 0, sy = 0;
            if (MobPlace.X < newPlace.X) // плавное передвижение игрока       ------------------------------------- равно было мобплейс
                sx = newPlace.X - MobPlace.X > step ? step : newPlace.X - MobPlace.X;
            else
                sx = MobPlace.X - newPlace.X < step ? newPlace.X - MobPlace.X : -step;
            if (MobPlace.Y < newPlace.Y)
                sy = newPlace.Y - MobPlace.Y > step ? step : newPlace.Y - MobPlace.Y;
            else
                sy = MobPlace.Y - newPlace.Y < step ? newPlace.Y - MobPlace.Y : -step;
            moving.Move(sx*step, sy*step);

            MobPlace = moving.MyNowPoint();

            if (level >= 2 && map[newPlace.X, newPlace.Y] == Sost.bomb || map[newPlace.X, newPlace.Y] == Sost.fire) //если уровень стал другой то мобы стали умнее
                GetNewPlace(); // если бомба и огонь ищет новый путь. 

        }

        // суть пойска пути, заполняем карту 0 и выстраеваем путь изменяе 0 nr++; пока не найдем наш путь 
        private bool FindPath() // поиск пути врага 
        {
            for (int x = 0; x < map.GetLength(0); x++) // получаем длинну карты X,Y для пойска маршрута 
                for(int y=0;y<map.GetLength(1);y++)
                    fmap[x, y] = 0;
            bool added;
            bool found = false; // найден ли наш путь
            fmap[MobPlace.X, MobPlace.Y] = 1; // первый путь врага с 1 
            int nr = 1; // путь врага будет рассти куда идти 
            do
            {
                added = false;
                for (int x = 0; x < map.GetLength(0); x++) // перебор карты 
                    for (int y = 0; y < map.GetLength(1); y++)
                        if(fmap[x,y]==nr) // споимали наш номер. увеличиваем значение 
                        {
                            MarkPath(x + 1, y, nr + 1);
                            MarkPath(x - 1, y, nr + 1);
                            MarkPath(x, y - 1, nr + 1);
                            MarkPath(x, y + 1, nr + 1);
                            added = true;
                        }
                if(fmap[destinePlace.X, destinePlace.Y]>0) // дошли до конца 
                {
                    found = true;
                    break;
                }
                nr++;
            } while (added); 
            if (!found) return false; // не нашли путь 
            // нашли путь обознаем его
            int sx = destinePlace.X; // длина х 
            int sy = destinePlace.Y; // длина 
            paths = nr; // длина пути 
            while(nr>=0)
            {
                path[nr].X = sx;
                path[nr].Y = sy;
                if (IsPath(sx + 1, sy, nr)) sx++;
                else if (IsPath(sx - 1, sy, nr)) sx--;
                else if (IsPath(sx, sy + 1, nr)) sy++;
                else if (IsPath(sx, sy - 1, nr)) sy--;
                nr--;
            }
            pathStep = 0;
            return true;
        }
        private void MarkPath(int x, int y, int n) // можно идти или нет 
        {
            if (x < 0 || x >= map.GetLength(0)) return; // если значение ушло за карту 
            if (y < 0 || y >= map.GetLength(1)) return;
            if (fmap[x, y] > 0) return; // что бы не ходил встороны и по проиденному пути 1,2,3,4 и т.д а нет 1,1,1,1,1 
            if (map[x, y] != Sost.empty) return;  // если не пусто выходим 
            fmap[x, y] = n; //отмечаем что можно идти дальше 
        }
        private bool IsPath(int x, int y, int n)
        {
            if (x < 0 || x >= map.GetLength(0)) return false; // ушел за карту, на всякий случий 
            if (y < 0 || y >= map.GetLength(1)) return false;
            return fmap[x,y]==n;
        }
        private void GetNewPlace() // создание нового пути, маршрута, к
        {
            if(level >= 3) // уровень 3 бежит к игроку 
            {
                destinePlace = player.MyNowPoint(); // если путь равен с игроком то возращаем этот путь 
                if (FindPath()) return; 
            }
            int loop = 0;
            do
            {   // рандомные значение куда должен идти враг 
                destinePlace.X = rand.Next(1, map.GetLength(0) - 1);
                destinePlace.Y = rand.Next(1, map.GetLength(1) - 1);
            } while (!FindPath() && loop++<300); // наден путь или нет, и второе страховка шоб выйти из цикла
            if (loop >= 300) destinePlace = MobPlace; // якобы нашел путь .... 
        }
        public Point MyNowPoint() // расспложение врага 
        {
            return moving.MyNowPoint(); 
        }
        public void SetLevel(int _level) // меняет уровень игрока
        {
            level = _level; 
        }
        private void AddBonus(Prize p) { }
    }
}
