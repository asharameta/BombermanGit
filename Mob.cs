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
        Point destinePlace;
        Point MobPlace;
        MovingClass moving;
        int step=3;

        public Mob(PictureBox picture, PictureBox[,] picM, Sost[,] _map) // конструктор 
        {
            mob = picture;
            moving = new MovingClass(picture, picM, _map);
            MobPlace = moving.MyNowPoint();
            this.mob = picture;
            destinePlace = new Point(15, 7);
            CreateTimer();
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
            if (MobPlace == destinePlace) return;
            MoveMob(destinePlace);
        }
        private void MoveMob(Point newPlace)
        {
            int sx = 0, sy = 0;
            if (MobPlace.X < newPlace.X) sx = newPlace.X - MobPlace.X > step ? step : newPlace.X = MobPlace.X;
            else sx = MobPlace.X - newPlace.X < step ? newPlace.X - MobPlace.X : -step;
            if (MobPlace.Y < newPlace.Y) sy = newPlace.Y - MobPlace.Y > step ? step : newPlace.Y = MobPlace.Y;
            else sy = MobPlace.Y - newPlace.Y < step ? newPlace.Y - MobPlace.Y : -step;
            moving.Move(sx, sy);

            MobPlace = moving.MyNowPoint();

        }
    }
}
