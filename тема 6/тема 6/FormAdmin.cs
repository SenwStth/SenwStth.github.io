using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace EnglishLoto
{
    public partial class FormAdmin : Form
    {
        private GameManager gameManager;
        private string xmlPath;

        public FormAdmin(GameManager manager)
        {
            InitializeComponent();
            gameManager = manager;
            xmlPath = Path.Combine(Application.StartupPath, "data", "questions.xml");
            LoadThemes();
        }

        /// <summary>
        /// Простой диалог ввода текста
        /// </summary>
        private string ShowInputDialog(string title, string prompt, string defaultValue = "")
        {
            Form dialog = new Form();
            dialog.Text = title;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.Size = new Size(300, 150);
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MaximizeBox = false;
            dialog.MinimizeBox = false;

            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(10, 20);
            label.Size = new Size(260, 20);

            TextBox textBox = new TextBox();
            textBox.Text = defaultValue;
            textBox.Location = new Point(10, 50);
            textBox.Size = new Size(260, 20);

            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new Point(150, 85);
            okButton.Size = new Size(70, 25);
            okButton.DialogResult = DialogResult.OK;

            Button cancelButton = new Button();
            cancelButton.Text = "Отмена";
            cancelButton.Location = new Point(230, 85);
            cancelButton.Size = new Size(70, 25);
            cancelButton.DialogResult = DialogResult.Cancel;

            dialog.Controls.Add(label);
            dialog.Controls.Add(textBox);
            dialog.Controls.Add(okButton);
            dialog.Controls.Add(cancelButton);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return textBox.Text;
            }
            return "";
        }

        private void LoadThemes()
        {
            listBoxThemes.Items.Clear();
            foreach (Theme theme in gameManager.Themes)
            {
                listBoxThemes.Items.Add(theme);
            }
        }

        private void listBoxThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxThemes.SelectedItem != null)
            {
                Theme theme = listBoxThemes.SelectedItem as Theme;
                txtThemeName.Text = theme.Name;
                LoadLevels(theme);
            }
        }

        private void LoadLevels(Theme theme)
        {
            listBoxLevels.Items.Clear();
            foreach (int level in theme.GetLevels())
            {
                Level levelObj = theme.GetLevel(level);
                listBoxLevels.Items.Add($"Уровень {level} ({levelObj.CardsCount} картинок)");
            }

            if (listBoxLevels.Items.Count > 0)
                listBoxLevels.SelectedIndex = 0;
        }

        private void listBoxLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxThemes.SelectedItem != null && listBoxLevels.SelectedItem != null)
            {
                Theme theme = listBoxThemes.SelectedItem as Theme;
                int levelIndex = listBoxLevels.SelectedIndex + 1;
                Level level = theme.GetLevel(levelIndex);

                if (level != null)
                {
                    LoadQuestions(level);
                }
            }
        }

        private void LoadQuestions(Level level)
        {
            listBoxQuestions.Items.Clear();
            foreach (Question q in level.Questions)
            {
                listBoxQuestions.Items.Add($"{q.EnglishWord} - {q.RussianWord}");
            }
        }

        private void btnAddTheme_Click(object sender, EventArgs e)
        {
            string themeName = ShowInputDialog("Добавление темы", "Введите название темы:", "");

            if (!string.IsNullOrEmpty(themeName))
            {
                int newId = gameManager.Themes.Count + 1;
                Theme newTheme = new Theme(themeName, newId);

                // Добавляем три уровня
                newTheme.AddLevel(1, 6, 80);
                newTheme.AddLevel(2, 9, 85);
                newTheme.AddLevel(3, 16, 90);

                gameManager.Themes.Add(newTheme);
                SaveToXML();
                LoadThemes();
            }
        }

        private void btnAddLevel_Click(object sender, EventArgs e)
        {
            if (listBoxThemes.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите тему!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string levelNum = ShowInputDialog("Добавление уровня", "Введите номер уровня (1,2,3):", "");

            if (int.TryParse(levelNum, out int levelNumber) && levelNumber >= 1 && levelNumber <= 3)
            {
                Theme theme = listBoxThemes.SelectedItem as Theme;

                if (!theme.HasLevel(levelNumber))
                {
                    int cardsCount = levelNumber == 1 ? 6 : (levelNumber == 2 ? 9 : 16);
                    int requiredScore = levelNumber == 1 ? 80 : (levelNumber == 2 ? 85 : 90);

                    theme.AddLevel(levelNumber, cardsCount, requiredScore);
                    SaveToXML();
                    LoadLevels(theme);
                }
                else
                {
                    MessageBox.Show("Уровень уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            if (listBoxThemes.SelectedItem == null || listBoxLevels.SelectedItem == null)
            {
                MessageBox.Show("Выберите тему и уровень!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormAddQuestion addForm = new FormAddQuestion();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                Theme theme = listBoxThemes.SelectedItem as Theme;
                int levelIndex = listBoxLevels.SelectedIndex + 1;

                Question newQuestion = new Question(
                    addForm.ImagePath,
                    addForm.EnglishWord,
                    addForm.RussianWord,
                    addForm.Hint
                );

                theme.AddQuestion(levelIndex, newQuestion);
                SaveToXML();

                Level level = theme.GetLevel(levelIndex);
                LoadQuestions(level);
            }
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            SaveToXML();
            MessageBox.Show("Данные сохранены!", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveToXML()
        {
            try
            {
                // Создаем папку data, если её нет
                string dataPath = Path.GetDirectoryName(xmlPath);
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                XmlDocument doc = new XmlDocument();
                XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(decl);

                XmlElement root = doc.CreateElement("game");
                doc.AppendChild(root);

                XmlElement gameinfo = doc.CreateElement("gameinfo");
                root.AppendChild(gameinfo);

                XmlElement name = doc.CreateElement("name");
                name.InnerText = "English Loto";
                gameinfo.AppendChild(name);

                XmlElement version = doc.CreateElement("version");
                version.InnerText = "1.0";
                gameinfo.AppendChild(version);

                XmlElement themes = doc.CreateElement("themes");
                root.AppendChild(themes);

                foreach (Theme theme in gameManager.Themes)
                {
                    XmlElement themeElem = doc.CreateElement("theme");
                    themeElem.SetAttribute("name", theme.Name);
                    themeElem.SetAttribute("id", theme.Id.ToString());
                    themes.AppendChild(themeElem);

                    foreach (int levelNum in theme.GetLevels())
                    {
                        Level level = theme.GetLevel(levelNum);
                        XmlElement levelElem = doc.CreateElement("level");
                        levelElem.SetAttribute("level", levelNum.ToString());
                        levelElem.SetAttribute("cards", level.CardsCount.ToString());
                        levelElem.SetAttribute("required_score", level.RequiredScore.ToString());
                        themeElem.AppendChild(levelElem);

                        foreach (Question q in level.Questions)
                        {
                            XmlElement questionElem = doc.CreateElement("question");
                            levelElem.AppendChild(questionElem);

                            XmlElement imageElem = doc.CreateElement("image");
                            imageElem.InnerText = q.ImagePath;
                            questionElem.AppendChild(imageElem);

                            XmlElement englishElem = doc.CreateElement("english");
                            englishElem.InnerText = q.EnglishWord;
                            questionElem.AppendChild(englishElem);

                            XmlElement russianElem = doc.CreateElement("russian");
                            russianElem.InnerText = q.RussianWord;
                            questionElem.AppendChild(russianElem);

                            XmlElement hintElem = doc.CreateElement("hint");
                            hintElem.InnerText = q.Hint;
                            questionElem.AppendChild(hintElem);
                        }
                    }
                }

                doc.Save(xmlPath);
                gameManager.LoadXML();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}