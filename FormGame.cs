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

        private void обИгреToolStripMenuItem_Click(object sender, EventArgs e) //описание меню игры 
        {
            MessageBox.Show("Игровой процесс " +
                "\nБольшинство игр серии выполнены в жанре аркадного лабиринта." +
                "Игрок управляет персонажем, находящимся в лабиринте, состоящем" +
                " из разрушаемых и неразрушаемых стен.Он может оставлять бомбу," +
                " взрывающуюся через некоторое фиксированное время и разрушающую " +
                "стены рядом с ней.Специальные бонусы могут увеличить количество" +
                " одновременно оставляемых бомб, дальность их взрыва, скорость" +
                " перемещения героя, дать возможность взрыва бомб по нажатию" +
                " кнопки, невосприимчивость от взрыва бомб, прохождение сквозь" +
                " разрушаемые стены или сквозь собственные еще невзорванные" +
                " бомбы.На уровне присутствуют противники.В некоторых играх " +
                "серии целью игры является нахождение скрытой за одной из" +
                " разрушаемых стен двери, ведущей в следующий уровень с" +
                " предварительным уничтожением врагов.Другие игры рассчитаны " +
                "на многопользовательскую игру на одном экране, целью в них" +
                " является победа над всеми противниками.Среди игр серии " +
                "также есть пошаговая стратегия","Описание игры");
        }

        private void обАвтареToolStripMenuItem_Click(object sender, EventArgs e)//описание меню автора
        {
            MessageBox.Show("Анто Пашкевич 09.03.1997 \n Студент Унверситата Лодзь \n Студент 3 курса, Проектирование игр и графический дизайн" +
                "\nДля связи использовать \nE-mail: natoy7000@gmail.com ","Об Авторе"); 
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
