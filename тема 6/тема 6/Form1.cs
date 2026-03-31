using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using тема_6;

namespace EnglishLoto
{
    public partial class Form1 : Form
    {
        private GameManager gameManager;
        private List<PictureBox> pictureBoxes;
        private List<Button> wordButtons;
        private Question currentDraggedQuestion;
        private Button currentDraggedButton;
        private Timer gameTimer;
        private int timeRemaining;
        private bool gameActive;

        public Form1()
        {
            InitializeComponent();
            gameManager = new GameManager();
            pictureBoxes = new List<PictureBox>();
            wordButtons = new List<Button>();
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            // Загрузка данных
            if (!gameManager.LoadXML())
            {
                MessageBox.Show("Не удалось загрузить данные игры!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadThemes();
        }

        private void LoadThemes()
        {
            comboBoxTheme.Items.Clear();
            foreach (Theme theme in gameManager.Themes)
            {
                comboBoxTheme.Items.Add(theme);
            }
            if (comboBoxTheme.Items.Count > 0)
                comboBoxTheme.SelectedIndex = 0;
        }

        private void comboBoxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Theme selectedTheme = comboBoxTheme.SelectedItem as Theme;
            if (selectedTheme != null)
            {
                LoadLevels(selectedTheme);
            }
        }

        private void LoadLevels(Theme theme)
        {
            comboBoxLevel.Items.Clear();
            foreach (int level in theme.GetLevels())
            {
                Level levelObj = theme.GetLevel(level);
                comboBoxLevel.Items.Add($"Уровень {level} ({levelObj.CardsCount} картинок)");
            }
            if (comboBoxLevel.Items.Count > 0)
                comboBoxLevel.SelectedIndex = 0;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (comboBoxTheme.SelectedItem == null || comboBoxLevel.SelectedItem == null)
            {
                MessageBox.Show("Выберите тему и уровень!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Theme theme = comboBoxTheme.SelectedItem as Theme;
            int level = comboBoxLevel.SelectedIndex + 1;

            gameManager.StartGame(theme.Id, level);
            SetupGameBoard();

            // Запуск таймера (60 секунд)
            timeRemaining = 60;
            lblTimer.Text = $"Время: {timeRemaining} сек";
            gameTimer.Start();
            gameActive = true;

            btnStartGame.Enabled = false;
            btnAdmin.Enabled = false;
            comboBoxTheme.Enabled = false;
            comboBoxLevel.Enabled = false;
        }

        private void SetupGameBoard()
        {
            // Очистка игрового поля
            foreach (var pb in pictureBoxes) pb.Dispose();
            foreach (var btn in wordButtons) btn.Dispose();
            pictureBoxes.Clear();
            wordButtons.Clear();
            panelGame.Controls.Clear();

            List<Question> questions = gameManager.CurrentQuestions;
            int cardsCount = questions.Count;

            // Определяем размер сетки
            int cols = 0, rows = 0;
            switch (cardsCount)
            {
                case 6:
                    cols = 3; rows = 2;
                    break;
                case 9:
                    cols = 3; rows = 3;
                    break;
                case 16:
                    cols = 4; rows = 4;
                    break;
            }

            int cellWidth = panelGame.Width / cols - 10;
            int cellHeight = panelGame.Height / rows - 10;

            // Создаем картинки
            List<Question> shuffledQuestions = new List<Question>(questions);
            Shuffle(shuffledQuestions);

            for (int i = 0; i < cardsCount; i++)
            {
                int row = i / cols;
                int col = i % cols;

                PictureBox pb = new PictureBox();
                pb.Image = shuffledQuestions[i].Image;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.BackColor = Color.White;
                pb.Location = new Point(col * (cellWidth + 10) + 10, row * (cellHeight + 10) + 10);
                pb.Size = new Size(cellWidth, cellHeight);
                pb.Tag = shuffledQuestions[i];
                pb.AllowDrop = true;
                pb.DragEnter += PictureBox_DragEnter;
                pb.DragDrop += PictureBox_DragDrop;

                panelGame.Controls.Add(pb);
                pictureBoxes.Add(pb);
            }

            // Создаем кнопки со словами
            List<string> words = new List<string>();
            foreach (Question q in questions)
            {
                words.Add(q.EnglishWord);
            }
            Shuffle(words);

            int buttonWidth = 120;
            int buttonHeight = 40;
            int buttonsPerRow = 4;
            int startY = panelWords.Height - 100;

            for (int i = 0; i < words.Count; i++)
            {
                Button btn = new Button();
                btn.Text = words[i];
                btn.Size = new Size(buttonWidth, buttonHeight);
                btn.BackColor = Color.LightBlue;
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                btn.Tag = words[i];
                btn.MouseDown += WordButton_MouseDown;

                int row = i / buttonsPerRow;
                int col = i % buttonsPerRow;
                btn.Location = new Point(col * (buttonWidth + 10) + 10, row * (buttonHeight + 10) + 10);

                panelWords.Controls.Add(btn);
                wordButtons.Add(btn);
            }

            // Обновляем счет
            UpdateScore();
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void WordButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && gameActive)
            {
                currentDraggedButton = btn;
                btn.DoDragDrop(btn.Text, DragDropEffects.Move);
            }
        }

        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null && gameActive)
            {
                string droppedWord = e.Data.GetData(DataFormats.Text).ToString();
                Question question = pb.Tag as Question;

                if (question != null && !string.IsNullOrEmpty(droppedWord))
                {
                    bool isCorrect = gameManager.CheckAnswer(question, droppedWord);

                    if (isCorrect)
                    {
                        pb.BackColor = Color.LightGreen;
                        pb.BorderStyle = BorderStyle.Fixed3D;

                        // Удаляем использованную кнопку
                        if (currentDraggedButton != null)
                        {
                            currentDraggedButton.Enabled = false;
                            currentDraggedButton.BackColor = Color.Gray;
                            currentDraggedButton.Text = "✓";
                        }

                        UpdateScore();

                        // Проверка окончания игры
                        if (gameManager.GetCorrectAnswersCount() == gameManager.CurrentQuestions.Count)
                        {
                            EndGame(true);
                        }
                    }
                    else
                    {
                        pb.BackColor = Color.LightCoral;
                        Timer t = new Timer();
                        t.Interval = 500;
                        t.Tick += (s, ev) => {
                            pb.BackColor = Color.White;
                            t.Stop();
                        };
                        t.Start();

                        // Показываем подсказку
                        ShowHint(question);
                    }
                }
            }
        }

