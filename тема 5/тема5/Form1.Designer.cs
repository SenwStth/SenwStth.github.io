using System.Windows.Forms;

namespace DictionaryApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Элементы управления
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem loadMenu;
        private ToolStripMenuItem saveMenu;
        private ToolStripMenuItem newMenu;
        private ToolStripMenuItem exitMenu;

        private GroupBox groupBoxDictionary;
        private DataGridView dataGridViewWords;
        private Label labelWordCount;
        private TextBox textBoxSearch;

        private GroupBox groupBoxAddRemove;
        private TextBox textBoxNewWord;
        private Button btnAddWord;
        private Button btnRemoveWord;

        private GroupBox groupBoxSearch;
        private Label labelLength;
        private TextBox textBoxLength;
        private Label labelPositions;
        private TextBox textBoxPositions;
        private Button btnSearch;

        private GroupBox groupBoxFuzzy;
        private Label labelFuzzyPattern;
        private TextBox textBoxFuzzyPattern;
        private Button btnFuzzySearch;

        private GroupBox groupBoxPrefix;
        private Label labelPrefix;
        private TextBox textBoxPrefix;
        private Button btnPrefixSearch;

        private GroupBox groupBoxResults;
        private ListBox listBoxResults;
        private Button btnSaveResults;

        private Button btnLoadDictionary;
        private Button btnSaveDictionary;
        private Button btnNewDictionary;
        private Label labelInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new MenuStrip();
            this.fileMenu = new ToolStripMenuItem();
            this.newMenu = new ToolStripMenuItem();
            this.loadMenu = new ToolStripMenuItem();
            this.saveMenu = new ToolStripMenuItem();
            this.exitMenu = new ToolStripMenuItem();

            this.groupBoxDictionary = new GroupBox();
            this.dataGridViewWords = new DataGridView();
            this.labelWordCount = new Label();
            this.textBoxSearch = new TextBox();

            this.groupBoxAddRemove = new GroupBox();
            this.textBoxNewWord = new TextBox();
            this.btnAddWord = new Button();
            this.btnRemoveWord = new Button();

            this.groupBoxSearch = new GroupBox();
            this.labelLength = new Label();
            this.textBoxLength = new TextBox();
            this.labelPositions = new Label();
            this.textBoxPositions = new TextBox();
            this.btnSearch = new Button();
            this.labelInfo = new Label();

            this.groupBoxFuzzy = new GroupBox();
            this.labelFuzzyPattern = new Label();
            this.textBoxFuzzyPattern = new TextBox();
            this.btnFuzzySearch = new Button();

            this.groupBoxPrefix = new GroupBox();
            this.labelPrefix = new Label();
            this.textBoxPrefix = new TextBox();
            this.btnPrefixSearch = new Button();

            this.groupBoxResults = new GroupBox();
            this.listBoxResults = new ListBox();
            this.btnSaveResults = new Button();

            this.btnLoadDictionary = new Button();
            this.btnSaveDictionary = new Button();
            this.btnNewDictionary = new Button();

            this.menuStrip.SuspendLayout();
            this.groupBoxDictionary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).BeginInit();
            this.groupBoxAddRemove.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.groupBoxFuzzy.SuspendLayout();
            this.groupBoxPrefix.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new ToolStripItem[] { this.fileMenu });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1100, 24);
            this.menuStrip.TabIndex = 0;

            // fileMenu
            this.fileMenu.Text = "Файл";
            this.fileMenu.DropDownItems.AddRange(new ToolStripItem[] {
                this.newMenu, this.loadMenu, this.saveMenu, this.exitMenu });

            this.newMenu.Text = "Новый словарь";
            this.newMenu.Click += new System.EventHandler(this.btnNewDictionary_Click);

            this.loadMenu.Text = "Загрузить словарь";
            this.loadMenu.Click += new System.EventHandler(this.btnLoadDictionary_Click);

            this.saveMenu.Text = "Сохранить словарь";
            this.saveMenu.Click += new System.EventHandler(this.btnSaveDictionary_Click);

            this.exitMenu.Text = "Выход";
            this.exitMenu.Click += (s, e) => this.Close();

            // groupBoxDictionary
            this.groupBoxDictionary.Controls.Add(this.dataGridViewWords);
            this.groupBoxDictionary.Controls.Add(this.labelWordCount);
            this.groupBoxDictionary.Controls.Add(this.textBoxSearch);
            this.groupBoxDictionary.Location = new System.Drawing.Point(12, 40);
            this.groupBoxDictionary.Size = new System.Drawing.Size(350, 500);
            this.groupBoxDictionary.Text = "Словарь";

            // textBoxSearch
            this.textBoxSearch.Location = new System.Drawing.Point(10, 20);
            this.textBoxSearch.Size = new System.Drawing.Size(330, 20);
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);

            // dataGridViewWords
            this.dataGridViewWords.Location = new System.Drawing.Point(10, 50);
            this.dataGridViewWords.Size = new System.Drawing.Size(330, 420);
            this.dataGridViewWords.AllowUserToAddRows = false;
            this.dataGridViewWords.AllowUserToDeleteRows = false;
            this.dataGridViewWords.ReadOnly = true;
            this.dataGridViewWords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // labelWordCount
            this.labelWordCount.Text = "Всего слов: 0";
            this.labelWordCount.Location = new System.Drawing.Point(10, 475);
            this.labelWordCount.Size = new System.Drawing.Size(330, 20);

            // groupBoxAddRemove
            this.groupBoxAddRemove.Controls.Add(this.textBoxNewWord);
            this.groupBoxAddRemove.Controls.Add(this.btnAddWord);
            this.groupBoxAddRemove.Controls.Add(this.btnRemoveWord);
            this.groupBoxAddRemove.Location = new System.Drawing.Point(370, 40);
            this.groupBoxAddRemove.Size = new System.Drawing.Size(340, 100);
            this.groupBoxAddRemove.Text = "Добавление/Удаление слов";

            this.textBoxNewWord.Location = new System.Drawing.Point(10, 25);
            this.textBoxNewWord.Size = new System.Drawing.Size(200, 20);

            this.btnAddWord.Text = "Добавить";
            this.btnAddWord.Location = new System.Drawing.Point(220, 23);
            this.btnAddWord.Size = new System.Drawing.Size(100, 25);
            this.btnAddWord.Click += new System.EventHandler(this.btnAddWord_Click);

            this.btnRemoveWord.Text = "Удалить выделенное";
            this.btnRemoveWord.Location = new System.Drawing.Point(10, 60);
            this.btnRemoveWord.Size = new System.Drawing.Size(150, 30);
            this.btnRemoveWord.Click += new System.EventHandler(this.btnRemoveWord_Click);

            // groupBoxSearch
            this.groupBoxSearch.Controls.Add(this.labelLength);
            this.groupBoxSearch.Controls.Add(this.textBoxLength);
            this.groupBoxSearch.Controls.Add(this.labelPositions);
            this.groupBoxSearch.Controls.Add(this.textBoxPositions);
            this.groupBoxSearch.Controls.Add(this.btnSearch);
            this.groupBoxSearch.Controls.Add(this.labelInfo);
            this.groupBoxSearch.Location = new System.Drawing.Point(370, 150);
            this.groupBoxSearch.Size = new System.Drawing.Size(340, 150);
            this.groupBoxSearch.Text = "Поиск по длине и позициям (Вариант 14)";

            this.labelLength.Text = "Длина слова:";
            this.labelLength.Location = new System.Drawing.Point(10, 25);
            this.labelLength.Size = new System.Drawing.Size(80, 20);

            this.textBoxLength.Location = new System.Drawing.Point(100, 23);
            this.textBoxLength.Size = new System.Drawing.Size(50, 20);

            this.labelPositions.Text = "Позиции (позиция=буква):";
            this.labelPositions.Location = new System.Drawing.Point(10, 55);
            this.labelPositions.Size = new System.Drawing.Size(150, 20);

            this.textBoxPositions.Location = new System.Drawing.Point(10, 75);
            this.textBoxPositions.Size = new System.Drawing.Size(320, 20);
            this.textBoxPositions.Multiline = true;
            this.textBoxPositions.Height = 40;

            this.btnSearch.Text = "Найти";
            this.btnSearch.Location = new System.Drawing.Point(10, 120);
            this.btnSearch.Size = new System.Drawing.Size(100, 25);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.labelInfo.Text = "Пример: 1=а\n2=б (каждая позиция на новой строке)";
            this.labelInfo.Location = new System.Drawing.Point(120, 120);
            this.labelInfo.Size = new System.Drawing.Size(210, 30);
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.labelInfo.ForeColor = System.Drawing.Color.Gray;

            // groupBoxFuzzy
            this.groupBoxFuzzy.Controls.Add(this.labelFuzzyPattern);
            this.groupBoxFuzzy.Controls.Add(this.textBoxFuzzyPattern);
            this.groupBoxFuzzy.Controls.Add(this.btnFuzzySearch);
            this.groupBoxFuzzy.Location = new System.Drawing.Point(370, 310);
            this.groupBoxFuzzy.Size = new System.Drawing.Size(340, 80);
            this.groupBoxFuzzy.Text = "Нечеткий поиск (Левенштейн <= 3)";

            this.labelFuzzyPattern.Text = "Слово для поиска:";
            this.labelFuzzyPattern.Location = new System.Drawing.Point(10, 25);
            this.labelFuzzyPattern.Size = new System.Drawing.Size(100, 20);

            this.textBoxFuzzyPattern.Location = new System.Drawing.Point(120, 23);
            this.textBoxFuzzyPattern.Size = new System.Drawing.Size(150, 20);

            this.btnFuzzySearch.Text = "Найти похожие";
            this.btnFuzzySearch.Location = new System.Drawing.Point(10, 50);
            this.btnFuzzySearch.Size = new System.Drawing.Size(320, 25);
            this.btnFuzzySearch.Click += new System.EventHandler(this.btnFuzzySearch_Click);

            // groupBoxPrefix
            this.groupBoxPrefix.Controls.Add(this.labelPrefix);
            this.groupBoxPrefix.Controls.Add(this.textBoxPrefix);
            this.groupBoxPrefix.Controls.Add(this.btnPrefixSearch);
            this.groupBoxPrefix.Location = new System.Drawing.Point(370, 400);
            this.groupBoxPrefix.Size = new System.Drawing.Size(340, 80);
            this.groupBoxPrefix.Text = "Поиск по началу слова";

            this.labelPrefix.Text = "Начало слова:";
            this.labelPrefix.Location = new System.Drawing.Point(10, 25);
            this.labelPrefix.Size = new System.Drawing.Size(80, 20);

            this.textBoxPrefix.Location = new System.Drawing.Point(100, 23);
            this.textBoxPrefix.Size = new System.Drawing.Size(150, 20);

            this.btnPrefixSearch.Text = "Найти";
            this.btnPrefixSearch.Location = new System.Drawing.Point(10, 50);
            this.btnPrefixSearch.Size = new System.Drawing.Size(320, 25);
            this.btnPrefixSearch.Click += new System.EventHandler(this.btnPrefixSearch_Click);

            // groupBoxResults
            this.groupBoxResults.Controls.Add(this.listBoxResults);
            this.groupBoxResults.Controls.Add(this.btnSaveResults);
            this.groupBoxResults.Location = new System.Drawing.Point(720, 40);
            this.groupBoxResults.Size = new System.Drawing.Size(370, 500);
            this.groupBoxResults.Text = "Результаты поиска";

            this.listBoxResults.Location = new System.Drawing.Point(10, 20);
            this.listBoxResults.Size = new System.Drawing.Size(350, 430);
            this.listBoxResults.Font = new System.Drawing.Font("Consolas", 9F);

            this.btnSaveResults.Text = "Сохранить результаты в файл";
            this.btnSaveResults.Location = new System.Drawing.Point(10, 460);
            this.btnSaveResults.Size = new System.Drawing.Size(350, 30);
            this.btnSaveResults.Enabled = false;
            this.btnSaveResults.Click += new System.EventHandler(this.btnSaveResults_Click);

            // Кнопки управления
            this.btnLoadDictionary = new Button();
            this.btnSaveDictionary = new Button();
            this.btnNewDictionary = new Button();

            this.btnLoadDictionary.Text = "Загрузить словарь";
            this.btnLoadDictionary.Location = new System.Drawing.Point(370, 490);
            this.btnLoadDictionary.Size = new System.Drawing.Size(100, 30);
            this.btnLoadDictionary.Click += new System.EventHandler(this.btnLoadDictionary_Click);

            this.btnSaveDictionary.Text = "Сохранить";
            this.btnSaveDictionary.Location = new System.Drawing.Point(480, 490);
            this.btnSaveDictionary.Size = new System.Drawing.Size(100, 30);
            this.btnSaveDictionary.Click += new System.EventHandler(this.btnSaveDictionary_Click);

            this.btnNewDictionary.Text = "Новый словарь";
            this.btnNewDictionary.Location = new System.Drawing.Point(590, 490);
            this.btnNewDictionary.Size = new System.Drawing.Size(100, 30);
            this.btnNewDictionary.Click += new System.EventHandler(this.btnNewDictionary_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(1100, 560);
            this.Controls.Add(this.groupBoxDictionary);
            this.Controls.Add(this.groupBoxAddRemove);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.groupBoxFuzzy);
            this.Controls.Add(this.groupBoxPrefix);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.btnLoadDictionary);
            this.Controls.Add(this.btnSaveDictionary);
            this.Controls.Add(this.btnNewDictionary);
            this.Controls.Add(this.menuStrip);
            this.Text = "Словарь - Вариант 14 (Поиск по длине и позициям)";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.menuStrip.ResumeLayout(false);
            this.groupBoxDictionary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).EndInit();
            this.groupBoxAddRemove.ResumeLayout(false);
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxFuzzy.ResumeLayout(false);
            this.groupBoxPrefix.ResumeLayout(false);
            this.groupBoxResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}