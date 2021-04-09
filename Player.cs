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
        int step = 5; // длина шага
        MovingClass moving;
        public Player(PictureBox _player, PictureBox[,]_mapPic, Sost[,] _map)
        {
            player = _player;
            mapPic = _mapPic;
            map = _map;
            moving = new MovingClass(_player,_mapPic,_map);
        }

        public void MovePlayer(Arrows arrow) // движение игрока 
        {
           switch(arrow) // передвижение игрока (картинки его) 
            {
                case Arrows.left:
                    moving.Move(-step,0);        
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
       
    }
}
