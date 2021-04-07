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
        public Player(PictureBox _player, PictureBox[,]_mapPic, Sost[,] _map)
        {
            player = _player;
            mapPic = _mapPic;
            map = _map;
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
            if(isEmpty(ref sx,ref sy))//проверяем пустая ли клетка следующая для хода 
            player.Location = new Point(player.Location.X +sx, player.Location.Y +sy);
        }

        private bool isEmpty(ref int sx, ref int sy) // пустота на карте можно идти , ref дает права для изменение переменных 
        {
            Point playerPoint = MyNowPoint(); // месположение игрока

            //расположение игрока
            int playerRight = player.Location.X + player.Size.Width;
            int playerLeft = player.Location.X;
            int playerDown = player.Location.Y + player.Size.Height;
            int playerUp = player.Location.Y;

            // расположение боксов и их сторон 
            // расположение бокса правого. его левой стенки 
            int rightWallLeft = mapPic[playerPoint.X + 1, playerPoint.Y].Location.X;
            int leftWallRight = mapPic[playerPoint.X - 1, playerPoint.Y].Location.X + mapPic[playerPoint.X - 1, playerPoint.Y].Size.Width;
            int downWallUp = mapPic[playerPoint.X, playerPoint.Y + 1].Location.Y;
            int upWallDown = mapPic[playerPoint.X, playerPoint.Y - 1].Location.Y + mapPic[playerPoint.X, playerPoint.Y - 1].Size.Height;

            // расположение угловых боксов 
            int rightUpWallDown = mapPic[playerPoint.X + 1, playerPoint.Y - 1].Location.Y + mapPic[playerPoint.X + 1, playerPoint.Y - 1].Size.Height;
            int rightDownWallUp = mapPic[playerPoint.X + 1, playerPoint.Y + 1].Location.Y;
            int leftUpWallDown = mapPic[playerPoint.X - 1, playerPoint.Y - 1].Location.Y+ mapPic[playerPoint.X - 1, playerPoint.Y - 1].Size.Height;
            int leftDownWallUp = mapPic[playerPoint.X - 1, playerPoint.Y + 1].Location.Y;

            int rightUpWallLeft = mapPic[playerPoint.X + 1, playerPoint.Y - 1].Location.X; 
            int leftUpWallRight = mapPic[playerPoint.X - 1, playerPoint.Y - 1].Location.X + mapPic[playerPoint.X - 1, playerPoint.Y - 1].Size.Width; 
            int rightDownWallLeft = mapPic[playerPoint.X + 1, playerPoint.Y + 1].Location.X;
            int leftDownWallRight = mapPic[playerPoint.X - 1, playerPoint.Y + 1].Location.X + mapPic[playerPoint.X -1, playerPoint.Y + 1].Size.Width ;
            // проверяем пустая клетка и можно ли туда идти . еще меняем параметры х и у что бы не проходить рядом боксов
            if (sx > 0 && map[playerPoint.X + 1, playerPoint.Y] == Sost.пусто)
            {
                if (playerUp < rightUpWallDown)
                    sy = rightUpWallDown - playerUp;
                if (playerDown > rightDownWallUp)
                    sy = rightDownWallUp - playerDown; 
                return true;
            }
            if (sx < 0 && map[playerPoint.X - 1, playerPoint.Y] == Sost.пусто)
            {
                if (playerUp < leftUpWallDown)
                    sy = leftUpWallDown - playerUp;
                if (playerDown > leftDownWallUp)
                    sy = leftDownWallUp - playerDown;
                return true;
            }
            if (sy > 0 && map[playerPoint.X, playerPoint.Y + 1] == Sost.пусто)
            {
                if (playerRight > rightDownWallLeft)
                    sx = rightDownWallLeft - playerRight;
                if (playerLeft < leftDownWallRight)
                    sx = leftDownWallRight - playerLeft; 
                return true;
            }
            if (sy < 0 && map[playerPoint.X, playerPoint.Y - 1] == Sost.пусто)
            {
                if (playerRight > rightUpWallLeft)
                    sx = rightUpWallLeft - playerRight;
                if (playerLeft < leftUpWallRight)
                    sx = leftUpWallRight - playerLeft; 
                return true;
            }


            if (sx > 0 && playerRight + sx > rightWallLeft)
                sx = rightWallLeft - playerRight;
            if (sx < 0 && playerLeft + sx < leftWallRight)
                sx = leftWallRight - playerLeft;
            if (sy > 0 && playerDown + sy > downWallUp)
                sy = downWallUp - playerDown;
            if (sy < 0 && playerUp + sy < upWallDown)
                sy = upWallDown - playerUp; 

            return true;
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
