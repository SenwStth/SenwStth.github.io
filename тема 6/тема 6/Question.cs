using System;
using System.Drawing;
using System.Windows.Forms;

namespace EnglishLoto
{
    /// <summary>
    /// Класс вопроса (картинка-слово)
    /// </summary>
    public class Question
    {
        private string imagePath;
        private string englishWord;
        private string russianWord;
        private string hint;
        private Image image;

        public Question(string imagePath, string englishWord, string russianWord, string hint)
        {
            this.imagePath = imagePath;
            this.englishWord = englishWord;
            this.russianWord = russianWord;
            this.hint = hint;
            LoadImage();
        }

        private void LoadImage()
        {
            try
            {
                string basePath = System.IO.Path.GetFullPath(
                    System.IO.Path.Combine(Application.StartupPath, "..", "..", ".."));
                string fullPath = System.IO.Path.Combine(basePath, "images", imagePath);
                
                if (System.IO.File.Exists(fullPath))
                {
                    image = Image.FromFile(fullPath);
                }
            }
            catch
            {
                image = null;
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
        }

        public string EnglishWord
        {
            get { return englishWord; }
        }

        public string RussianWord
        {
            get { return russianWord; }
        }

        public string Hint
        {
            get { return hint; }
        }

        public Image Image
        {
            get { return image; }
        }

        public override string ToString()
        {
            return $"{englishWord} - {russianWord}";
        }
    }
}