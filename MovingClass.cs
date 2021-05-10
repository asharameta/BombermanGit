using System.Drawing;
using System.Windows.Forms;

namespace bomberman
{
    class MovingClass
    {
        PictureBox player; //игрок (картинка)
        PictureBox[,] mapPic; // карта
        Sost[,] map; // состояние карта 
        deAddBonus addBonus; 
        public MovingClass(PictureBox item, PictureBox[,] _mapPic, Sost[,] _map, deAddBonus methodBonus) // конструктор. передаем параметры 
        {
            player = item;
            mapPic = _mapPic;
            map = _map;
            addBonus = methodBonus; 
        }
        public void Move(int sx, int sy) // изменение расположение картинки для движение 
        {
            if (isEmpty(ref sx, ref sy))//проверяем пустая ли клетка следующая для хода 
            {
                player.Location = new Point(player.Location.X + sx, player.Location.Y + sy);
                Point myPlace = MyNowPoint();
                if(map[myPlace.X,myPlace.Y] == Sost.bonus)
                {
                    addBonus(BonusClass.GetBonus()); 
                    map[myPlace.X, myPlace.Y] = Sost.empty;
                    mapPic[myPlace.X, myPlace.Y].Image = Properties.Resources.grass; 
                }
            }
        }

        private bool isEmpty(ref int sx, ref int sy) // emptyта на карте можно идти , ref дает права для изменение переменных 
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
            int leftUpWallDown = mapPic[playerPoint.X - 1, playerPoint.Y - 1].Location.Y + mapPic[playerPoint.X - 1, playerPoint.Y - 1].Size.Height;
            int leftDownWallUp = mapPic[playerPoint.X - 1, playerPoint.Y + 1].Location.Y;

            int rightUpWallLeft = mapPic[playerPoint.X + 1, playerPoint.Y - 1].Location.X;
            int leftUpWallRight = mapPic[playerPoint.X - 1, playerPoint.Y - 1].Location.X + mapPic[playerPoint.X - 1, playerPoint.Y - 1].Size.Width;
            int rightDownWallLeft = mapPic[playerPoint.X + 1, playerPoint.Y + 1].Location.X;
            int leftDownWallRight = mapPic[playerPoint.X - 1, playerPoint.Y + 1].Location.X + mapPic[playerPoint.X - 1, playerPoint.Y + 1].Size.Width;
            // проверяем пустая клетка и можно ли туда идти . еще меняем параметры х и у что бы не проходить рядом боксов

            int offset = 10; // для плавного перемещение. смещение игрока

            if (sx > 0 && (map[playerPoint.X + 1, playerPoint.Y] == Sost.empty ||
                           map[playerPoint.X + 1, playerPoint.Y] == Sost.fire  ||
                           map[playerPoint.X + 1, playerPoint.Y] == Sost.bonus))
            {
                if (playerUp < rightUpWallDown)
                    if (rightUpWallDown - playerUp > offset)
                        sy = offset;
                    else
                        sy = rightUpWallDown - playerUp;
                if (playerDown > rightDownWallUp)
                    if (rightDownWallUp - playerDown < -offset)
                        sy = -offset;
                    else
                        sy = rightDownWallUp - playerDown;
                return true;
            }
            if (sx < 0 && (map[playerPoint.X - 1, playerPoint.Y] == Sost.empty||
                           map[playerPoint.X - 1, playerPoint.Y] == Sost.fire ||
                           map[playerPoint.X - 1, playerPoint.Y] == Sost.bonus))
            {
                if (playerUp < leftUpWallDown)
                    if (leftUpWallDown - playerUp > offset)
                        sy = offset;
                    else
                        sy = leftUpWallDown - playerUp;
                if (playerDown > leftDownWallUp)
                    if (leftDownWallUp - playerDown < -offset)
                        sy = -offset;
                    else
                        sy = leftDownWallUp - playerDown;
                return true;
            }
            if (sy > 0 && (map[playerPoint.X, playerPoint.Y + 1] == Sost.empty||
                           map[playerPoint.X, playerPoint.Y + 1] == Sost.fire ||
                           map[playerPoint.X, playerPoint.Y + 1] == Sost.bonus))
            {
                if (playerRight > rightDownWallLeft)
                    if (rightDownWallLeft - playerRight < -offset)
                        sx = -offset;
                    else
                        sx = rightDownWallLeft - playerRight;
                if (playerLeft < leftDownWallRight)
                    if (leftDownWallRight - playerLeft > offset)
                        sx = offset;
                    else
                        sx = leftDownWallRight - playerLeft;
                return true;
            }
            if (sy < 0 && (map[playerPoint.X, playerPoint.Y - 1] == Sost.empty||
                           map[playerPoint.X, playerPoint.Y - 1] == Sost.fire ||
                           map[playerPoint.X, playerPoint.Y - 1] == Sost.bonus))
            {
                if (playerRight > rightUpWallLeft)
                    if (rightUpWallLeft - playerRight < -offset)
                        sx = -offset;
                    else
                        sx = rightUpWallLeft - playerRight;
                if (playerLeft < leftUpWallRight)
                    if (leftDownWallRight - playerLeft > offset)
                        sx = offset;
                    else
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
        public Point MyNowPoint() // расположение игрока 
        {
            Point point = new Point();
            {
                point.X = player.Location.X + player.Size.Width / 2;
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
