namespace bomberman
{
    partial class FormGame
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.StripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяИграToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обИгреToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обАвтареToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelGame = new System.Windows.Forms.Panel();
            this.labelScore = new System.Windows.Forms.Label();
            this.timerFireClear = new System.Windows.Forms.Timer(this.components);
            this.GameOverTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(906, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // StripMenuItem
            // 
            this.StripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяИграToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.StripMenuItem.Name = "StripMenuItem";
            this.StripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.StripMenuItem.Text = "File";
            this.StripMenuItem.Click += new System.EventHandler(this.StripMenuItem_Click);
            // 
            // новаяИграToolStripMenuItem
            // 
            this.новаяИграToolStripMenuItem.Name = "новаяИграToolStripMenuItem";
            this.новаяИграToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.новаяИграToolStripMenuItem.Text = "New Game";
            this.новаяИграToolStripMenuItem.Click += new System.EventHandler(this.новаяИграToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.выходToolStripMenuItem.Text = "Exit";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обИгреToolStripMenuItem,
            this.обАвтареToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.справкаToolStripMenuItem.Text = "Help";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // обИгреToolStripMenuItem
            // 
            this.обИгреToolStripMenuItem.Name = "обИгреToolStripMenuItem";
            this.обИгреToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.обИгреToolStripMenuItem.Text = "About Game";
            this.обИгреToolStripMenuItem.Click += new System.EventHandler(this.обИгреToolStripMenuItem_Click);
            // 
            // обАвтареToolStripMenuItem
            // 
            this.обАвтареToolStripMenuItem.Name = "обАвтареToolStripMenuItem";
            this.обАвтареToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.обАвтареToolStripMenuItem.Text = "About Author";
            this.обАвтареToolStripMenuItem.Click += new System.EventHandler(this.обАвтареToolStripMenuItem_Click);
            // 
            // panelGame
            // 
            this.panelGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(130)))), ((int)(((byte)(133)))));
            this.panelGame.Location = new System.Drawing.Point(32, 73);
            this.panelGame.Margin = new System.Windows.Forms.Padding(2);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(835, 539);
            this.panelGame.TabIndex = 1;
            this.panelGame.Paint += new System.Windows.Forms.PaintEventHandler(this.panelgame_Paint);
            // 
            // labelScore
            // 
            this.labelScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelScore.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelScore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelScore.Location = new System.Drawing.Point(0, 23);
            this.labelScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(906, 19);
            this.labelScore.TabIndex = 0;
            this.labelScore.Text = "NewGame";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerFireClear
            // 
            this.timerFireClear.Interval = 1000;
            this.timerFireClear.Tick += new System.EventHandler(this.timerFireClear_Tick);
            // 
            // GameOverTimer
            // 
            this.GameOverTimer.Tick += new System.EventHandler(this.GameOverTimer_Tick);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 636);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BomberMan";
            this.Load += new System.EventHandler(this.formgame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяИграToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обИгреToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обАвтареToolStripMenuItem;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Timer timerFireClear;
        private System.Windows.Forms.Timer GameOverTimer;
    }
}

