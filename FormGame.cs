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
    public delegate void deClear(); // очистка 
    public partial class FormGame : Form
    {
        System.Media.SoundPlayer player;
        MainBoard board;
        int level = 0; // уровень сложности
        int mobs = 0; // количество мобов 

        public FormGame()
        {
            player = new System.Media.SoundPlayer();
            InitializeComponent();
            NewGame(); // запуска игрового поля 
        }
        private void NewGame() // запуск игры
        {
            level++;
            mobs++; 
            board = new MainBoard(panelGame, StartClear, labelScore,mobs);  //создание карты панели 
            ChageLevel(level); // меняет уровень;
            GameOverTimer.Enabled = true; // вкл таймера проигрыша
            timer1.Enabled = true;  // вкл таймера выйгреша
            timer2.Enabled = true;
        }

        private void formgame_Load(object sender, EventArgs e)
        {

        }
        //обИгреToolStripMenuItem_Click
        private void AboutGameToolStripMenuItem_Click(object sender, EventArgs e) //описание меню игры 
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

        private void AboutAuthorToolStripMenuItem_Click(object sender, EventArgs e)//описание меню автора
        {
            MessageBox.Show("Anton Pashkevich, Aliaksei Sharameta \nStudents of the University of Lodz \n3rd year students, Computer graphics and Game Programming" +
                "\nFor feedback: natoy7000@gmail.com", "About Authors"); 
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e) // при нажатие клавиатуры (форма, клавиша) 
        {
            if (GameOverTimer.Enabled) // таймер выкл, ты проиграл 
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
                    case Keys.Space: board.PutBomb(); break;

                }
            }        
                    
      
        }

        private void timerFireClear_Tick(object sender, EventArgs e) // таймер вычещение огня после зрыва
        {
            board.ClearFire(); // запуск функций вычещение огня 
            timerFireClear.Enabled = false; // выкл таймера 
        }
        private void StartClear() // включение таймера вычищение огня 
        {
            timerFireClear.Enabled = true;
        }

        private void GameOverTimer_Tick(object sender, EventArgs e) //проверяет проиграли 
        {
            if(board.GameOver()) // игра закончилось  
            {
                GameOverTimer.Enabled = false;
                DialogResult dr = MessageBox.Show("You died!\nDo you want to play again?", "Game Over!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr==System.Windows.Forms.DialogResult.Yes)
                {
                    NewGame(); 
                }
                else
                {
                    this.Close();//Это я от себя добавил если нажать No то закроется окно, а можно по его способу просто обездвижить игрока.
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e) // выиграли  убили всех врагов 
        { 
            if (board.GameFinish())
            {
                timer1.Enabled = false;
                board.NextLevel(); 
            }

        }
        private void timer2_Tick(object sender, EventArgs e) // дошли до двери 
        {
            if (board.NextGame())
            {
                timer2.Enabled = false;
                DialogResult dr = MessageBox.Show("NEXT LEVEL", "Game Finish!", MessageBoxButtons.OK, MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    NewGame();
                }
            }
        }



        private void newGameToolStripMenuItem_Click(object sender, EventArgs e) //перезапуск игр
        {
            NewGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) // выход игры
        {
            this.Close();
        }

        private void ChageLevel(int _level)
        {
            level = _level;
            board.SetMobLevel(level); 
        }


        //смена сложности 

        private void easyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ChageLevel(1);
        }

        private void normalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ChageLevel(2);
        }

        private void hardToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ChageLevel(3);
        }

        private void oNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player.SoundLocation = @"menu.wav";
            player.Play();
        }

        private void oFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void labelScore_Click(object sender, EventArgs e)
        {

        }
    }
}
