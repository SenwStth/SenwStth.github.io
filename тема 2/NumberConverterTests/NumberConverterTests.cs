using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp1;

namespace NumberConverterTests
{
    [TestClass]
    public class NumberConverterTests
    {
        // === ПОЗИТИВНЫЕ ТЕСТЫ (корректные данные) ===

        [TestMethod]
        public void TestBinaryToDecimal()
        {
            // 2-ичная система: 1010 -> 10
            string number = "1010";
            int baseSystem = 2;
            long expected = 10;

            long actual = NumberConverter.ConvertToDecimal(number, baseSystem);

            Assert.AreEqual(expected, actual, "Неверный перевод из двоичной системы");
        }

        [TestMethod]
        public void TestOctalToDecimal()
        {
            // 8-ичная система: 17 -> 15
            string number = "17";
            int baseSystem = 8;
            long expected = 15;

            long actual = NumberConverter.ConvertToDecimal(number, baseSystem);

            Assert.AreEqual(expected, actual, "Неверный перевод из восьмеричной системы");
        }

        [TestMethod]
        public void TestDecimalToDecimal()
        {
            // 10-ичная система: 123 -> 123
            string number = "123";
            int baseSystem = 10;
            long expected = 123;

            long actual = NumberConverter.ConvertToDecimal(number, baseSystem);

            Assert.AreEqual(expected, actual, "Неверный перевод из десятичной системы");
        }

        [TestMethod]
        public void TestHexToDecimal()
        {
            // 16-ичная система: 1F -> 31
            string number = "1F";
            int baseSystem = 16;
            long expected = 31;

            long actual = NumberConverter.ConvertToDecimal(number, baseSystem);

            Assert.AreEqual(expected, actual, "Неверный перевод из шестнадцатеричной системы");
        }

        [TestMethod]
        public void TestLowerCaseHexToDecimal()
        {
            // Проверка на нижний регистр: 1f -> 31
            string number = "1f";
            int baseSystem = 16;
            long expected = 31;

            long actual = NumberConverter.ConvertToDecimal(number, baseSystem);

            Assert.AreEqual(expected, actual, "Неверная обработка нижнего регистра");
        }

        // === ТЕСТЫ НА ВАЛИДАЦИЮ ===

        [TestMethod]
        public void TestIsValidNumber_ValidBinary()
        {
            string number = "101010";
            int baseSystem = 2;

            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            Assert.IsTrue(result, "Допустимое двоичное число не прошло валидацию");
        }

        [TestMethod]
        public void TestIsValidNumber_InvalidBinary()
        {
            string number = "10201"; // 2 недопустима в двоичной
            int baseSystem = 2;

            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            Assert.IsFalse(result, "Недопустимое двоичное число прошло валидацию");
        }

        [TestMethod]
        public void TestIsValidNumber_ValidHex()
        {
            string number = "ABCDEF";
            int baseSystem = 16;

            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            Assert.IsTrue(result, "Допустимое шестнадцатеричное число не прошло валидацию");
        }

        [TestMethod]
        public void TestIsValidNumber_InvalidHex()
        {
            string number = "G123"; // G недопустима
            int baseSystem = 16;

            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            Assert.IsFalse(result, "Недопустимое шестнадцатеричное число прошло валидацию");
        }

        [TestMethod]
        public void TestIsValidNumber_EmptyString()
        {
            string number = "";
            int baseSystem = 10;

            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            Assert.IsFalse(result, "Пустая строка прошла валидацию");
        }

        [TestMethod]
        public void TestIsValidNumber_Whitespace()
        {
            string number = "   ";
            int baseSystem = 10;

            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            Assert.IsFalse(result, "Строка из пробелов прошла валидацию");
        }

        // === НЕГАТИВНЫЕ ТЕСТЫ (исключения) ===

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConvertToDecimal_InvalidBase_LessThan2()
        {
            // Основание меньше 2
            string number = "1010";
            int baseSystem = 1;

            NumberConverter.ConvertToDecimal(number, baseSystem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConvertToDecimal_InvalidBase_MoreThan16()
        {
            // Основание больше 16
            string number = "1010";
            int baseSystem = 17;

            NumberConverter.ConvertToDecimal(number, baseSystem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConvertToDecimal_EmptyString()
        {
            // Пустая строка
            string number = "";
            int baseSystem = 10;

            NumberConverter.ConvertToDecimal(number, baseSystem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConvertToDecimal_InvalidDigit()
        {
            // Недопустимая цифра для 10-ичной системы
            string number = "12A45";
            int baseSystem = 10;

            NumberConverter.ConvertToDecimal(number, baseSystem);
        }

        // === ТЕСТЫ ГРАНИЧНЫХ ЗНАЧЕНИЙ ===

        [TestMethod]
        public void TestTryConvertToDecimal_Overflow()
        {
            // Слишком большое число для long
            string number = "FFFFFFFFFFFFFFFF"; // 16 символов F
            int baseSystem = 16;

            bool result = NumberConverter.TryConvertToDecimal(number, baseSystem, out long actual);

            Assert.IsFalse(result, "Должно быть переполнение, но метод вернул true");
            Assert.AreEqual(0, actual, "При переполнении результат должен быть 0");
        }

        [TestMethod]
        public void TestTryConvertToDecimal_InvalidInput()
        {
            // Некорректный ввод
            string number = "GHIJK";
            int baseSystem = 16;

            bool result = NumberConverter.TryConvertToDecimal(number, baseSystem, out long actual);

            Assert.IsFalse(result, "Некорректный ввод должен возвращать false");
            Assert.AreEqual(0, actual, "При ошибке результат должен быть 0");
        }

        // === ТЕСТЫ ВСПОМОГАТЕЛЬНЫХ МЕТОДОВ ===

        [TestMethod]
        public void TestGetAvailableBases()
        {
            int[] bases = NumberConverter.GetAvailableBases();

            Assert.AreEqual(15, bases.Length, "Должно быть 15 систем (2-16)");
            Assert.AreEqual(2, bases[0], "Первая система должна быть 2");
            Assert.AreEqual(16, bases[14], "Последняя система должна быть 16");
        }

        [TestMethod]
        public void TestGetAllowedChars_Binary()
        {
            string chars = NumberConverter.GetAllowedChars(2);

            Assert.AreEqual("01", chars, "Для двоичной системы допустимы 0 и 1");
        }

        [TestMethod]
        public void TestGetAllowedChars_Octal()
        {
            string chars = NumberConverter.GetAllowedChars(8);

            Assert.AreEqual("01234567", chars, "Для восьмеричной системы неверный набор символов");
        }

        [TestMethod]
        public void TestGetAllowedChars_Hex()
        {
            string chars = NumberConverter.GetAllowedChars(16);

            Assert.AreEqual("0123456789ABCDEF", chars, "Для 16-ичной системы неверный набор символов");
        }

        [TestMethod]
        public void TestGetAllowedChars_InvalidBase()
        {
            string chars = NumberConverter.GetAllowedChars(20);

            Assert.AreEqual("", chars, "Для недопустимого основания должна возвращаться пустая строка");
        }
    }
}