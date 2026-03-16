using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Класс для перевода чисел из различных систем счисления в десятичную
    /// Вариант 14: перевод из n-ой системы (2-16) в десятичную
    /// </summary>
    public static class NumberConverter
    {
        /// <summary>
        /// Словарь соответствия символов и их значений в системах счисления до 16
        /// </summary>
        private static readonly Dictionary<char, int> CharToValue = new Dictionary<char, int>
        {
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
            {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
            {'A', 10}, {'B', 11}, {'C', 12}, {'D', 13}, {'E', 14}, {'F', 15},
            {'a', 10}, {'b', 11}, {'c', 12}, {'d', 13}, {'e', 14}, {'f', 15}
        };

        /// <summary>
        /// Проверяет, является ли строка допустимым числом в заданной системе счисления
        /// </summary>
        /// <param name="number">Строка с числом</param>
        /// <param name="baseSystem">Основание системы счисления (2-16)</param>
        /// <returns>true, если число корректно, иначе false</returns>
        public static bool IsValidNumber(string number, int baseSystem)
        {
            if (string.IsNullOrWhiteSpace(number))
                return false;

            if (baseSystem < 2 || baseSystem > 16)
                return false;

            string upperNumber = number.ToUpper().Trim();

            foreach (char c in upperNumber)
            {
                // Проверяем, есть ли символ в словаре
                if (!CharToValue.ContainsKey(c))
                    return false;

                // Проверяем, не превышает ли значение допустимое для данной системы
                if (CharToValue[c] >= baseSystem)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Переводит число из заданной системы счисления в десятичную
        /// </summary>
        /// <param name="number">Строка с числом в исходной системе</param>
        /// <param name="baseSystem">Основание исходной системы счисления (2-16)</param>
        /// <returns>Десятичное представление числа</returns>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных входных данных</exception>
        public static long ConvertToDecimal(string number, int baseSystem)
        {
            // Проверка входных данных
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Число не может быть пустым.");

            if (baseSystem < 2 || baseSystem > 16)
                throw new ArgumentException("Основание системы счисления должно быть от 2 до 16.");

            if (!IsValidNumber(number, baseSystem))
                throw new ArgumentException($"Число '{number}' не является допустимым в {baseSystem}-ичной системе счисления.");

            // Приводим к верхнему регистру и убираем пробелы
            string upperNumber = number.ToUpper().Trim();
            long result = 0;
            int power = 0;

            // Проходим по цифрам числа справа налево
            for (int i = upperNumber.Length - 1; i >= 0; i--)
            {
                char digit = upperNumber[i];
                int value = CharToValue[digit];

                // Вычисляем: result += value * (baseSystem ^ power)
                result += value * (long)Math.Pow(baseSystem, power);
                power++;
            }

            return result;
        }

        /// <summary>
        /// Перегруженный метод для перевода с проверкой на переполнение
        /// </summary>
        /// <param name="number">Строка с числом</param>
        /// <param name="baseSystem">Основание системы</param>
        /// <param name="result">Результат перевода</param>
        /// <returns>true если перевод успешен, false при переполнении</returns>
        public static bool TryConvertToDecimal(string number, int baseSystem, out long result)
        {
            result = 0;
            try
            {
                // Проверяем валидность числа
                if (!IsValidNumber(number, baseSystem))
                    return false;

                string upperNumber = number.ToUpper().Trim();
                long temp = 0;
                int power = 0;

                for (int i = upperNumber.Length - 1; i >= 0; i--)
                {
                    char digit = upperNumber[i];
                    int value = CharToValue[digit];

                    // Проверка на переполнение
                    checked
                    {
                        temp += value * (long)Math.Pow(baseSystem, power);
                    }
                    power++;
                }

                result = temp;
                return true;
            }
            catch (OverflowException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Получает список допустимых систем счисления
        /// </summary>
        public static int[] GetAvailableBases()
        {
            return Enumerable.Range(2, 15).ToArray(); // 2..16
        }

        /// <summary>
        /// Получает строку с примерами допустимых цифр для системы
        /// </summary>
        public static string GetAllowedChars(int baseSystem)
        {
            if (baseSystem < 2 || baseSystem > 16)
                return "";

            string chars = "0123456789ABCDEF";
            return chars.Substring(0, baseSystem);
        }
    }
}