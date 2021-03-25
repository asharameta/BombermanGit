using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomberman
{
    public partial class FormGame : Form
    {
        MainBoard board;

        public FormGame()
        {
            InitializeComponent();
            Init(); // запуска игрового поля 
        }
        private void Init()
        {
            board = new MainBoard(panelGame);  //создание карты панели 
        }

        private void formgame_Load(object sender, EventArgs e)
        {

        }

        private void обИгреToolStripMenuItem_Click(object sender, EventArgs e) //описание меню игры 
        {
            MessageBox.Show(
                "Most of the games in the series are made in the genre of arcade maze. " +
                "The player controls a character located in a maze consisting of" +
                " from destructible and indestructible walls. He can leave a bomb" +
                " exploding after a certain fixed time and destroying " +
                "the walls are next to it. Special bonuses can increase the number of" +
                " bombs left at the same time the range of their explosion, the speed" +
                " move the player, give the opportunity to explode bombs by pressing" +
                " buttons immunity from the explosion of bombs passing through" +
                " destructible walls or through your own walls that haven't been blown up yet" +
                " bombs. There are opponents on the level. In some games " +
                "series the goal of the game is to find the hidden behind one of the" +
                " destructible walls of the door leading to the next level with" +
                " preliminary destruction of enemies. Other games are calculated " +
                "on a single-screen multiplayer game aim at them" +
                " it is a victory over all opponents. Among the games in the series " +
                "there is also a turn-based strategy game.", "Game Description");
        }

        private void обАвтареToolStripMenuItem_Click(object sender, EventArgs e)//описание меню автора
        {
            MessageBox.Show("Anton Pashkevich, Aliaksei Sharameta, Kiryl Lysiuk \nStudents of the University of Lodz \n3rd year students, Computer graphics and Game Programming" +
                "\nFor feedback: natoy7000@gmail.com", "About Authors"); 
        }

        private void panelgame_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e) // при нажатие клавиатуры (форма, клавиша) 
        {
            switch (e.KeyCode) // нажатие клавиши(движение игрока) 
            {
                  // управление стрелочками (вчитывает символ, и в майнборд, передает значение) 
                case Keys.Left: board.MovePlayer(Arrows.left); break;
                case Keys.Right: board.MovePlayer(Arrows.right); break;
                case Keys.Up: board.MovePlayer(Arrows.up); break;
                case Keys.Down: board.MovePlayer(Arrows.down); break;
                    //управление буквами
                case Keys.A: board.MovePlayer(Arrows.left); break;
                case Keys.D: board.MovePlayer(Arrows.right); break;
                case Keys.W: board.MovePlayer(Arrows.up); break;
                case Keys.S: board.MovePlayer(Arrows.down); break;

            }
                    
                    
      
        }
    }
}
