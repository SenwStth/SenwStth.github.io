using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace EnglishLoto
{
    public partial class FormAddQuestion : Form
    {
        private string selectedImagePath = "";

        public string ImagePath { get; private set; }
        public string EnglishWord { get; private set; }
        public string RussianWord { get; private set; }
        public string Hint { get; private set; }

        public FormAddQuestion()
        {
            InitializeComponent();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                ofd.Title = "Выберите изображение";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    lblImagePath.Text = Path.GetFileName(selectedImagePath);

                    // Предпросмотр
                    try
                    {
                        pictureBoxPreview.Image = Image.FromFile(selectedImagePath);
                        pictureBoxPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch { }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnglish.Text))
            {
                MessageBox.Show("Введите английское слово!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtRussian.Text))
            {
                MessageBox.Show("Введите русский перевод!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Выберите изображение!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ImagePath = selectedImagePath;
            EnglishWord = txtEnglish.Text.Trim();
            RussianWord = txtRussian.Text.Trim();
            Hint = txtHint.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}