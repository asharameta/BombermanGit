using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomberman
{
    class Bomb
    {
        Timer timer;  // таймер бомбы 
        int NumberOfSec = 4; //таймер бомбы, время взрыва 
        PictureBox[,] mapPic; // картинки цифр 
        Point bombPlace; // разположение бомбы
        public Bomb(PictureBox[,]_mapPic, Point _bombPlace)
        {
            mapPic = _mapPic; // картинки цифр 
            bombPlace = _bombPlace;// распложение бомб от игрока
            CreateTimer(); // запуск таймер
            timer.Enabled = true;// вкл таймера
        }

        private void CreateTimer() // создание таймера
        {
            timer = new Timer(); // таймер 
            timer.Interval = 1000; // 1000 - 1 с интервела. тобишь каждую секунду 
            timer.Tick += timer_tick;  // счещик таймера 
        }
        void timer_tick(object sender, System.EventArgs e)
        {
            WriteTimer(--NumberOfSec); // рисует цифры на бомбе 
            //throw new System.NotImplementedException();
        }
        private void WriteTimer(int num) // показывает время на бомбе 
        {
  //          mapPic[bombPlace.X, bombPlace.Y].Image = Properties.Resources.bomb; // присвоение новой картинки, что бы числа не рисовались друг на друге 
            mapPic[bombPlace.X, bombPlace.Y].Refresh();// перерисовывает себя. обновляеться 
            using (Graphics graph=mapPic[bombPlace.X, bombPlace.Y].CreateGraphics()) // соз график . (создаеться толька здесь и удаляеться) 
            {
                PointF point = new PointF(mapPic[bombPlace.X, bombPlace.Y].Size.Width / 2, mapPic[bombPlace.X, bombPlace.Y].Size.Height / 3+5);// кординаты и размер где рисовать таймер
                graph.DrawString(num.ToString(), new Font("Arial", 10), Brushes.Red, point);//рисование таймера (что рисуем, стиль шрифта, цвет , где)
            }
        }
    }


}
