using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace bomberman   // локация 
{
    public delegate void deBlow(Bomb b); // метод взрыва  delegate - передает ссылку на функцию что бы вызвать 
    enum Sost    // перечисление состояний на поле 
    {
        empty,
        wall,
        brick,
        bomb,
        fire,
        door, 
        door2,
        bonus
    }
    class MainBoard
    {
        Panel panelGame;     // панель игровая 
        PictureBox[,] mapPic; // картинки 
        Sost[,] map;  // массив состояний карт 
        int sizeX = 17;     // ширина игрового поля 
        int sizeY = 11;     // высота игрового поля 
        int sizeMob = 3;     // количество врагов 
        static Random rand = new Random(); // рандом для создание блоков которые можно будет рушить 
        Player player; // наш игрок
        List<Mob> mobs; // враг 
        deClear NeedClear; // делегат очистки огня
        int boxSize;
        Label score; //статистика панелька 
        System.Media.SoundPlayer sounds;
        public MainBoard(Panel panel, deClear _clear, Label _score, int _mobs) // конструктор 
        {
            sounds = new System.Media.SoundPlayer();
            NeedClear = _clear;  // очищение огня поосле бомбы
            panelGame = panel; // присвоение понели 
            score = _score; // статистика 
            sizeMob = _mobs; 
            mobs = new List<Mob>();           
            if ((panelGame.Width / sizeX) < (panelGame.Height / sizeY))  // узнаем размер бокса ( что чего больше по Х или У) и делим 
                boxSize = panelGame.Width / sizeX;
            else
                boxSize = panelGame.Height / sizeY;
            InitStartMap(boxSize+1);  //создание карты 
            InitStartPlayer(boxSize);  // создание игрока
            for (int i = 0; i < sizeMob; i++) // создание количество мобов 
            {
                InitMob(boxSize); //создание моба
            }
            BonusClass.Prepare();

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
                        CreatePlace(new Point(x, y), boxSize, Sost.wall);
                    else if(x%2 == 0 && y%2==0)  // через 2 бокса стенка
                        CreatePlace(new Point(x, y), boxSize, Sost.wall);
                    else if(rand.Next(3) == 0) // рандом из 3 чисел, когда выбодит число 0 создаст brick 
                        CreatePlace(new Point(x, y), boxSize, Sost.brick);
                    else
                        CreatePlace(new Point(x, y), boxSize, Sost.empty);
                    //   CreatePlace(new Point(x, y), boxSize,); 
                }

            // пустые три клеточки, для игрока что бы он мог запуститься и не подорвать себя что бы выйти из керпичей 
            ChangeSost(new Point(1, 1),Sost.empty);
            ChangeSost(new Point(2, 1), Sost.empty);
            ChangeSost(new Point(1, 2), Sost.empty);
        }
        private void CreatePlace(Point point, int boxSize, Sost sost) // заполнение карты 
        {
            PictureBox picture = new PictureBox(); // создание emptyго 
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

                case Sost.wall:
                    mapPic[point.X, point.Y].Image = Properties.Resources.wall;
                    break;
                case Sost.brick:
                    mapPic[point.X, point.Y].Image = Properties.Resources.brick;
                    break;
                case Sost.bomb:
                    mapPic[point.X, point.Y].Image = Properties.Resources.bomb;
                    break;
                case Sost.fire:
                    mapPic[point.X, point.Y].Image = Properties.Resources.fire;
                    break;
                case Sost.bonus:
                        mapPic[point.X, point.Y].Image = Properties.Resources.prize;  
                    break;
                case Sost.door:
                    mapPic[point.X, point.Y].Image = Properties.Resources.door2;
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
            PictureBox picture = new PictureBox(); //картинка главного игрока
            Bitmap picture1 = new Bitmap(Properties.Resources.player);
            picture.Location = new Point(x * (boxSize) + 7, y * (boxSize) + 3); // местоположение игрока  (по середине)
            picture.Size = new Size(boxSize - 14, boxSize - 6); // размер игрока               (можно редактировать)
            picture.Image = Properties.Resources.player; // присвоение картинки игрока
            picture.BackgroundImage = Properties.Resources.grass;  // задний фон игрока
            picture.BackgroundImageLayout = ImageLayout.Stretch;  // задний фон растянуло на всю клеточку 
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // задний фон растянуло на всю клеточку 
            panelGame.Controls.Add(picture); // добавление картинку на контролс
            picture.BringToFront(); // выджвижение игрока на передний фон 
            player = new Player(picture, mapPic, map, score);  // присвоили картинку игрока к игроку (классу) 
        }

        private void InitMob(int boxSize) // создание моба 
        {
            int x = sizeX - 2, y = sizeY - 2;  // кординаты создание моба
            FindEmptyPlace(out x, out y); // ищет свободное место для создание моба 
            PictureBox picture = new PictureBox(); //кортинка моба
            picture.Location = new Point(x * (boxSize) + 7, y * (boxSize) + 3); // местоположение игрока  (по середине)
            picture.Size = new Size(boxSize - 11, boxSize - 5); // размер mob             (можно редактировать)
            picture.Image = Properties.Resources.enemy; // присвоение картинки mob
            picture.BackgroundImage = Properties.Resources.grass;  // задний фон mob 
            picture.BackgroundImageLayout = ImageLayout.Stretch;  // задний фон растянуло на всю клеточку 
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // задний фон растянуло на всю клеточку 
            panelGame.Controls.Add(picture); // добавление картинку на контролс
            picture.BringToFront(); // выджвижение mob на передний фон 
            mobs.Add(new Mob(picture, mapPic, map, player)); // передача инфы мобу 
        }
        private void FindEmptyPlace(out int x, out int y) // ищет emptyе место для создание моба 
        {
            int loop = 0; // выйти из цикла, если нету emptyго места (страховка) 
            do
            {
                x = rand.Next(map.GetLength(0)/2, map.GetLength(0)); // рандомное число с половины поля, что бы не появлялись около врага 
                y = rand.Next(1, map.GetLength(1));
            } while (map[x,y]!=Sost.empty&&loop++<100);
        }

        public void MovePlayer(Arrows arrow) // движение игрока
        {
            if (player == null) return; // страховка, если игрока нету и нажали движение      
            player.MovePlayer(arrow);// передаем движение игроку 
        }

        public void PutBomb() // установка бомбы 
        {
            Point playerPoint = player.MyNowPoint(); // расположение игрока
            if (map[playerPoint.X, playerPoint.Y] == Sost.bomb) return; // нельзя ставить 2 бомбы на одном месте 
            if (map[playerPoint.X, playerPoint.Y] == Sost.door) return;
            if (player.PutBomb(mapPic, deBlow)) // проверка можно ли бомбу ставить 
            {
                ChangeSost(player.MyNowPoint(), Sost.bomb); // установка самой бомбы на позиций игрока 
                sounds.SoundLocation = @"put.wav";
                sounds.Play();
            }    
        }

        private void deBlow(Bomb bomb) // взрыв бомбы
        {
            ChangeSost(bomb.bombPlace, Sost.fire); //бомбу меняем на взрыв 
            // функция для растановки огня по направлению лево, право , верх, вниз
            Flame(bomb.bombPlace, Arrows.left);
            Flame(bomb.bombPlace, Arrows.right);
            Flame(bomb.bombPlace, Arrows.up);
            Flame(bomb.bombPlace, Arrows.down);
            player.RemoveBomb(bomb);  // удаление мобов 
            Blaze(); // удаление мобов 
            //  MessageBox.Show(str); // вызывает окошка бэнг

            NeedClear(); // очистка поли от огня
        }

        private void Blaze() // удаление мобов 
        {
            List<Mob> delMobs = new List<Mob>(); 
            foreach(Mob mob in mobs) // перебераем мобов которые находиться на огне 
            {
                Point mobPoint = mob.MyNowPoint();
                if (map[mobPoint.X, mobPoint.Y] == Sost.fire)
                    delMobs.Add(mob); 
            }
            for(int x=0; x < delMobs.Count; x++) // удаление мобов 
            {
                mobs.Remove(delMobs[x]); // удаляем моб с индексом моба 
                panelGame.Controls.Remove(delMobs[x].mob); // удаляем моба с карты 
                delMobs[x] = null; // адрес emptyты устанавливаем 
            }
            GC.Collect(); // очищение памяти, после удаление обьектов 
            GC.WaitForPendingFinalizers(); 
        }
        private void Flame(Point bombPlace, Arrows arrow) // размещает fire 
        {
            int sx = 0, sy = 0;
            switch (arrow)
            {
                case Arrows.left:
                    sx = -1;
                    break;
                case Arrows.right:
                    sx = 1;
                    break;
                case Arrows.up:
                    sy = -1;
                    break;
                case Arrows.down:
                    sy = 1;
                    break;
                default:
                    break; 
            }
                                
            bool isNotDone = true;
            int x = 0, y = 0;
            do
            {
                x += sx; y += sy; 
                if (Math.Abs(x) > player.leftFire || Math.Abs(y) > player.leftFire)
                    break;
                if (isFire(bombPlace, x, y)) // может быть fire, и изменяем состояние карты на fire
                    ChangeSost(new Point(bombPlace.X + x, bombPlace.Y + y), Sost.fire);
                else
                    isNotDone = false;

            } while (isNotDone);

        }
        private bool isFire(Point place, int sx, int sy) // проверяет можем ли мы состояние карты поменять на fire 
        {                                    
            switch(map[place.X+sx,place.Y+sy])// проверяем состояние карты                             
            {
                case Sost.empty:
                    return true;
                case Sost.wall:
                    return false;
                case Sost.door:
                    return false;
                case Sost.brick:
                    if(rand.Next(3)==0) // создание бонуса рандом из 3 чисел
                        ChangeSost(new Point(place.X + sx, place.Y + sy), Sost.bonus);
                    else
                    ChangeSost(new Point(place.X + sx, place.Y + sy), Sost.fire);
                    return false;
                case Sost.bomb:   // взрываем бомбы если они на пути бомбы 
                   foreach(Bomb bomb in player.bombs)
                    {
                        if (bomb.bombPlace == new Point(place.X + sx, place.Y + sy))
                            bomb.Reaction(); 
                    }
                    return false;
                default:
                    return true; 
                                            
            }         


        }
        public void ClearFire()  // очистка огня 
        {
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == Sost.fire) // изменяем состояние огня на пустое 
                        ChangeSost(new Point(x, y), Sost.empty);
                }
        }
        public bool GameOver() // игра закончилось 
        {
            Point myPoint = player.MyNowPoint();

            if (map[myPoint.X, myPoint.Y] == Sost.fire)// попал на огонь конец игре
            {
                sounds.SoundLocation = @"you_lose.wav";
                sounds.Play();
                return true;
            } 
            foreach (Mob mob in mobs) // если игрок попался на моба
            {
                if (myPoint == mob.MyNowPoint())
                {
                    sounds.SoundLocation = @"loser.wav";
                    sounds.Play();
                    return true;
                }
            }
            return false;
        }
        public bool GameFinish() // игра закончилось 
        {
            if (mobs.Count == 0)// закончились мобы конец игре 
            {
                sounds.SoundLocation = @"you_win.wav";
                sounds.Play();
                return true;
            }
            return false;
        }
        public void SetMobLevel(int level) // установка уровня 
        {
            foreach(Mob mob in mobs)
            {
                mob.SetLevel(level); 
            }
        }

        public void NextLevel() // создание двери
        {
            int x = rand.Next(3, sizeX - 1);
            int y = rand.Next(3, sizeY - 1);
            ChangeSost(new Point(x, y), Sost.door); 
        }
        public bool NextGame() // дошли до двери 
        {
            Point myPoint = player.MyNowPoint();
            if (map[myPoint.X, myPoint.Y] == Sost.door)
                return true; 
            return false;
        }


    }
}
