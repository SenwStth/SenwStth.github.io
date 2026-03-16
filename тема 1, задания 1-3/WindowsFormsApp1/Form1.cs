using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonCalculate.Enabled = false;
        }

        private void textBoxX_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем цифры, минус, запятую/точку и backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8) // 8 = backspace
                return;

            // Разрешаем минус только в начале строки
            if (e.KeyChar == '-' && string.IsNullOrEmpty(textBoxX.Text))
                return;

            // Разрешаем запятую или точку (преобразуем точку в запятую)
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = ','; // Преобразуем точку в запятую
                // Проверяем, что запятая еще не встречалась
                if (!textBoxX.Text.Contains(","))
                    return;
            }

            // Все остальное запрещаем
            e.Handled = true;
        }

        private void textBoxX_TextChanged(object sender, EventArgs e)
        {
            buttonCalculate.Enabled = !string.IsNullOrEmpty(textBoxX.Text) &&
                                      !string.IsNullOrEmpty(textBoxEpsilon.Text);
        }

        private void textBoxEpsilon_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем цифры, запятую/точку и backspace
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
                return;

            // Разрешаем запятую или точку (преобразуем точку в запятую)
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = ','; // Преобразуем точку в запятую
                // Проверяем, что запятая еще не встречалась
                if (!textBoxEpsilon.Text.Contains(","))
                    return;
            }

            // Все остальное запрещаем
            e.Handled = true;
        }

        private void textBoxEpsilon_TextChanged(object sender, EventArgs e)
        {
            buttonCalculate.Enabled = !string.IsNullOrEmpty(textBoxX.Text) &&
                                      !string.IsNullOrEmpty(textBoxEpsilon.Text);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Заменяем точку на запятую для парсинга
                string xText = textBoxX.Text.Replace('.', ',');
                string epsilonText = textBoxEpsilon.Text.Replace('.', ',');

                double x = double.Parse(xText);
                double epsilon = double.Parse(epsilonText);

                // === ДЕМОНСТРАЦИЯ ИСКЛЮЧЕНИЯ ДЛЯ ЗАДАНИЯ 3.2 ===
                // Если x = -2, создаем тестовое исключение
                if (x == -2)
                {
                    throw new ArgumentException("ТЕСТОВОЕ ИСКЛЮЧЕНИЕ: x = -2 (демонстрация работы отладчика)");
                }
                // ==============================================

                // Проверка условий
                if (x >= -1)
                {
                    MessageBox.Show($"Ошибка: x должен быть меньше -1. Текущее значение: {x}");
                    return;
                }

                if (epsilon <= 0 || epsilon >= 1)
                {
                    MessageBox.Show($"Ошибка: точность должна быть от 0 до 1. Текущее значение: {epsilon}");
                    return;
                }

                // Точное значение через встроенную функцию
                double exactValue = Math.Atan(x);

                // Вычисление ряда
                double sum = -Math.PI / 2;
                double term = -1.0 / x;
                sum += term;
                int n = 0;
                int iterations = 1;

                // Основной цикл вычисления - здесь точка останова для задания 3.1
                while (Math.Abs(term) >= epsilon && iterations < 100000)
                {
                    n++;
                    iterations++;

                    // Вычисление следующего члена ряда через предыдущий
                    term = term * (-1.0) / (x * x) * (2.0 * n - 1.0) / (2.0 * n + 1.0);
                    sum += term;
                }

                if (iterations >= 100000)
                {
                    MessageBox.Show("Достигнуто максимальное число итераций (100000)!\n" +
                                  "Ряд сходится слишком медленно.",
                                  "Предупреждение",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                }

                // Вывод результатов
                labelResultFunc.Text = exactValue.ToString("F10");
                labelResultSeries.Text = sum.ToString("F10");
                labelCount.Text = iterations.ToString();
                labelError.Text = Math.Abs(exactValue - sum).ToString("E2");

            }
            catch (ArgumentException ex) // Ловим наше тестовое исключение
            {
                MessageBox.Show($"ПЕРЕХВАЧЕНО ТЕСТОВОЕ ИСКЛЮЧЕНИЕ:\n{ex.Message}",
                              "Демонстрация задания 3.2",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Ошибка формата числа. Используйте запятую для десятичной части.\n\nДетали: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}