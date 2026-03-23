using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace DictionaryLibrary
{
    public class DictionaryManager
    {
        private List<string> dictionary;
        private string currentFilePath;
        private bool isModified;
        private string dictionaryName;

        public DictionaryManager()
        {
            dictionary = new List<string>();
            currentFilePath = "";
            isModified = false;
            dictionaryName = "Новый словарь";
        }

        public DictionaryManager(string filePath)
        {
            dictionary = new List<string>();
            currentFilePath = filePath;
            dictionaryName = Path.GetFileNameWithoutExtension(filePath);
            LoadDictionary(filePath);
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public bool IsModified
        {
            get { return isModified; }
        }

        public string DictionaryName
        {
            get { return dictionaryName; }
            set { dictionaryName = value; }
        }

        public string CurrentFilePath
        {
            get { return currentFilePath; }
        }

        public List<string> GetAllWords()
        {
            return new List<string>(dictionary);
        }

        public List<string> GetWordsStartingWith(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return new List<string>(dictionary);

            return dictionary.Where(w => w.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Загрузка словаря из файла с автоматическим определением кодировки
        /// </summary>
        public void LoadDictionary(string filePath)
        {
            try
            {
                dictionary.Clear();

                Encoding encoding = GetFileEncoding(filePath);

                using (StreamReader reader = new StreamReader(filePath, encoding))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim().ToLower();
                        if (!string.IsNullOrEmpty(line) && !dictionary.Contains(line))
                        {
                            dictionary.Add(line);
                        }
                    }
                }

                dictionary.Sort();
                currentFilePath = filePath;
                dictionaryName = Path.GetFileNameWithoutExtension(filePath);
                isModified = false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка загрузки словаря: {ex.Message}");
            }
        }

        /// <summary>
        /// Определение кодировки файла
        /// </summary>
        private Encoding GetFileEncoding(string filePath)
        {
            // Сначала пробуем UTF-8 с BOM
            byte[] bom = new byte[4];
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Read(bom, 0, 4);
            }

            // Проверка BOM
            if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                return Encoding.UTF8;

            if (bom[0] == 0xFF && bom[1] == 0xFE)
                return Encoding.Unicode;

            if (bom[0] == 0xFE && bom[1] == 0xFF)
                return Encoding.BigEndianUnicode;

            // Для русского языка чаще всего используется Windows-1251
            try
            {
                string content1251;
                using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding(1251)))
                {
                    content1251 = reader.ReadToEnd();
                }

                // Проверяем наличие русских букв
                if (System.Text.RegularExpressions.Regex.IsMatch(content1251, @"[а-яА-Я]"))
                {
                    return Encoding.GetEncoding(1251);
                }
            }
            catch { }

            // По умолчанию UTF-8
            return Encoding.UTF8;
        }

        /// <summary>
        /// Сохранение словаря в файл в UTF-8
        /// </summary>
        public void SaveDictionary(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (string word in dictionary)
                    {
                        writer.WriteLine(word);
                    }
                }

                currentFilePath = filePath;
                dictionaryName = Path.GetFileNameWithoutExtension(filePath);
                isModified = false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка сохранения словаря: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавление слова в словарь
        /// </summary>
        public bool AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            string lowerWord = word.Trim().ToLower();

            if (dictionary.Contains(lowerWord))
                return false;

            dictionary.Add(lowerWord);
            dictionary.Sort();
            isModified = true;
            return true;
        }

        /// <summary>
        /// Удаление слова из словаря
        /// </summary>
        public bool RemoveWord(string word)
        {
            string lowerWord = word.Trim().ToLower();

            if (dictionary.Remove(lowerWord))
            {
                isModified = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Поиск слов по длине и позициям букв (Вариант 14)
        /// </summary>
        public WordSearchResult FindByLengthAndPositions(int length, Dictionary<int, char> positions)
        {
            WordSearchResult result = new WordSearchResult();

            string pattern = $"Длина: {length}, Позиции: ";
            foreach (var pos in positions)
            {
                pattern += $"{pos.Key + 1}={pos.Value} ";
            }
            result.SearchPattern = pattern;

            foreach (string word in dictionary)
            {
                if (word.Length != length)
                    continue;

                bool matches = true;

                foreach (var pos in positions)
                {
                    int position = pos.Key;
                    char expectedChar = pos.Value;

                    if (position >= word.Length || word[position] != expectedChar)
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    result.AddWord(word);
                }
            }

            result.Sort();
            return result;
        }

        /// <summary>
        /// Поиск слов по длине и позициям с несколькими вариантами букв
        /// </summary>
        public WordSearchResult FindByLengthAndMultiplePositions(int length, Dictionary<int, List<char>> positions)
        {
            WordSearchResult result = new WordSearchResult();

            foreach (string word in dictionary)
            {
                if (word.Length != length)
                    continue;

                bool matches = true;

                foreach (var pos in positions)
                {
                    int position = pos.Key;
                    List<char> allowedChars = pos.Value;

                    if (position >= word.Length || !allowedChars.Contains(word[position]))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    result.AddWord(word);
                }
            }

            result.Sort();
            return result;
        }

        /// <summary>
        /// Нечеткий поиск слов с расстоянием Левенштейна не более 3
        /// </summary>
        public WordSearchResult FuzzySearch(string pattern, int maxDistance = 3)
        {
            WordSearchResult result = new WordSearchResult(pattern);

            foreach (string word in dictionary)
            {
                if (LevenshteinDistance.IsWithinDistance(word, pattern, maxDistance))
                {
                    result.AddWord(word);
                }
            }

            result.Sort();
            return result;
        }

        /// <summary>
        /// Поиск по началу слова
        /// </summary>
        public WordSearchResult SearchByPrefix(string prefix)
        {
            WordSearchResult result = new WordSearchResult($"Начинается с: {prefix}");

            foreach (string word in dictionary)
            {
                if (word.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    result.AddWord(word);
                }
            }

            result.Sort();
            return result;
        }

        /// <summary>
        /// Сохранение результатов поиска в файл
        /// </summary>
        public void SaveSearchResult(WordSearchResult result, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine($"Результаты поиска");
                    writer.WriteLine($"Дата: {result.SearchTime}");
                    writer.WriteLine($"Условие: {result.SearchPattern}");
                    writer.WriteLine($"Найдено слов: {result.Count}");
                    writer.WriteLine(new string('-', 50));

                    for (int i = 0; i < result.FoundWords.Count; i++)
                    {
                        writer.WriteLine($"{i + 1,4}. {result.FoundWords[i]}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка сохранения результатов: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверка наличия слова в словаре
        /// </summary>
        public bool Contains(string word)
        {
            return dictionary.Contains(word.Trim().ToLower());
        }

        /// <summary>
        /// Создание нового словаря
        /// </summary>
        public void CreateNewDictionary()
        {
            dictionary.Clear();
            dictionaryName = "Новый словарь";
            currentFilePath = "";
            isModified = false;
        }
    }
}