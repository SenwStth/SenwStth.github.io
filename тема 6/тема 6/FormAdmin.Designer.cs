using System;
using System.Drawing;
using System.Windows.Forms;

namespace EnglishLoto
{
    partial class FormAdmin
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ListBox listBoxThemes;
        private System.Windows.Forms.ListBox listBoxLevels;
        private System.Windows.Forms.ListBox listBoxQuestions;
        private System.Windows.Forms.TextBox txtThemeName;
        private System.Windows.Forms.Button btnAddTheme;
        private System.Windows.Forms.Button btnAddLevel;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblQuestion;

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
            this.listBoxThemes = new System.Windows.Forms.ListBox();
            this.listBoxLevels = new System.Windows.Forms.ListBox();
            this.listBoxQuestions = new System.Windows.Forms.ListBox();
            this.txtThemeName = new System.Windows.Forms.TextBox();
            this.btnAddTheme = new System.Windows.Forms.Button();
            this.btnAddLevel = new System.Windows.Forms.Button();
            this.btnAddQuestion = new System.Windows.Forms.Button();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.lblTheme = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTheme
            this.lblTheme.Text = "Темы:";
            this.lblTheme.Location = new System.Drawing.Point(20, 20);
            this.lblTheme.Size = new System.Drawing.Size(50, 20);

            // listBoxThemes
            this.listBoxThemes.Location = new System.Drawing.Point(20, 45);
            this.listBoxThemes.Size = new System.Drawing.Size(200, 300);
            this.listBoxThemes.SelectedIndexChanged += new System.EventHandler(this.listBoxThemes_SelectedIndexChanged);

            // lblLevel
            this.lblLevel.Text = "Уровни:";
            this.lblLevel.Location = new System.Drawing.Point(240, 20);
            this.lblLevel.Size = new System.Drawing.Size(50, 20);

            // listBoxLevels
            this.listBoxLevels.Location = new System.Drawing.Point(240, 45);
            this.listBoxLevels.Size = new System.Drawing.Size(180, 150);
            this.listBoxLevels.SelectedIndexChanged += new System.EventHandler(this.listBoxLevels_SelectedIndexChanged);

            // lblQuestion
            this.lblQuestion.Text = "Вопросы:";
            this.lblQuestion.Location = new System.Drawing.Point(440, 20);
            this.lblQuestion.Size = new System.Drawing.Size(50, 20);

            // listBoxQuestions
            this.listBoxQuestions.Location = new System.Drawing.Point(440, 45);
            this.listBoxQuestions.Size = new System.Drawing.Size(300, 300);

            // txtThemeName
            this.txtThemeName.Location = new System.Drawing.Point(20, 360);
            this.txtThemeName.Size = new System.Drawing.Size(200, 20);
            this.txtThemeName.ReadOnly = true;

            // btnAddTheme
            this.btnAddTheme.Location = new System.Drawing.Point(20, 390);
            this.btnAddTheme.Size = new System.Drawing.Size(200, 30);
            this.btnAddTheme.Text = "Добавить тему";
            this.btnAddTheme.UseVisualStyleBackColor = true;
            this.btnAddTheme.Click += new System.EventHandler(this.btnAddTheme_Click);

            // btnAddLevel
            this.btnAddLevel.Location = new System.Drawing.Point(240, 210);
            this.btnAddLevel.Size = new System.Drawing.Size(180, 30);
            this.btnAddLevel.Text = "Добавить уровень";
            this.btnAddLevel.UseVisualStyleBackColor = true;
            this.btnAddLevel.Click += new System.EventHandler(this.btnAddLevel_Click);

            // btnAddQuestion
            this.btnAddQuestion.Location = new System.Drawing.Point(440, 360);
            this.btnAddQuestion.Size = new System.Drawing.Size(300, 30);
            this.btnAddQuestion.Text = "Добавить вопрос";
            this.btnAddQuestion.UseVisualStyleBackColor = true;
            this.btnAddQuestion.Click += new System.EventHandler(this.btnAddQuestion_Click);

            // btnSaveXML
            this.btnSaveXML.Location = new System.Drawing.Point(440, 400);
            this.btnSaveXML.Size = new System.Drawing.Size(300, 30);
            this.btnSaveXML.Text = "Сохранить в XML";
            this.btnSaveXML.BackColor = System.Drawing.Color.LightGreen;
            this.btnSaveXML.UseVisualStyleBackColor = false;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);

            // FormAdmin
            this.ClientSize = new System.Drawing.Size(770, 450);
            this.Controls.Add(this.listBoxThemes);
            this.Controls.Add(this.listBoxLevels);
            this.Controls.Add(this.listBoxQuestions);
            this.Controls.Add(this.txtThemeName);
            this.Controls.Add(this.btnAddTheme);
            this.Controls.Add(this.btnAddLevel);
            this.Controls.Add(this.btnAddQuestion);
            this.Controls.Add(this.btnSaveXML);
            this.Controls.Add(this.lblTheme);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblQuestion);
            this.Text = "Панель администратора";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}