        private void ShowHint(Question question)
        {
            lblHint.Text = $"Подсказка: {question.Hint} ({question.RussianWord})";
            Timer t = new Timer();
            t.Interval = 3000;
            t.Tick += (s, ev) => {
                lblHint.Text = "Перетащите слово на картинку";
                t.Stop();
            };
            t.Start();
        }

        private void UpdateScore()
        {
            lblScore.Text = $"Счет: {gameManager.CurrentScore}";
            lblProgress.Text = $"Прогресс: {gameManager.GetProgressPercent()}%";
            lblCorrectCount.Text = $"Правильно: {gameManager.GetCorrectAnswersCount()}/{gameManager.CurrentQuestions.Count}";

            progressBar.Value = gameManager.GetProgressPercent();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;
            lblTimer.Text = $"Время: {timeRemaining} сек";

            if (timeRemaining <= 0)
            {
                EndGame(false);
            }
        }

        private void EndGame(bool won)
        {
            gameTimer.Stop();
            gameActive = false;

            string message;
            if (won)
            {
                message = $"Поздравляем! Вы набрали {gameManager.CurrentScore} очков!\n";

                if (gameManager.CanGoToNextLevel())
                {
                    message += "\nВы проходите на следующий уровень!";
                    DialogResult result = MessageBox.Show(message, "Победа!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (gameManager.GoToNextLevel())
                        {
                            SetupGameBoard();
                            timeRemaining = 60;
                            gameTimer.Start();
                            gameActive = true;
                            return;
                        }
                        else
                        {
                            message += "\nЭто был последний уровень! Игра завершена!";
                        }
                    }
                }
                else
                {
                    message += $"\nВам нужно набрать {gameManager.GetCurrentLevel().RequiredScore}% для перехода на следующий уровень.";
                }
            }
            else
            {
                message = "Время вышло! Игра окончена.";
            }

            MessageBox.Show(message, won ? "Победа!" : "Время вышло",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Возврат в главное меню
            btnStartGame.Enabled = true;
            btnAdmin.Enabled = true;
            comboBoxTheme.Enabled = true;
            comboBoxLevel.Enabled = true;
            panelGame.Controls.Clear();
            panelWords.Controls.Clear();
            pictureBoxes.Clear();
            wordButtons.Clear();
            lblHint.Text = "Выберите тему и уровень, нажмите Старт";
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (gameActive)
            {
                DialogResult result = MessageBox.Show("Игра активна! Выйти в меню администратора?",
                    "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;

                gameTimer.Stop();
                gameActive = false;
            }

            FormAdmin adminForm = new FormAdmin(gameManager);
            adminForm.ShowDialog();

            // Перезагружаем данные после закрытия админки
            gameManager.LoadXML();
            LoadThemes();
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            if (!gameActive)
            {
                MessageBox.Show("Сначала начните игру!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Показываем подсказку для любого неотвеченного вопроса
            foreach (PictureBox pb in pictureBoxes)
            {
                if (pb.BackColor != Color.LightGreen)
                {
                    Question q = pb.Tag as Question;
                    if (q != null)
                    {
                        ShowHint(q);
                        return;
                    }
                }
            }

            MessageBox.Show("Все вопросы уже решены!", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}