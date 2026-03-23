using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DictionaryLibrary;

namespace DictionaryApp
{
    public partial class Form1 : Form
    {
        private DictionaryManager dictionaryManager;
        private string currentDictionaryPath;

        public Form1()
        {
            InitializeComponent();
            dictionaryManager = new DictionaryManager();
            currentDictionaryPath = "";

            // Настройка DataGridView
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridViewWords.AutoGenerateColumns = false;
            dataGridViewWords.Columns.Clear();

            DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
            colNumber.HeaderText = "№";
            colNumber.Name = "Number";
            colNumber.Width = 50;
            colNumber.ReadOnly = true;
            dataGridViewWords.Columns.Add(colNumber);

            DataGridViewTextBoxColumn colWord = new DataGridViewTextBoxColumn();
            colWord.HeaderText = "Слово";
            colWord.Name = "Word";
            colWord.Width = 200;
            colWord.DataPropertyName = "Word";
            dataGridViewWords.Columns.Add(colWord);
        }

        private void UpdateWordsList()
        {
            dataGridViewWords.Rows.Clear();

            List<string> words = dictionaryManager.GetAllWords();
            for (int i = 0; i < words.Count; i++)
            {
                dataGridViewWords.Rows.Add((i + 1).ToString(), words[i]);
            }

            labelWordCount.Text = $"Всего слов: {dictionaryManager.Count}";
        }

        // === Загрузка словаря ===
        private void btnLoadDictionary_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл словаря";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dictionaryManager.LoadDictionary(openFileDialog.FileName);
                        currentDictionaryPath = openFileDialog.FileName;
                        UpdateWordsList();

                        // Очистка результатов поиска
                        listBoxResults.Items.Clear();
                        ClearSearchInputs();

