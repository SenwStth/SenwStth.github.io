using System;
using System.Collections.Generic; 
using System.IO;
using System.Linq;  

namespace WindowsFormsApp1
{
    /// <summary>
    /// Класс для управления списком участников
    /// </summary>
    public class RunnersList
    {
        private List<Runner> runners;
        private string competitionName;  // Название соревнования
        private double commonDistance;    // Общая дистанция

        /// <summary>
        /// Конструктор
        /// </summary>
        public RunnersList()
        {
            runners = new List<Runner>();
            competitionName = "";
            commonDistance = 0;
        }

        /// <summary>
        /// Конструктор с загрузкой из файла
        /// </summary>
        public RunnersList(string fileName)
        {
            runners = new List<Runner>();
            LoadFromFile(fileName);
        }

        // Свойства
        public string CompetitionName
        {
            get { return competitionName; }
            set { competitionName = value; }
        }

        public double CommonDistance
        {
            get { return commonDistance; }
            set { commonDistance = value; }
        }

        public int Count
        {
            get { return runners.Count; }
        }

        /// <summary>
        /// Индексатор для доступа к участникам
        /// </summary>
        public Runner this[int index]
        {
            get
            {
                if (index >= 0 && index < runners.Count)
                    return runners[index];
                else
                    return null;
            }
        }

        /// <summary>
        /// Добавление участника
        /// </summary>
        public void Add(Runner runner)
        {
            runners.Add(runner);
        }

        /// <summary>
        /// Добавление участника с параметрами
        /// </summary>
        public void Add(string name, double distance, double time)
        {
            runners.Add(new Runner(name, distance, time));
        }

        /// <summary>
        /// Удаление участника
        /// </summary>
        public bool Remove(int index)
        {
            if (index >= 0 && index < runners.Count)
            {
                runners.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Очистка списка
        /// </summary>
        public void Clear()
        {
            runners.Clear();
        }

        /// <summary>
        /// Получение всех участников
        /// </summary>
        public List<Runner> GetAllRunners()
        {
            return new List<Runner>(runners);
        }

        /// <summary>
        /// Поиск участников с лучшей скоростью (топ N)
        /// </summary>
        public List<Runner> FindBestRunners(int topCount = 3)
        {
            return runners.OrderByDescending(r => r.Speed).Take(topCount).ToList();
        }

        /// <summary>
        /// Получение максимальной скорости (для диаграммы)
        /// </summary>
        public double GetMaxSpeed()
        {
            if (runners.Count == 0) return 0;
            return runners.Max(r => r.Speed);
        }

        /// <summary>
        /// Сохранение в файл
        /// </summary>
        public void SaveToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                // Первая строка: название соревнования
                writer.WriteLine(competitionName);

                // Вторая строка: общая дистанция и количество участников
                writer.WriteLine($"{commonDistance};{runners.Count}");

                // Остальные строки: данные участников
                foreach (Runner runner in runners)
                {
                    writer.WriteLine(runner.ToFileString());
                }
            }
        }

        /// <summary>
        /// Загрузка из файла
        /// </summary>
        public void LoadFromFile(string fileName)
        {
            runners.Clear();

            using (StreamReader reader = new StreamReader(fileName))
            {
                // Читаем название соревнования
                competitionName = reader.ReadLine();

                // Читаем дистанцию и количество
                string secondLine = reader.ReadLine();
                string[] parts = secondLine.Split(';');

                if (parts.Length == 2)
                {
                    commonDistance = double.Parse(parts[0]);
                    int count = int.Parse(parts[1]);

                    // Читаем участников
                    for (int i = 0; i < count; i++)
                    {
                        string line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            Runner runner = Runner.FromFileString(line);
                            runners.Add(runner);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверка формата файла
        /// </summary>
        public static bool IsValidFileFormat(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    // Проверяем, что есть хотя бы 2 строки
                    if (reader.ReadLine() == null) return false;
                    if (reader.ReadLine() == null) return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}