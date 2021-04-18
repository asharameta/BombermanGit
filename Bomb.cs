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
        Timer timer;
        int NumberOfSec = 4;
        PictureBox[,] mapPic;
        Point bombPlace;
        public Bomb(PictureBox[,]_mapPic, Point _bombPlace)
        {
            mapPic = _mapPic;
            bombPlace = _bombPlace;
            CreateTimer();
            timer.Enabled = true;
        }

        private void CreateTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_tick;
        }
        void timer_tick(object sender, System.EventArgs e)
        {
            WriteTimer(--NumberOfSec);
            //throw new System.NotImplementedException();
        }
        private void WriteTimer(int num)
        {
            mapPic[bombPlace.X, bombPlace.Y].Image = Properties.Resources.bomb;
            mapPic[bombPlace.X, bombPlace.Y].Refresh();
            using (Graphics graph=mapPic[bombPlace.X, bombPlace.Y].CreateGraphics())
            {
                PointF point = new PointF(mapPic[bombPlace.X, bombPlace.Y].Size.Width / 2, mapPic[bombPlace.X, bombPlace.Y].Size.Height / 3+5);
                graph.DrawString(num.ToString(), new Font("Arial", 10), Brushes.White, point);
            }
        }
    }


}