                        MessageBox.Show($"Словарь загружен. {dictionaryManager.Count} слов.",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // === Сохранение словаря ===
        private void btnSaveDictionary_Click(object sender, EventArgs e)
        {
            if (!dictionaryManager.IsModified)
            {
                MessageBox.Show("Словарь не был изменен.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Сохранить изменения в словаре?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (string.IsNullOrEmpty(currentDictionaryPath))
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                            saveDialog.DefaultExt = "txt";

                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                dictionaryManager.SaveDictionary(saveDialog.FileName);
                                currentDictionaryPath = saveDialog.FileName;
                            }
                        }
                    }
                    else
                    {
                        dictionaryManager.SaveDictionary(currentDictionaryPath);
                    }

                    MessageBox.Show("Словарь сохранен.", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // === Добавление слова ===
        private void btnAddWord_Click(object sender, EventArgs e)
        {
            string word = textBoxNewWord.Text.Trim();

            if (string.IsNullOrEmpty(word))
            {
                MessageBox.Show("Введите слово для добавления.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dictionaryManager.AddWord(word))
            {
                UpdateWordsList();
                textBoxNewWord.Clear();
                MessageBox.Show($"Слово \"{word}\" добавлено.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Слово \"{word}\" уже есть в словаре.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // === Удаление слова ===
        private void btnRemoveWord_Click(object sender, EventArgs e)
        {
            if (dataGridViewWords.SelectedRows.Count > 0)
            {
                string word = dataGridViewWords.SelectedRows[0].Cells["Word"].Value.ToString();

                DialogResult result = MessageBox.Show($"Удалить слово \"{word}\"?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (dictionaryManager.RemoveWord(word))
                    {
                        UpdateWordsList();
                        MessageBox.Show($"Слово \"{word}\" удалено.", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите слово для удаления.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // === Поиск по Варианту 14 ===
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Получение длины слова
                if (!int.TryParse(textBoxLength.Text, out int length) || length <= 0)
                {
                    MessageBox.Show("Введите корректную длину слова (положительное число).",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Сбор позиций и букв
                Dictionary<int, char> positions = new Dictionary<int, char>();

                string[] lines = textBoxPositions.Lines;
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        if (int.TryParse(parts[0], out int position) && parts[1].Length == 1)
                        {
                            // Позиции с 0 (пользователь вводит с 1)
                            positions[position - 1] = parts[1][0];
                        }
                    }
                }

                if (positions.Count == 0)
                {
                    MessageBox.Show("Введите условия поиска (позиция=буква, например: 1=а).",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Поиск
                WordSearchResult result = dictionaryManager.FindByLengthAndPositions(length, positions);

                // Отображение результатов
                listBoxResults.Items.Clear();
                listBoxResults.Items.Add($"=== Результаты поиска ===");
                listBoxResults.Items.Add($"Длина слова: {length}");
                listBoxResults.Items.Add($"Условия: ");
                foreach (var pos in positions)
                {
                    listBoxResults.Items.Add($"  Позиция {pos.Key + 1}: буква '{pos.Value}'");
                }
                listBoxResults.Items.Add($"Найдено слов: {result.Count}");
                listBoxResults.Items.Add(new string('-', 40));

                foreach (string word in result.FoundWords)
                {
                    listBoxResults.Items.Add(word);
                }

                if (result.Count == 0)
                {
                    listBoxResults.Items.Add("(Ничего не найдено)");
                }

                // Сохраняем результат для возможного сохранения в файл
                currentSearchResult = result;
                btnSaveResults.Enabled = result.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private WordSearchResult currentSearchResult;

        // === Нечеткий поиск ===
        private void btnFuzzySearch_Click(object sender, EventArgs e)
        {
            string pattern = textBoxFuzzyPattern.Text.Trim();

            if (string.IsNullOrEmpty(pattern))
            {
                MessageBox.Show("Введите слово для нечеткого поиска.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                WordSearchResult result = dictionaryManager.FuzzySearch(pattern, 3);

                listBoxResults.Items.Clear();
                listBoxResults.Items.Add($"=== Нечеткий поиск ===");
                listBoxResults.Items.Add($"Шаблон: {pattern}");
                listBoxResults.Items.Add($"Расстояние Левенштейна <= 3");
                listBoxResults.Items.Add($"Найдено слов: {result.Count}");
                listBoxResults.Items.Add(new string('-', 40));

                foreach (string word in result.FoundWords)
                {
                    listBoxResults.Items.Add(word);
                }

                if (result.Count == 0)
                {
                    listBoxResults.Items.Add("(Ничего не найдено)");
                }

                currentSearchResult = result;
                btnSaveResults.Enabled = result.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === Поиск по началу слова ===
        private void btnPrefixSearch_Click(object sender, EventArgs e)
        {
            string prefix = textBoxPrefix.Text.Trim();

            if (string.IsNullOrEmpty(prefix))
            {
                MessageBox.Show("Введите начало слова для поиска.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                WordSearchResult result = dictionaryManager.SearchByPrefix(prefix);

                listBoxResults.Items.Clear();
                listBoxResults.Items.Add($"=== Поиск по началу слова ===");
                listBoxResults.Items.Add($"Начинается с: {prefix}");
                listBoxResults.Items.Add($"Найдено слов: {result.Count}");
                listBoxResults.Items.Add(new string('-', 40));

                foreach (string word in result.FoundWords)
                {
                    listBoxResults.Items.Add(word);
                }

                if (result.Count == 0)
                {
                    listBoxResults.Items.Add("(Ничего не найдено)");
                }

                currentSearchResult = result;
                btnSaveResults.Enabled = result.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === Сохранение результатов в файл ===
        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            if (currentSearchResult == null || currentSearchResult.Count == 0)
            {
                MessageBox.Show("Нет результатов для сохранения.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                saveDialog.DefaultExt = "txt";
                saveDialog.FileName = "search_results.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dictionaryManager.SaveSearchResult(currentSearchResult, saveDialog.FileName);
                        MessageBox.Show($"Результаты сохранены в:\n{saveDialog.FileName}",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // === Создание нового словаря ===
        private void btnNewDictionary_Click(object sender, EventArgs e)
        {
            if (dictionaryManager.IsModified)
            {
                DialogResult result = MessageBox.Show("Словарь был изменен. Сохранить изменения?",
                    "Подтверждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    btnSaveDictionary_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            dictionaryManager.CreateNewDictionary();
            currentDictionaryPath = "";
            UpdateWordsList();
            listBoxResults.Items.Clear();
            ClearSearchInputs();

            MessageBox.Show("Создан новый пустой словарь.", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearSearchInputs()
        {
            textBoxLength.Clear();
            textBoxPositions.Clear();
            textBoxFuzzyPattern.Clear();
            textBoxPrefix.Clear();
        }

        // === Обновление списка при вводе ===
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = textBoxSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(filter))
            {
                UpdateWordsList();
                return;
            }

            dataGridViewWords.Rows.Clear();

            List<string> words = dictionaryManager.GetAllWords();
            int index = 1;

            foreach (string word in words)
            {
                if (word.Contains(filter))
                {
                    dataGridViewWords.Rows.Add(index.ToString(), word);
                    index++;
                }
            }

            labelWordCount.Text = $"Показано: {index - 1} из {dictionaryManager.Count}";
        }
    }
}