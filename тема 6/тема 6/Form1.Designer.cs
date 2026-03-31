using System;
using System.Drawing;
using System.Windows.Forms;

namespace EnglishLoto
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Явно указываем полные имена типов
        private System.Windows.Forms.ComboBox comboBoxTheme;
        private System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Panel panelWords;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblCorrectCount;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnHint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxTheme = new System.Windows.Forms.ComboBox();
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.panelGame = new System.Windows.Forms.Panel();
            this.panelWords = new System.Windows.Forms.Panel();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblCorrectCount = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnHint = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // comboBoxTheme
            this.comboBoxTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTheme.Location = new System.Drawing.Point(20, 20);
            this.comboBoxTheme.Size = new System.Drawing.Size(200, 21);
            this.comboBoxTheme.SelectedIndexChanged += new System.EventHandler(this.comboBoxTheme_SelectedIndexChanged);

            // comboBoxLevel
            this.comboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevel.Location = new System.Drawing.Point(240, 20);
            this.comboBoxLevel.Size = new System.Drawing.Size(150, 21);

            // btnStartGame
            this.btnStartGame.Location = new System.Drawing.Point(410, 18);
            this.btnStartGame.Size = new System.Drawing.Size(100, 25);
            this.btnStartGame.Text = "Старт";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);

            // btnAdmin
            this.btnAdmin.Location = new System.Drawing.Point(520, 18);
            this.btnAdmin.Size = new System.Drawing.Size(100, 25);
            this.btnAdmin.Text = "Админ";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);

            // panelGame
            this.panelGame.Location = new System.Drawing.Point(20, 60);
            this.panelGame.Size = new System.Drawing.Size(600, 400);
            this.panelGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGame.BackColor = System.Drawing.Color.White;

            // panelWords
            this.panelWords.Location = new System.Drawing.Point(20, 470);
            this.panelWords.Size = new System.Drawing.Size(600, 120);
            this.panelWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWords.BackColor = System.Drawing.Color.LightGray;
            this.panelWords.AutoScroll = true;

            // lblScore
            this.lblScore.Location = new System.Drawing.Point(640, 60);
            this.lblScore.Size = new System.Drawing.Size(150, 25);
            this.lblScore.Text = "Счет: 0";
            this.lblScore.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            // lblTimer
            this.lblTimer.Location = new System.Drawing.Point(640, 95);
            this.lblTimer.Size = new System.Drawing.Size(150, 25);
            this.lblTimer.Text = "Время: 60 сек";

            // lblProgress
            this.lblProgress.Location = new System.Drawing.Point(640, 130);
            this.lblProgress.Size = new System.Drawing.Size(150, 25);
            this.lblProgress.Text = "Прогресс: 0%";

            // lblCorrectCount
            this.lblCorrectCount.Location = new System.Drawing.Point(640, 165);
            this.lblCorrectCount.Size = new System.Drawing.Size(150, 25);
            this.lblCorrectCount.Text = "Правильно: 0/0";

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(640, 200);
            this.progressBar.Size = new System.Drawing.Size(150, 20);

            // lblHint
            this.lblHint.Location = new System.Drawing.Point(640, 240);
            this.lblHint.Size = new System.Drawing.Size(150, 60);
            this.lblHint.Text = "Перетащите слово на картинку";
            this.lblHint.BackColor = System.Drawing.Color.LightYellow;
            this.lblHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // btnHint
            this.btnHint.Location = new System.Drawing.Point(640, 310);
            this.btnHint.Size = new System.Drawing.Size(150, 30);
            this.btnHint.Text = "Подсказка";
            this.btnHint.UseVisualStyleBackColor = true;
            this.btnHint.Click += new System.EventHandler(this.btnHint_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(820, 610);
            this.Controls.Add(this.comboBoxTheme);
            this.Controls.Add(this.comboBoxLevel);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelWords);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblCorrectCount);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnHint);
            this.Text = "English Loto - Учим английские слова";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}