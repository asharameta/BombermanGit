using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomberman
{
    class Mob
    {
        PictureBox mob; // создание картинки моба 
        Timer timer; // таймер отвечает за движение моба 
        Point destinePlace; // поинт точка до которой двигается моб 
        Point MobPlace;  // поинт что бы добрался враг 
        MovingClass moving; // класс движение 
        int step=3; // шаг врага
        Sost[,] map;
        int[,] fmap;
        int paths;
        Point[] path;
        int pathStep;
        static Random rand = new Random();
        public Mob(PictureBox picture, PictureBox[,] picM, Sost[,] _map) // конструктор 
        {
            mob = picture;
            map = _map;
            fmap = new int[map.GetLength(0), map.GetLength(1)];
            path = new Point[map.GetLength(0)* map.GetLength(1)];
            moving = new MovingClass(picture, picM, _map); // класс движение 
            MobPlace = moving.MyNowPoint(); // получаем поинты врага 
            this.mob = picture;
            destinePlace = MobPlace; // кардинаты движение моба 
            CreateTimer(); // запуск таймера для движение игрока 
            timer.Enabled = true;  // вкл таймера 
        }

        private void CreateTimer() // создание таймера для движение врага 
        {
            timer = new Timer(); // создание таймера
            timer.Interval = 100; // 1 с . через заданное время двигается врак  
            timer.Tick += Timer_Tick; // счетчик 
        }

        private void Timer_Tick(object sender, EventArgs e) // движене врага 
        {
            if (MobPlace == destinePlace) GetNewPlace(); // дошел до точки которая указан 
            if (path[0].X == 0 & path[0].Y == 0)
                if (!FindPath()) return;
            if (pathStep > paths) return;
            if (path[pathStep] == MobPlace)
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
            moving.Move(sx, sy);

            MobPlace = moving.MyNowPoint();

        }

        private bool FindPath()
        {
            for (int x = 0; x < map.GetLength(0); x++)
                for(int y=0;y<map.GetLength(1);y++)
                    fmap[x, y] = 0;
            bool added;
            bool found = false;
            fmap[MobPlace.X, MobPlace.Y] = 1;
            int nr = 1;
            do
            {
                added = false;
                for (int x = 0; x < map.GetLength(0); x++)
                    for (int y = 0; y < map.GetLength(1); y++)
                        if(fmap[x,y]==nr)
                        {
                            MarkPath(x + 1, y, nr + 1);
                            MarkPath(x - 1, y, nr + 1);
                            MarkPath(x, y - 1, nr + 1);
                            MarkPath(x, y + 1, nr + 1);
                            added = true;
                        }
                if(fmap[destinePlace.X, destinePlace.Y]>0)
                {
                    found = true;
                    break;
                }
                nr++;
            } while (added);
            if (!found) return false;
            int sx = destinePlace.X;
            int sy = destinePlace.Y;
            paths = nr;
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
        private void MarkPath(int x, int y, int n)
        {
            if (x < 0 || x >= map.GetLength(0)) return;
            if (y < 0 || y >= map.GetLength(1)) return;
            if (fmap[x, y] > 0) return;
            if (map[x, y] != Sost.пусто) return;
            fmap[x, y] = n;
        }
        private bool IsPath(int x, int y, int n)
        {
            if (x < 0 || x >= map.GetLength(0)) return false;
            if (y < 0 || y >= map.GetLength(1)) return false;
            return fmap[x,y]==n;
        }
        private void GetNewPlace()
        {
            int loop = 0;
            do
            {
                destinePlace.X = rand.Next(1, map.GetLength(0) - 1);
                destinePlace.Y = rand.Next(1, map.GetLength(1) - 1);
            } while (!FindPath() && loop++<100);
            if (loop >= 100) destinePlace = MobPlace;
        }
    }
}
