using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonCalculate.Enabled = false;

            // Заполняем ComboBox системами счисления
            comboBoxBase.Items.Clear();
            foreach (int baseSystem in NumberConverter.GetAvailableBases())
            {
                comboBoxBase.Items.Add(baseSystem.ToString());
            }
            comboBoxBase.SelectedIndex = 0; // Выбираем 2-ичную систему по умолчанию

            // Подсказка для пользователя
            UpdateAllowedCharsHint();
        }

        private void UpdateAllowedCharsHint()
        {
            if (comboBoxBase.SelectedItem != null)
            {
                int baseSystem = int.Parse(comboBoxBase.SelectedItem.ToString());
                string allowedChars = NumberConverter.GetAllowedChars(baseSystem);
                labelAllowedChars.Text = $"Допустимые символы: {allowedChars}";
            }
        }

        private void comboBoxBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAllowedCharsHint();
            CheckFields();
        }

        private void textBoxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем цифры, буквы A-F (в любом регистре) и backspace
            if (char.IsControl(e.KeyChar))
                return;

            if (comboBoxBase.SelectedItem == null)
            {
                e.Handled = true;
                return;
            }

            int baseSystem = int.Parse(comboBoxBase.SelectedItem.ToString());
            string allowedChars = NumberConverter.GetAllowedChars(baseSystem);

            // Проверяем, разрешен ли введенный символ (в верхнем регистре)
            char upperChar = char.ToUpper(e.KeyChar);
            if (!allowedChars.Contains(upperChar.ToString()))
            {
                e.Handled = true;
                MessageBox.Show($"В {baseSystem}-ичной системе разрешены только символы: {allowedChars}",
                    "Недопустимый символ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void CheckFields()
        {
            bool fieldsFilled = !string.IsNullOrWhiteSpace(textBoxNumber.Text) &&
                                comboBoxBase.SelectedItem != null;

            buttonCalculate.Enabled = fieldsFilled;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                string number = textBoxNumber.Text.Trim();
                int baseSystem = int.Parse(comboBoxBase.SelectedItem.ToString());

                // Проверка на пустое число
                if (string.IsNullOrWhiteSpace(number))
                {
                    MessageBox.Show("Введите число для перевода.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверка на допустимые символы
                if (!NumberConverter.IsValidNumber(number, baseSystem))
                {
                    string allowed = NumberConverter.GetAllowedChars(baseSystem);
                    MessageBox.Show($"Число '{number}' не является допустимым в {baseSystem}-ичной системе.\n" +
                                  $"Разрешены только символы: {allowed}",
                                  "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Пытаемся перевести число
                if (NumberConverter.TryConvertToDecimal(number, baseSystem, out long decimalValue))
                {
                    // Выводим результат
                    labelResult.Text = decimalValue.ToString();

                    // Показываем подробности
                    string details = $"Исходное число: {number} ({baseSystem}-ичная)\n" +
                                    $"Десятичное значение: {decimalValue}";
                    MessageBox.Show(details, "Результат перевода",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Произошло переполнение. Число слишком большое для преобразования.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}