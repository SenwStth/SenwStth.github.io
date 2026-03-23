using System;

namespace DictionaryLibrary
{
    /// <summary>
    /// Класс для вычисления расстояния Левенштейна
    /// </summary>
    public static class LevenshteinDistance
    {
        /// <summary>
        /// Вычисление расстояния Левенштейна между двумя строками
        /// </summary>
        public static int Compute(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.IsNullOrEmpty(t) ? 0 : t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Инициализация
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            // Вычисление
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        /// <summary>
        /// Проверка, является ли расстояние Левенштейна не более maxDistance
        /// </summary>
        public static bool IsWithinDistance(string word, string pattern, int maxDistance)
        {
            return Compute(word, pattern) <= maxDistance;
        }
    }
}