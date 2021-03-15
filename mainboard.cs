using System.Windows.Forms;
using System.Drawing; 

namespace bomberman
{
    // lexa lox
    //anton lox
    enum Sost
    {
        пусто,
        стена,
        кирпич
    }
    class MainBoard
    {
        Panel panelGame;
        PictureBox[,] mapPic;
        Sost[,] map; 
        int sizeX = 17;
        int sizeY = 11;
         public MainBoard(Panel panel)
        {
            panelGame = panel;

            InitStartMap();
        }

        private void InitStartMap()
        {
            mapPic = new PictureBox[sizeX, sizeY];
            map = new Sost[sizeX, sizeY];

            panelGame.Controls.Clear();

            int boxSize;
            if ((panelGame.Width / sizeX) < (panelGame.Height / sizeY))
                boxSize = panelGame.Width / sizeX;
            else
                boxSize = panelGame.Height / sizeY;
            for (int x = 0; x< sizeX; x++)
                for(int y=0; y<sizeY; y++)
                {
                    CreatePlace(x, y, boxSize);
                }
        }
        private void CreatePlace(int x, int y, int boxSize)
        {
            PictureBox picture = new PictureBox();

           picture.Location = new Point(x * (boxSize - 1), y * (boxSize - 1));
            picture.Size = new Size(boxSize, boxSize);
            picture.BorderStyle = BorderStyle.FixedSingle;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            mapPic[x, y] = picture;
            panelGame.Controls.Add(picture); 
            picture.BackColor = Color.WhiteSmoke; 
        }
    }
}
