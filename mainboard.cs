using System.Windows.Forms;
using System.Drawing;
using System;

namespace bomberman   // локация 
{
    public delegate void deBlow();
    enum Sost    // перечисление состояний на поле 
    {
        пусто,
        стена,
        кирпич,
        бомба,
        огонь,
        приз
    }
    class MainBoard
    {
        Panel panelGame;     // панель игровая 
        PictureBox[,] mapPic; // картинки 
        Sost[,] map;  // массив состояний карт 
        int sizeX = 17;     // ширина игрового поля 
        int sizeY = 11;     // высота игрового поля 
        static Random rand = new Random(); // рандом для создание блоков которые можно будет рушить 
        Player player; // наш игрок
        Mob mob; // противник(моб, враг)
         public MainBoard(Panel panel) // конструктор 
        {
            panelGame = panel; // присвоение понели 
            int boxSize;
            if ((panelGame.Width / sizeX) < (panelGame.Height / sizeY))  // узнаем размер бокса ( что чего больше по Х или У) и делим 
                boxSize = panelGame.Width / sizeX;
            else
                boxSize = panelGame.Height / sizeY;
            InitStartMap(boxSize+1);  //создание карты 
            InitStartPlayer(boxSize);  // создание игрока
            for (int i = 0; i < 3; i++)
            {
                InitMob(boxSize); //создание моба
            }

        }

        private void InitStartMap(int boxSize)
        {
            mapPic = new PictureBox[sizeX, sizeY];  // инцилизация картинок 
            map = new Sost[sizeX, sizeY]; // создание сосятоние карты 

            panelGame.Controls.Clear();  //вычищения игрового поля (если что то осталось с прошлого запуска) для безопасности 




            for (int x = 0; x< sizeX; x++) //заполнение массива по "x and y" 
                for(int y=0; y<sizeY; y++)
                {
                    if(x==0 || y==0 || x == sizeX - 1 || y == sizeY -1 ) // рамка стены 
                        CreatePlace(new Point(x, y), boxSize, Sost.стена);
                    else if(x%2 == 0 && y%2==0)  // через 2 бокса стенка
                        CreatePlace(new Point(x, y), boxSize, Sost.стена);
                    else if(rand.Next(3) == 0) // рандом из 3 чисел, когда выбодит число 0 создаст кирпич 
                        CreatePlace(new Point(x, y), boxSize, Sost.кирпич);
                    else
                        CreatePlace(new Point(x, y), boxSize, Sost.пусто);
                    //   CreatePlace(new Point(x, y), boxSize,); 
                }

            // пустые три клеточки, для игрока что бы он мог запуститься и не подорвать себя что бы выйти из керпичей 
            ChangeSost(new Point(1, 1),Sost.пусто);
            ChangeSost(new Point(2, 1), Sost.пусто);
            ChangeSost(new Point(1, 2), Sost.пусто);
        }
        private void CreatePlace(Point point, int boxSize, Sost sost) // заполнение карты 
        {
            PictureBox picture = new PictureBox(); // создание пустого 
             // настроика 
           picture.Location = new Point(point.X * (boxSize - 1), point.Y * (boxSize - 1)); // место положение Box
            picture.Size = new Size(boxSize, boxSize);    // размер  Box
//            picture.BorderStyle = BorderStyle.FixedSingle; // создает видимость картинок (пикчер) рамку  
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // заполняет картинку весь бокс котор ый мы создали 
            mapPic[point.X, point.Y] = picture; // присвоение картинки 
            ChangeSost(point, sost);  // заполнение картинками 
            panelGame.Controls.Add(picture); // добавление картинки на панель 
  //          picture.BackColor = Color.WhiteSmoke; // присвоение цвета  WhiteSmoke
        }

        // point - x,y адрес 
        private void ChangeSost(Point point, Sost newSost) // изменение состоянние карты 
        {
            switch (newSost) // присвоение картинки 
            {

                case Sost.стена:
                    mapPic[point.X, point.Y].Image = Properties.Resources.wall;
                    break;
                case Sost.кирпич:
                    mapPic[point.X, point.Y].Image = Properties.Resources.brick;
                    break;
                case Sost.бомба:
                    mapPic[point.X, point.Y].Image = Properties.Resources.bomb;
                    break;
                case Sost.огонь:
                    mapPic[point.X, point.Y].Image = Properties.Resources.fire;
                    break;
                case Sost.приз:
                        mapPic[point.X, point.Y].Image = Properties.Resources.prize;  
                    break;
                default:
                    mapPic[point.X, point.Y].Image = Properties.Resources.grass;
                    break;
            }
          map[point.X, point.Y] = newSost; // состояние карты
        }

        private void InitStartPlayer(int boxSize)
        {
            int x = 1, y = 1;  // кординаты создание главного игрока
            PictureBox picture = new PictureBox(); //кортинка главного игрока
            picture.Location = new Point(x * (boxSize) + 7, y * (boxSize) + 3); // местоположение игрока  (по середине)
            picture.Size = new Size(boxSize - 14 , boxSize - 6); // размер игрока               (можно редактировать)
            picture.Image = Properties.Resources.player; // присвоение картинки игрока
            picture.BackgroundImage = Properties.Resources.grass;  // задний фон игрока 
            picture.BackgroundImageLayout = ImageLayout.Stretch;  // задний фон растянуло на всю клеточку 
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // задний фон растянуло на всю клеточку 
            panelGame.Controls.Add(picture); // добавление картинку на контролс
            picture.BringToFront(); // выджвижение игрока на передний фон 
            player = new Player(picture,mapPic,map);  // присвоили картинку игрока к игроку (классу) 
        }
        private void InitMob(int boxSize) // создание моба 
        {
            int x = sizeX - 2, y = sizeY - 2;  // кординаты создание моба
            FindEmptyPlace(out x, out y);
            PictureBox picture = new PictureBox(); //кортинка моба
            picture.Location = new Point(x * (boxSize) + 7, y * (boxSize) + 3); // местоположение игрока  (по середине)
            picture.Size = new Size(boxSize - 11, boxSize - 5); // размер mob             (можно редактировать)
            picture.Image = Properties.Resources.enemy; // присвоение картинки mob
            picture.BackgroundImage = Properties.Resources.grass;  // задний фон mob 
            picture.BackgroundImageLayout = ImageLayout.Stretch;  // задний фон растянуло на всю клеточку 
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // задний фон растянуло на всю клеточку 
            panelGame.Controls.Add(picture); // добавление картинку на контролс
            picture.BringToFront(); // выджвижение mob на передний фон 
            mob = new Mob(picture, mapPic, map);
        }
        private void FindEmptyPlace(out int x, out int y)
        {
            int loop = 0;
            do
            {
                x = rand.Next(map.GetLength(0)/2, map.GetLength(0));
                y = rand.Next(1, map.GetLength(1));
            } while (map[x,y]!=Sost.пусто&&loop++<100);
        }

        public void MovePlayer(Arrows arrow) // движение игрока
        {
            if (player == null) return; // страховка, если игрока нету и нажали движение 
            player.MovePlayer(arrow);// передаем движение игроку 
        }

        public void PutBomb()
        {
            Point playerPoint = player.MyNowPoint();
            if (map[playerPoint.X, playerPoint.Y] == Sost.бомба) return;
            if (player.PutBomb(mapPic))
                ChangeSost(player.MyNowPoint(), Sost.бомба);
        }
    }
}
