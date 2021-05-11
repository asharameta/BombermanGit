using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace bomberman
{
 public class Bomb
    {
        Timer timer;  // таймер бомбы 
        int NumberOfSec = 4; //таймер бомбы, время взрыва 
        PictureBox[,] mapPic; // картинки цифр 
        System.Media.SoundPlayer blowsound;
        public Point bombPlace { get; private set;  } // разположение бомбы .  (получаем место положение бомбы из других функций, но изменять не можем) 
        deBlow baBah; // взрыв 
        public Bomb(PictureBox[,]_mapPic, Point _bombPlace, deBlow bb)
        {
            blowsound = new System.Media.SoundPlayer();
            mapPic = _mapPic; // картинки цифр 
            bombPlace = _bombPlace;// распложение бомб от игрока
            baBah = bb;  //взрыв ссылка на метод 
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
            if(NumberOfSec <= 0) // кончилось время бомбы 
            {
                timer.Enabled = false; // выключаем таймер 
                blowsound.SoundLocation = @"bomb.wav";
                blowsound.Play();
                baBah(this); // взрыв 
                return; 
            }
            WriteTimer(--NumberOfSec); // рисует цифры на бомбе 
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
        public void Reaction() // мгновенно зрывает бомбу, таймер = 0, сделанно если возрвали бомба об бомбу 
        {
            NumberOfSec = 0; 
        }

    }
}
