using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        
        // Элементы управления
        private TabControl tabControl1;
        private TabPage tabPageInput;
        private TabPage tabPageTable;
        private TabPage tabPageChart;
        
        // Вкладка 1: Ввод данных
        private Label labelName;
        private TextBox textBoxName;
        private Label labelDistance;
        private TextBox textBoxDistance;
        private Label labelTime;
        private TextBox textBoxTime;
        private Button btnAdd;
        private GroupBox groupBoxCompetition;
        private Label labelCompetition;
        private TextBox textBoxCompetition;
        
        // Вкладка 2: Таблица
        private DataGridView dataGridViewRunners;
        private Button btnRemove;
        private Button btnClear;
        private Button btnSave;
        private Button btnLoad;
        private Button btnFindBest;
        private Label labelTotalCount;
        private ListBox listBoxBestResults;
        
        // Вкладка 3: Диаграмма
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSpeed;
        
        // Диалоги
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Создание диалогов
            this.saveFileDialog = new SaveFileDialog();
            this.openFileDialog = new OpenFileDialog();
            
            // Создание TabControl
            this.tabControl1 = new TabControl();
            this.tabPageInput = new TabPage();
            this.tabPageTable = new TabPage();
            this.tabPageChart = new TabPage();
            
            // === Вкладка 1: Ввод данных ===
            this.groupBoxCompetition = new GroupBox();
            this.labelCompetition = new Label();
            this.textBoxCompetition = new TextBox();
            
            this.labelName = new Label();
            this.textBoxName = new TextBox();
            this.labelDistance = new Label();
            this.textBoxDistance = new TextBox();
            this.labelTime = new Label();
            this.textBoxTime = new TextBox();
            this.btnAdd = new Button();
            
            // groupBoxCompetition
            this.groupBoxCompetition.Controls.Add(this.labelCompetition);
            this.groupBoxCompetition.Controls.Add(this.textBoxCompetition);
            this.groupBoxCompetition.Location = new System.Drawing.Point(20, 20);
            this.groupBoxCompetition.Size = new System.Drawing.Size(400, 70);
            this.groupBoxCompetition.Text = "Соревнование";
            
            this.labelCompetition.Text = "Название:";
            this.labelCompetition.Location = new System.Drawing.Point(10, 30);
            this.labelCompetition.Size = new System.Drawing.Size(80, 20);
            
            this.textBoxCompetition.Location = new System.Drawing.Point(100, 27);
            this.textBoxCompetition.Size = new System.Drawing.Size(280, 20);
            this.textBoxCompetition.TextChanged += new System.EventHandler(this.textBoxCompetition_TextChanged);
            
            // labelName
            this.labelName.Text = "Фамилия:";
            this.labelName.Location = new System.Drawing.Point(20, 110);
            this.labelName.Size = new System.Drawing.Size(80, 20);
            
            this.textBoxName.Location = new System.Drawing.Point(100, 107);
            this.textBoxName.Size = new System.Drawing.Size(200, 20);
            
            // labelDistance
            this.labelDistance.Text = "Дистанция (м):";
            this.labelDistance.Location = new System.Drawing.Point(20, 150);
            this.labelDistance.Size = new System.Drawing.Size(80, 20);
            
            this.textBoxDistance.Location = new System.Drawing.Point(100, 147);
            this.textBoxDistance.Size = new System.Drawing.Size(200, 20);
            
            // labelTime
            this.labelTime.Text = "Время (сек):";
            this.labelTime.Location = new System.Drawing.Point(20, 190);
            this.labelTime.Size = new System.Drawing.Size(80, 20);
            
            this.textBoxTime.Location = new System.Drawing.Point(100, 187);
            this.textBoxTime.Size = new System.Drawing.Size(200, 20);
            
            // btnAdd
            this.btnAdd.Text = "Добавить участника";
            this.btnAdd.Location = new System.Drawing.Point(100, 230);
            this.btnAdd.Size = new System.Drawing.Size(200, 35);
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            
            // Добавление элементов на вкладку 1
            this.tabPageInput.Controls.Add(this.groupBoxCompetition);
            this.tabPageInput.Controls.Add(this.labelName);
            this.tabPageInput.Controls.Add(this.textBoxName);
            this.tabPageInput.Controls.Add(this.labelDistance);
            this.tabPageInput.Controls.Add(this.textBoxDistance);
            this.tabPageInput.Controls.Add(this.labelTime);
            this.tabPageInput.Controls.Add(this.textBoxTime);
            this.tabPageInput.Controls.Add(this.btnAdd);
            this.tabPageInput.Text = "Ввод данных";
            
            // === Вкладка 2: Таблица ===
            this.dataGridViewRunners = new DataGridView();
            this.btnRemove = new Button();
            this.btnClear = new Button();
            this.btnSave = new Button();
            this.btnLoad = new Button();
            this.btnFindBest = new Button();
            this.labelTotalCount = new Label();
            this.listBoxBestResults = new ListBox();
            
            // dataGridViewRunners
            this.dataGridViewRunners.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewRunners.Size = new System.Drawing.Size(560, 250);
            this.dataGridViewRunners.AllowUserToAddRows = false;
            this.dataGridViewRunners.AllowUserToDeleteRows = false;
            this.dataGridViewRunners.ReadOnly = true;
            this.dataGridViewRunners.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRunners.MultiSelect = false;
            
            // btnRemove
            this.btnRemove.Text = "Удалить";
            this.btnRemove.Location = new System.Drawing.Point(20, 280);
            this.btnRemove.Size = new System.Drawing.Size(100, 30);
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            
            // btnClear
            this.btnClear.Text = "Очистить";
            this.btnClear.Location = new System.Drawing.Point(130, 280);
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            
            // btnSave
            this.btnSave.Text = "Сохранить";
            this.btnSave.Location = new System.Drawing.Point(240, 280);
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnLoad
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.Location = new System.Drawing.Point(350, 280);
            this.btnLoad.Size = new System.Drawing.Size(100, 30);
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            
            // btnFindBest
            this.btnFindBest.Text = "Лучшие";
            this.btnFindBest.Location = new System.Drawing.Point(460, 280);
            this.btnFindBest.Size = new System.Drawing.Size(100, 30);
            this.btnFindBest.Click += new System.EventHandler(this.btnFindBest_Click);
            
            // labelTotalCount
            this.labelTotalCount.Text = "Всего участников: 0";
            this.labelTotalCount.Location = new System.Drawing.Point(20, 320);
            this.labelTotalCount.Size = new System.Drawing.Size(200, 20);
            
            // listBoxBestResults
            this.listBoxBestResults.Location = new System.Drawing.Point(20, 350);
            this.listBoxBestResults.Size = new System.Drawing.Size(560, 120);
            this.listBoxBestResults.Font = new System.Drawing.Font("Consolas", 9F);
            
            // Добавление элементов на вкладку 2
            this.tabPageTable.Controls.Add(this.dataGridViewRunners);
            this.tabPageTable.Controls.Add(this.btnRemove);
            this.tabPageTable.Controls.Add(this.btnClear);
            this.tabPageTable.Controls.Add(this.btnSave);
            this.tabPageTable.Controls.Add(this.btnLoad);
            this.tabPageTable.Controls.Add(this.btnFindBest);
            this.tabPageTable.Controls.Add(this.labelTotalCount);
            this.tabPageTable.Controls.Add(this.listBoxBestResults);
            this.tabPageTable.Text = "Таблица";
            
            // === Вкладка 3: Диаграмма ===
            this.chartSpeed = new System.Windows.Forms.DataVisualization.Charting.Chart();
            
            // chartSpeed
            this.chartSpeed.Location = new System.Drawing.Point(20, 20);
            this.chartSpeed.Size = new System.Drawing.Size(560, 400);
            this.chartSpeed.Text = "Диаграмма скоростей";
            
            // Добавление элементов на вкладку 3
            this.tabPageChart.Controls.Add(this.chartSpeed);
            this.tabPageChart.Text = "Диаграмма";
            
            // === Настройка TabControl ===
            this.tabControl1.Controls.Add(this.tabPageInput);
            this.tabControl1.Controls.Add(this.tabPageTable);
            this.tabControl1.Controls.Add(this.tabPageChart);
            this.tabControl1.Dock = DockStyle.Fill;
            
            // === Form1 ===
            this.ClientSize = new System.Drawing.Size(620, 520);
            this.Controls.Add(this.tabControl1);
            this.Text = "Соревнования по бегу - Вариант 14";
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Подключение Chart
            this.chartSpeed.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());
            
            this.ResumeLayout(false);
        }

        #endregion
    }
}