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
        int step = 5; // длина шага
        public Player(PictureBox _player, PictureBox[,]_mapPic)
        {
            player = _player;
            mapPic = _mapPic;
        }

        public void MovePlayer(Arrows arrow) // движение игрока 
        {
           switch(arrow) // передвижение игрока (картинки его) 
            {
                case Arrows.left:
                    Move(-step,0);        
                    break;
                case Arrows.right:
                    Move(step, 0);
                    break;
                case Arrows.up:
                    Move(0, -step);
                    break;
                case Arrows.down:
                    Move(0, step);
                    break;
                default:
                    break; 
            }
        }
        private void Move(int sx, int sy) // изменение расположение картинки для движение 
        {
            player.Location = new Point(player.Location.X +sx, player.Location.Y +sy);
        }
        private Point MyNowPoint() // расположение игрока 
        {
            Point point = new Point();
            {
                point.X = player.Location.X + player.Size.Width/2;
                point.Y = player.Location.Y + player.Size.Height / 2; 
            }
            for (int x = 0; x < mapPic.GetLength(0); x++) // нахождение расположение игрока по боксам картинки
                for (int y = 0; y < mapPic.GetLength(1); y++)
                {
                    if (mapPic[x, y].Location.X < point.X &&
                        mapPic[x, y].Location.Y < point.Y &&
                        mapPic[x, y].Location.X + mapPic[x, y].Size.Width > point.X &&
                        mapPic[x, y].Location.Y + mapPic[x, y].Size.Height > point.Y)
                        return new Point(x, y); 
                }
            return point; 

        }
    }
}
