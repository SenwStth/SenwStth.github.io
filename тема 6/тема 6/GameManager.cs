using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace EnglishLoto
{
    /// <summary>
    /// Класс управления игрой - загрузка XML, хранение данных
    /// </summary>
    public class GameManager
    {
        private List<Theme> themes;
        private string xmlFilePath;
        private int currentThemeId;
        private int currentLevel;
        private int currentScore;
        private List<Question> currentQuestions;
        private Dictionary<Question, string> playerAnswers;
        private Random random;

        public GameManager()
        {
            themes = new List<Theme>();
            currentQuestions = new List<Question>();
            playerAnswers = new Dictionary<Question, string>();
            random = new Random();
            string basePath = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", ".."));
            xmlFilePath = Path.Combine(basePath, "data", "questions.xml");
        }

        public List<Theme> Themes
        {
            get { return themes; }
        }

        public int CurrentThemeId
        {
            get { return currentThemeId; }
            set { currentThemeId = value; }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        public int CurrentScore
        {
            get { return currentScore; }
            set { currentScore = value; }
        }

        public List<Question> CurrentQuestions
        {
            get { return currentQuestions; }
        }

        /// <summary>
        /// Загрузка данных из XML-файла
        /// </summary>
        public bool LoadXML()
        {
            if (!File.Exists(xmlFilePath))
            {
                MessageBox.Show($"Файл {xmlFilePath} не найден!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                themes.Clear();
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);

                XmlNodeList themeNodes = doc.SelectNodes("/game/themes/theme");

                foreach (XmlNode themeNode in themeNodes)
                {
                    string themeName = themeNode.Attributes["name"]?.Value;
                    int themeId = int.Parse(themeNode.Attributes["id"]?.Value ?? "0");

                    Theme theme = new Theme(themeName, themeId);

                    // Загрузка уровней
                    XmlNodeList levelNodes = themeNode.SelectNodes("level");
                    foreach (XmlNode levelNode in levelNodes)
                    {
                        int levelNumber = int.Parse(levelNode.Attributes["level"]?.Value ?? "1");
                        int cardsCount = int.Parse(levelNode.Attributes["cards"]?.Value ?? "6");
                        int requiredScore = int.Parse(levelNode.Attributes["required_score"]?.Value ?? "80");

                        theme.AddLevel(levelNumber, cardsCount, requiredScore);

                        // Загрузка вопросов
                        XmlNodeList questionNodes = levelNode.SelectNodes("question");
                        foreach (XmlNode questionNode in questionNodes)
                        {
                            string imagePath = questionNode.SelectSingleNode("image")?.InnerText ?? "";
                            string english = questionNode.SelectSingleNode("english")?.InnerText ?? "";
                            string russian = questionNode.SelectSingleNode("russian")?.InnerText ?? "";
                            string hint = questionNode.SelectSingleNode("hint")?.InnerText ?? "";

                            Question question = new Question(imagePath, english, russian, hint);
                            theme.AddQuestion(levelNumber, question);
                        }
                    }

                    themes.Add(theme);
                }

                return themes.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки XML: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Получить тему по ID
        /// </summary>
        public Theme GetTheme(int id)
        {
            return themes.Find(t => t.Id == id);
        }

        /// <summary>
        /// Получить текущую тему
        /// </summary>
        public Theme GetCurrentTheme()
        {
            return GetTheme(currentThemeId);
        }

        /// <summary>
        /// Получить текущий уровень
        /// </summary>
        public Level GetCurrentLevel()
        {
            Theme theme = GetCurrentTheme();
            return theme?.GetLevel(currentLevel);
        }

        /// <summary>
        /// Начать новую игру
        /// </summary>
        public void StartGame(int themeId, int level)
        {
            currentThemeId = themeId;
            currentLevel = level;
            currentScore = 0;
            playerAnswers.Clear();

            Level currentLevelObj = GetCurrentLevel();
            if (currentLevelObj != null)
            {
                currentQuestions = currentLevelObj.GetRandomQuestions();
                // Перемешиваем вопросы
                currentQuestions = new List<Question>(currentQuestions.OrderBy(x => random.Next()));
            }
        }

        /// <summary>
        /// Проверить ответ
        /// </summary>
        public bool CheckAnswer(Question question, string answer)
        {
            bool isCorrect = question.EnglishWord.Equals(answer, StringComparison.OrdinalIgnoreCase);
            if (isCorrect)
            {
                currentScore += 10;
            }
            playerAnswers[question] = answer;
            return isCorrect;
        }

        /// <summary>
        /// Проверить, можно ли перейти на следующий уровень
        /// </summary>
        public bool CanGoToNextLevel()
        {
            Level currentLevelObj = GetCurrentLevel();
            if (currentLevelObj == null) return false;

            int maxScore = currentQuestions.Count * 10;
            int requiredPoints = (int)(maxScore * currentLevelObj.RequiredScore / 100.0);

            return currentScore >= requiredPoints;
        }

        /// <summary>
        /// Перейти на следующий уровень
        /// </summary>
        public bool GoToNextLevel()
        {
            int nextLevel = currentLevel + 1;
            Theme theme = GetCurrentTheme();
            if (theme.HasLevel(nextLevel))
            {
                currentLevel = nextLevel;
                StartGame(currentThemeId, currentLevel);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Получить прогресс в процентах
        /// </summary>
        public int GetProgressPercent()
        {
            if (currentQuestions.Count == 0) return 0;
            int maxScore = currentQuestions.Count * 10;
            return (int)((double)currentScore / maxScore * 100);
        }

        /// <summary>
        /// Получить количество правильных ответов
        /// </summary>
        public int GetCorrectAnswersCount()
        {
            int correct = 0;
            foreach (var pair in playerAnswers)
            {
                if (pair.Key.EnglishWord.Equals(pair.Value, StringComparison.OrdinalIgnoreCase))
                {
                    correct++;
                }
            }
            return correct;
        }
    }
}