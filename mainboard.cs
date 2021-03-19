using System.Windows.Forms;
using System.Drawing; 

namespace bomberman   // локация 
{
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
         public MainBoard(Panel panel) // конструктор 
        {
            panelGame = panel; // присвоение понели 

            InitStartMap();  //создание карты 
        }

        private void InitStartMap()
        {
            mapPic = new PictureBox[sizeX, sizeY];  // инцилизация картинок 
            map = new Sost[sizeX, sizeY]; // создание сосятоние карты 

            panelGame.Controls.Clear();  //вычищения игрового поля (если что то осталось с прошлого запуска) для безопасности 

            int boxSize;
            if ((panelGame.Width / sizeX) < (panelGame.Height / sizeY))  // узнаем размер бокса ( что чего больше по Х или У) и делим 
                boxSize = panelGame.Width / sizeX;
            else
                boxSize = panelGame.Height / sizeY;


            for (int x = 0; x< sizeX; x++) //заполнение массива по "x and y" 
                for(int y=0; y<sizeY; y++)
                {
                    CreatePlace(x, y, boxSize); 
                }
        }
        private void CreatePlace(int x, int y, int boxSize) // заполнение карты 
        {
            PictureBox picture = new PictureBox(); // создание пустого 
             // настроика 
           picture.Location = new Point(x * (boxSize - 1), y * (boxSize - 1)); // место положение Box
            picture.Size = new Size(boxSize, boxSize);    // размер  Box
            picture.BorderStyle = BorderStyle.FixedSingle; // создает видимость картинок (пикчер) 
            picture.SizeMode = PictureBoxSizeMode.StretchImage; // заполняет картинку весь бокс который мы создали 
            mapPic[x, y] = picture; // присвоение картинки 
            panelGame.Controls.Add(picture); // добавление картинки на панель 
            picture.BackColor = Color.WhiteSmoke; // присвоение цвета  WhiteSmoke
        }
    }
}
