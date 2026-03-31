using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLoto
{
    /// <summary>
    /// Класс темы с уровнями сложности
    /// </summary>
    public class Theme
    {
        private string name;
        private int id;
        private Dictionary<int, Level> levels;

        public Theme(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.levels = new Dictionary<int, Level>();
        }

        public string Name
        {
            get { return name; }
        }

        public int Id
        {
            get { return id; }
        }

        public void AddLevel(int levelNumber, int cardsCount, int requiredScore)
        {
            if (!levels.ContainsKey(levelNumber))
            {
                levels[levelNumber] = new Level(levelNumber, cardsCount, requiredScore);
            }
        }

        public Level GetLevel(int levelNumber)
        {
            return levels.ContainsKey(levelNumber) ? levels[levelNumber] : null;
        }

        public List<int> GetLevels()
        {
            return levels.Keys.OrderBy(k => k).ToList();
        }

        public void AddQuestion(int levelNumber, Question question)
        {
            if (levels.ContainsKey(levelNumber))
            {
                levels[levelNumber].AddQuestion(question);
            }
        }

        public bool HasLevel(int levelNumber)
        {
            return levels.ContainsKey(levelNumber);
        }

        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    /// Класс уровня сложности
    /// </summary>
    public class Level
    {
        private int levelNumber;
        private int cardsCount;
        private int requiredScore;
        private List<Question> questions;

        public Level(int levelNumber, int cardsCount, int requiredScore)
        {
            this.levelNumber = levelNumber;
            this.cardsCount = cardsCount;
            this.requiredScore = requiredScore;
            this.questions = new List<Question>();
        }

        public int LevelNumber
        {
            get { return levelNumber; }
        }

        public int CardsCount
        {
            get { return cardsCount; }
        }

        public int RequiredScore
        {
            get { return requiredScore; }
        }

        public List<Question> Questions
        {
            get { return questions; }
        }

        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }

        /// <summary>
        /// Получить случайные вопросы для игры (количество = cardsCount)
        /// </summary>
        public List<Question> GetRandomQuestions()
        {
            Random rnd = new Random();
            return questions.OrderBy(x => rnd.Next()).Take(cardsCount).ToList();
        }
    }
}