using System;
using System.Drawing;
using System.Windows.Forms;

namespace EnglishLoto
{
    partial class FormAddQuestion
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtEnglish;
        private System.Windows.Forms.TextBox txtRussian;
        private System.Windows.Forms.TextBox txtHint;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblEnglish;
        private System.Windows.Forms.Label lblRussian;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.Label lblImagePath;
        private System.Windows.Forms.PictureBox pictureBoxPreview;

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
            this.txtEnglish = new System.Windows.Forms.TextBox();
            this.txtRussian = new System.Windows.Forms.TextBox();
            this.txtHint = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblEnglish = new System.Windows.Forms.Label();
            this.lblRussian = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblImage = new System.Windows.Forms.Label();
            this.lblImagePath = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();

            // lblEnglish
            this.lblEnglish.Text = "Английское слово:";
            this.lblEnglish.Location = new System.Drawing.Point(20, 20);
            this.lblEnglish.Size = new System.Drawing.Size(120, 20);

            // txtEnglish
            this.txtEnglish.Location = new System.Drawing.Point(150, 17);
            this.txtEnglish.Size = new System.Drawing.Size(200, 20);

            // lblRussian
            this.lblRussian.Text = "Русский перевод:";
            this.lblRussian.Location = new System.Drawing.Point(20, 55);
            this.lblRussian.Size = new System.Drawing.Size(120, 20);

            // txtRussian
            this.txtRussian.Location = new System.Drawing.Point(150, 52);
            this.txtRussian.Size = new System.Drawing.Size(200, 20);

            // lblHint
            this.lblHint.Text = "Подсказка:";
            this.lblHint.Location = new System.Drawing.Point(20, 90);
            this.lblHint.Size = new System.Drawing.Size(120, 20);

            // txtHint
            this.txtHint.Location = new System.Drawing.Point(150, 87);
            this.txtHint.Size = new System.Drawing.Size(200, 60);
            this.txtHint.Multiline = true;

            // lblImage
            this.lblImage.Text = "Изображение:";
            this.lblImage.Location = new System.Drawing.Point(20, 165);
            this.lblImage.Size = new System.Drawing.Size(120, 20);

            // btnSelectImage
            this.btnSelectImage.Location = new System.Drawing.Point(150, 162);
            this.btnSelectImage.Size = new System.Drawing.Size(100, 25);
            this.btnSelectImage.Text = "Выбрать";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);

            // lblImagePath
            this.lblImagePath.Location = new System.Drawing.Point(260, 165);
            this.lblImagePath.Size = new System.Drawing.Size(200, 20);
            this.lblImagePath.Text = "";

            // pictureBoxPreview
            this.pictureBoxPreview.Location = new System.Drawing.Point(150, 200);
            this.pictureBoxPreview.Size = new System.Drawing.Size(200, 150);
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // btnOK
            this.btnOK.Location = new System.Drawing.Point(100, 370);
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(220, 370);
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // FormAddQuestion
            this.ClientSize = new System.Drawing.Size(500, 420);
            this.Controls.Add(this.txtEnglish);
            this.Controls.Add(this.txtRussian);
            this.Controls.Add(this.txtHint);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblEnglish);
            this.Controls.Add(this.lblRussian);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.lblImagePath);
            this.Controls.Add(this.pictureBoxPreview);
            this.Text = "Добавление вопроса";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}