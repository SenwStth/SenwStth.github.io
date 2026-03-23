using System;
using System.Collections.Generic;

namespace DictionaryLibrary
{
    /// <summary>
    /// Класс для хранения результатов поиска
    /// </summary>
    public class WordSearchResult
    {
        private List<string> foundWords;
        private string searchPattern;
        private DateTime searchTime;

        public WordSearchResult()
        {
            foundWords = new List<string>();
            searchPattern = "";
            searchTime = DateTime.Now;
        }

        public WordSearchResult(string pattern)
        {
            foundWords = new List<string>();
            searchPattern = pattern;
            searchTime = DateTime.Now;
        }

        public List<string> FoundWords
        {
            get { return foundWords; }
            set { foundWords = value; }
        }

        public string SearchPattern
        {
            get { return searchPattern; }
            set { searchPattern = value; }
        }

        public DateTime SearchTime
        {
            get { return searchTime; }
            set { searchTime = value; }
        }

        public int Count
        {
            get { return foundWords.Count; }
        }

        public void AddWord(string word)
        {
            if (!foundWords.Contains(word))
            {
                foundWords.Add(word);
            }
        }

        public void AddRange(IEnumerable<string> words)
        {
            foreach (string word in words)
            {
                AddWord(word);
            }
        }

        public void Sort()
        {
            foundWords.Sort();
        }

        public override string ToString()
        {
            return $"Поиск: {searchPattern} - Найдено: {Count} слов";
        }
    }
}