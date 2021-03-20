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
        public FormGame()
        {
            InitializeComponent();
            Init(); // запуска игрового поля 
        }
        private void Init()
        {
            MainBoard board = new MainBoard(panelGame); 
        }

        private void formgame_Load(object sender, EventArgs e)
        {

        }

        private void aboutGameToolStripMenuItem_Click(object sender, EventArgs e) //описание меню игры 
        {
            MessageBox.Show("Gameplay " +
                "\nMost of the games in the series are made in the genre of arcade maze. " +
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
                "there is also a turn-based strategy game", "Game Description");
        }

        private void aboutAuthorToolStripMenuItem_Click(object sender, EventArgs e)//описание меню автора
        {
            MessageBox.Show("Anton Pashkevich 09.03.1997, Aliaksei Sharameta 01.08.2002, \n Students of the University of Lodz \n 3rd year student, Computer graphics and Game Programming" +
                "\nFor feedback, use \nE-mail: natoy7000@gmail.com ", "About Author"); 
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
    }
}
