using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;  

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private RunnersList runnersList;
        private string currentFileName = "";

        public Form1()
        {
            InitializeComponent();

            // Инициализация списка
            runnersList = new RunnersList();

            // Настройка DataGridView
            SetupDataGridView();

            // Настройка Chart
            SetupChart();

            // Обновление списка
            UpdateRunnersList();
        }

        /// <summary>
        /// Настройка таблицы
        /// </summary>
        private void SetupDataGridView()
        {
            dataGridViewRunners.AutoGenerateColumns = false;
            dataGridViewRunners.Columns.Clear();

            // Колонка с номером
            DataGridViewTextBoxColumn colNumber = new DataGridViewTextBoxColumn();
            colNumber.HeaderText = "№";
            colNumber.Name = "Number";
            colNumber.Width = 40;
            colNumber.ReadOnly = true;
            dataGridViewRunners.Columns.Add(colNumber);

            // Колонка с фамилией
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "Фамилия";
            colName.Name = "Name";
            colName.Width = 150;
            colName.DataPropertyName = "Name";
            dataGridViewRunners.Columns.Add(colName);

            // Колонка с дистанцией
            DataGridViewTextBoxColumn colDistance = new DataGridViewTextBoxColumn();
            colDistance.HeaderText = "Дистанция (м)";
            colDistance.Name = "Distance";
            colDistance.Width = 100;
            colDistance.DataPropertyName = "Distance";
            dataGridViewRunners.Columns.Add(colDistance);

            // Колонка со временем
            DataGridViewTextBoxColumn colTime = new DataGridViewTextBoxColumn();
            colTime.HeaderText = "Время (сек)";
            colTime.Name = "Time";
            colTime.Width = 100;
            colTime.DataPropertyName = "Time";
            dataGridViewRunners.Columns.Add(colTime);

            // Колонка со скоростью
            DataGridViewTextBoxColumn colSpeed = new DataGridViewTextBoxColumn();
            colSpeed.HeaderText = "Скорость (м/с)";
            colSpeed.Name = "Speed";
            colSpeed.Width = 100;
            colSpeed.DataPropertyName = "Speed";
            colSpeed.DefaultCellStyle.Format = "F2";
            colSpeed.ReadOnly = true;
            dataGridViewRunners.Columns.Add(colSpeed);
        }

        /// <summary>
        /// Настройка диаграммы
        /// </summary>
        private void SetupChart()
        {
            chartSpeed.ChartAreas.Clear();
            chartSpeed.Series.Clear();
            chartSpeed.Titles.Clear();

            // Создание области диаграммы
            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Участники";
            chartArea.AxisY.Title = "Скорость (м/с)";
            chartArea.AxisY.Minimum = 0;
            chartSpeed.ChartAreas.Add(chartArea);

            // Создание серии (столбчатая диаграмма)
            Series series = new Series("SpeedSeries");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "F2";
            series.Color = Color.SteelBlue;
            chartSpeed.Series.Add(series);

            // Заголовок
            Title title = new Title("Сравнение скоростей участников");
            title.Font = new Font("Arial", 12, FontStyle.Bold);
            chartSpeed.Titles.Add(title);
        }

        /// <summary>
        /// Обновление списка в DataGridView
        /// </summary>
        private void UpdateRunnersList()
        {
            dataGridViewRunners.Rows.Clear();

            for (int i = 0; i < runnersList.Count; i++)
            {
                Runner runner = runnersList[i];
                dataGridViewRunners.Rows.Add(
                    (i + 1).ToString(),
                    runner.Name,
                    runner.Distance,
                    runner.Time,
                    runner.Speed
                );
            }

            // Обновление информации
            labelTotalCount.Text = $"Всего участников: {runnersList.Count}";

            // Обновление диаграммы
            UpdateChart();
        }

        /// <summary>
        /// Обновление диаграммы
        /// </summary>
        private void UpdateChart()
        {
            Series series = chartSpeed.Series["SpeedSeries"];
            series.Points.Clear();

            List<Runner> runners = runnersList.GetAllRunners();

            // Сортируем по скорости для лучшего отображения
            runners.Sort((a, b) => b.Speed.CompareTo(a.Speed));

            foreach (Runner runner in runners)
            {
                int pointIndex = series.Points.AddXY(runner.Name, runner.Speed);

                // Раскраска: лучший - зеленый, остальные - синие
                if (pointIndex == 0) // Самый быстрый
                {
                    series.Points[pointIndex].Color = Color.Green;
                }
                else
                {
                    series.Points[pointIndex].Color = Color.SteelBlue;
                }
            }

            // Обновление лучших результатов
            UpdateBestResults();
        }

        /// <summary>
        /// Обновление информации о лучших
        /// </summary>
        private void UpdateBestResults()
        {
            listBoxBestResults.Items.Clear();

            List<Runner> bestRunners = runnersList.FindBestRunners(3);

            if (bestRunners.Count > 0)
            {
                listBoxBestResults.Items.Add("🏆 ЛУЧШИЕ РЕЗУЛЬТАТЫ:");
                listBoxBestResults.Items.Add("");

                for (int i = 0; i < bestRunners.Count; i++)
                {
                    Runner runner = bestRunners[i];
                    string medal = i == 0 ? "🥇" : (i == 1 ? "🥈" : "🥉");
                    listBoxBestResults.Items.Add($"{medal} {runner.Name}: {runner.Speed:F2} м/с");
                    listBoxBestResults.Items.Add($"   Дистанция: {runner.Distance} м, Время: {runner.Time} сек");
                    listBoxBestResults.Items.Add("");
                }
            }
        }

        /// <summary>
        /// Добавление участника
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка ввода
                if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    MessageBox.Show("Введите фамилию участника", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(textBoxDistance.Text, out double distance) || distance <= 0)
                {
                    MessageBox.Show("Введите корректную дистанцию (положительное число)", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(textBoxTime.Text, out double time) || time <= 0)
                {
                    MessageBox.Show("Введите корректное время (положительное число)", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Добавление участника
                runnersList.Add(textBoxName.Text.Trim(), distance, time);

                // Обновление списка
                UpdateRunnersList();

                // Очистка полей
                textBoxName.Clear();
                textBoxDistance.Clear();
                textBoxTime.Clear();

                MessageBox.Show("Участник успешно добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаление выбранного участника
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewRunners.SelectedRows.Count > 0)
            {
                int index = dataGridViewRunners.SelectedRows[0].Index;

                DialogResult result = MessageBox.Show(
                    $"Удалить участника {runnersList[index]?.Name}?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    runnersList.Remove(index);
                    UpdateRunnersList();
                }
            }
            else
            {
                MessageBox.Show("Выберите участника для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Очистка списка
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (runnersList.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Очистить список участников?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    runnersList.Clear();
                    UpdateRunnersList();
                }
            }
        }

        /// <summary>
        /// Сохранение в файл
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (runnersList.Count == 0)
            {
                MessageBox.Show("Нет данных для сохранения", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = "runners.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Если название соревнования не задано, используем имя файла
                    if (string.IsNullOrWhiteSpace(runnersList.CompetitionName))
                    {
                        runnersList.CompetitionName =
                            System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                    }

                    runnersList.SaveToFile(saveFileDialog.FileName);
                    currentFileName = saveFileDialog.FileName;

                    MessageBox.Show($"Данные сохранены в файл:\n{currentFileName}", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Загрузка из файла
        /// </summary>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.DefaultExt = "txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Проверка формата файла
                    if (!RunnersList.IsValidFileFormat(openFileDialog.FileName))
                    {
                        MessageBox.Show("Неверный формат файла", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    runnersList.LoadFromFile(openFileDialog.FileName);
                    currentFileName = openFileDialog.FileName;

                    // Отображение названия соревнования
                    textBoxCompetition.Text = runnersList.CompetitionName;

                    UpdateRunnersList();

                    MessageBox.Show($"Данные загружены из файла:\n{currentFileName}", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обновление названия соревнования
        /// </summary>
        private void textBoxCompetition_TextChanged(object sender, EventArgs e)
        {
            runnersList.CompetitionName = textBoxCompetition.Text.Trim();
        }

        /// <summary>
        /// Поиск лучших
        /// </summary>
        private void btnFindBest_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2; // Переключаемся на вкладку с диаграммой
            UpdateBestResults();
        }

        /// <summary>
        /// Обновление при изменении вкладки
        /// </summary>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2) // Вкладка диаграммы
            {
                UpdateChart();
            }
        }
    }
}