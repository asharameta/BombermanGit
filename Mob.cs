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

        public Mob(PictureBox picture, PictureBox[,] picM, Sost[,] _map) // конструктор 
        {
            mob = picture;
            moving = new MovingClass(picture, picM, _map); // класс движение 
            MobPlace = moving.MyNowPoint(); // получаем поинты врага 
            this.mob = picture;
            destinePlace = new Point(15, 7);  // кардинаты движение моба 
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
            if (MobPlace == destinePlace) return; // дошел до точки которая указан 
            MoveMob(destinePlace); // движение до указаной точки 
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
    }
}
