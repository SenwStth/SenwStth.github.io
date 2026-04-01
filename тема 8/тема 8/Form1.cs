using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace тема_8
{
    public partial class Form1 : Form
    {
        private const int GridSize = 10;
        private const int CellSize = 50;

        private Cell[,] _grid = new Cell[GridSize, GridSize];
        private List<Ship> _ships = new List<Ship>();
        private string _playerName;
        private int _shots = 0;
        private int _hits = 0;
        private int _misses = 0;
        private int _maxMisses = 30;
        private bool _gameActive = false;
        private List<GameResult> _results = new List<GameResult>();
        private string _dataPath;

        public Form1()
        {
            InitializeComponent();
            _dataPath = Path.Combine(Application.StartupPath, "data");
            if (!Directory.Exists(_dataPath))
                Directory.CreateDirectory(_dataPath);
            
            LoadResults();
            ShowLoginDialog();
            InitializeGrid();
            GenerateShips();
            UpdateStats();
        }

        private void ShowLoginDialog()
        {
            using (var form = new Form())
            {
                form.Text = "Добро пожаловать";
                form.StartPosition = FormStartPosition.CenterParent;
                form.ClientSize = new Size(300, 130);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var label = new Label { Text = "Введите ваше имя:", Location = new Point(10, 15), Size = new Size(280, 20) };
                var textBox = new TextBox { Location = new Point(10, 40), Size = new Size(280, 25), Text = "Игрок" };
                var okBtn = new Button { Text = "Начать игру", Location = new Point(80, 75), Size = new Size(140, 35), DialogResult = DialogResult.OK };

                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(okBtn);

                if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
                    _playerName = textBox.Text;
                else
                    _playerName = "Игрок";
            }
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < GridSize; i++)
                for (int j = 0; j < GridSize; j++)
                    _grid[i, j] = new Cell(i, j);
        }

        private void GenerateShips()
        {
            _ships.Clear();
            InitializeGrid();
            
            Random rand = new Random();
            int[] shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

            foreach (int size in shipSizes)
            {
                bool placed = false;
                int attempts = 0;

                while (!placed && attempts < 100)
                {
                    attempts++;
                    int direction = rand.Next(4);
                    int row = rand.Next(GridSize);
                    int col = rand.Next(GridSize);

                    List<Point> cells = new List<Point>();
                    bool canPlace = true;

                    for (int i = 0; i < size; i++)
                    {
                        int newRow = row, newCol = col;

                        if (direction == 0) newRow = row + i;
                        else if (direction == 1) newRow = row - i;
                        else if (direction == 2) newCol = col + i;
                        else newCol = col - i;

                        if (newRow < 0 || newRow >= GridSize || newCol < 0 || newCol >= GridSize)
                        {
                            canPlace = false;
                            break;
                        }

                        if (_grid[newRow, newCol].HasShip)
                        {
                            canPlace = false;
                            break;
                        }

                        cells.Add(new Point(newRow, newCol));
                    }

                    if (canPlace)
                    {
                        Ship ship = new Ship(cells);
                        _ships.Add(ship);

                        foreach (var cell in cells)
                        {
                            _grid[cell.X, cell.Y].HasShip = true;
                            _grid[cell.X, cell.Y].ShipIndex = _ships.Count - 1;
                        }
                        placed = true;
                    }
                }
            }
        }

        private int CountAdjacentShips(int row, int col)
        {
            int count = 0;
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    int r = row + dr, c = col + dc;
                    if (r >= 0 && r < GridSize && c >= 0 && c < GridSize)
                        if (_grid[r, c].HasShip && !_grid[r, c].IsOpened)
                            count++;
                }
            }
            return count;
        }

        private void PanelGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.LightBlue);

            using (var pen = new Pen(Color.DarkBlue, 1))
            {
                for (int i = 0; i <= GridSize; i++)
                {
                    g.DrawLine(pen, 0, i * CellSize, GridSize * CellSize, i * CellSize);
                    g.DrawLine(pen, i * CellSize, 0, i * CellSize, GridSize * CellSize);
                }
            }

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    Cell cell = _grid[row, col];
                    Rectangle rect = new Rectangle(col * CellSize + 1, row * CellSize + 1, CellSize - 2, CellSize - 2);

                    if (cell.IsOpened)
                    {
                        if (cell.HasShip)
                        {
                            g.FillRectangle(new SolidBrush(Color.Red), rect);
                            g.DrawString("X", new Font("Arial", 24, FontStyle.Bold), Brushes.White,
                                rect.X + CellSize / 2 - 12, rect.Y + CellSize / 2 - 15);
                        }
                        else
                        {
                            int count = CountAdjacentShips(row, col);
                            if (count > 0)
                            {
                                g.FillRectangle(new SolidBrush(Color.White), rect);
                                g.DrawString(count.ToString(), new Font("Arial", 20, FontStyle.Bold), Brushes.Black,
                                    rect.X + CellSize / 2 - 8, rect.Y + CellSize / 2 - 12);
                            }
                        }
                    }
                    else if (cell.IsMarked)
                    {
                        g.FillRectangle(new SolidBrush(Color.Yellow), rect);
                        g.DrawString("?", new Font("Arial", 24, FontStyle.Bold), Brushes.Black,
                            rect.X + CellSize / 2 - 10, rect.Y + CellSize / 2 - 15);
                    }
                }
            }

            using (var font = new Font("Arial", 10, FontStyle.Bold))
            {
                for (int i = 0; i < GridSize; i++)
                {
                    g.DrawString((i + 1).ToString(), font, Brushes.Black, i * CellSize + CellSize / 2 - 5, GridSize * CellSize + 5);
                    g.DrawString(((char)('A' + i)).ToString(), font, Brushes.Black, -3, i * CellSize + CellSize / 2 - 6);
                }
            }
        }

        private void PanelGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_gameActive)
            {
                MessageBox.Show("Нажмите 'НОВАЯ ИГРА' для начала!", "Игра не активна", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int col = e.X / CellSize;
            int row = e.Y / CellSize;

            if (row < 0 || row >= GridSize || col < 0 || col >= GridSize)
                return;

            Cell cell = _grid[row, col];
            if (cell.IsOpened) return;

            _shots++;

            if (cell.HasShip)
            {
                _hits++;
                cell.IsOpened = true;

                var ship = _ships[cell.ShipIndex];
                bool allDestroyed = true;
                foreach (var c in ship.Cells)
                {
                    if (!_grid[c.X, c.Y].IsOpened)
                    {
                        allDestroyed = false;
                        break;
                    }
                }

                if (allDestroyed)
                {
                    ship.IsDestroyed = true;
                    CheckWin();
                }
            }
            else
            {
                _misses++;
                cell.IsOpened = true;

                if (_misses >= _maxMisses)
                {
                    EndGame(false);
                    return;
                }
            }

            UpdateStats();
            gamePanel.Invalidate();
        }

        private void PanelGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _gameActive)
            {
                int col = e.X / CellSize;
                int row = e.Y / CellSize;

                if (row >= 0 && row < GridSize && col >= 0 && col < GridSize)
                {
                    Cell cell = _grid[row, col];
                    if (!cell.IsOpened)
                    {
                        cell.IsMarked = !cell.IsMarked;
                        gamePanel.Invalidate();
                    }
                }
            }
        }

        private void CheckWin()
        {
            int destroyedCount = 0;
            foreach (var ship in _ships)
                if (ship.IsDestroyed) destroyedCount++;

            if (destroyedCount == _ships.Count)
                EndGame(true);
        }

        private void EndGame(bool won)
        {
            _gameActive = false;

            var result = new GameResult(_playerName, _shots, _hits, _misses,
                GetDestroyedShipsCount(), _ships.Count, won);
            _results.Add(result);
            SaveResults();

            string message = won
                ? "🎉 ПОБЕДА! 🎉\n\nВы уничтожили все корабли!\n\nВыстрелов: " + _shots + "\nПопаданий: " + _hits + "\nПромахов: " + _misses
                : "💥 ИГРА ОКОНЧЕНА 💥\n\nСлишком много промахов!\n\nВыстрелов: " + _shots + "\nПопаданий: " + _hits + "\nПромахов: " + _misses;

            var dialogResult = MessageBox.Show(message + "\n\nНачать новую игру?", 
                won ? "Победа!" : "Проигрыш",
                MessageBoxButtons.YesNo, 
                won ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
                BtnNewGame_Click(null, null);
        }

        private int GetDestroyedShipsCount()
        {
            int count = 0;
            foreach (var ship in _ships)
                if (ship.IsDestroyed) count++;
            return count;
        }

        private void UpdateStats()
        {
            shotsLabel.Text = "Выстрелов: " + _shots;
            hitsLabel.Text = "Попаданий: " + _hits;
            missesLabel.Text = "Промахов: " + _misses + " / " + _maxMisses;
            int destroyed = GetDestroyedShipsCount();
            shipsLabel.Text = "Кораблей: " + destroyed + " / " + _ships.Count;
            statusLabel.Text = "Игрок: " + _playerName + " | Кораблей: " + destroyed + "/" + _ships.Count;
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            _shots = 0;
            _hits = 0;
            _misses = 0;
            _ships.Clear();
            GenerateShips();
            _gameActive = true;
            UpdateStats();
            gamePanel.Invalidate();
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridSize; i++)
                for (int j = 0; j < GridSize; j++)
                    if (_grid[i, j].HasShip)
                        _grid[i, j].IsOpened = true;
            
            _gameActive = false;
            gamePanel.Invalidate();
        }

        private void SaveResults()
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "results.dat");
                using (var fs = new FileStream(filePath, FileMode.Create))
                    new BinaryFormatter().Serialize(fs, _results);
            }
            catch { }
        }

        private void LoadResults()
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "results.dat");
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                        _results = (List<GameResult>)new BinaryFormatter().Deserialize(fs);
                }
            }
            catch { }
        }

        private void ShowSettings()
        {
            using (var form = new Form())
            {
                form.Text = "Настройки";
                form.StartPosition = FormStartPosition.CenterParent;
                form.ClientSize = new Size(300, 150);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;

                var label = new Label { Text = "Максимум промахов:", Location = new Point(10, 20), Size = new Size(120, 20) };
                var numeric = new NumericUpDown { Location = new Point(140, 17), Size = new Size(80, 25), Value = _maxMisses, Minimum = 10, Maximum = 100 };
                var okBtn = new Button { Text = "OK", Location = new Point(100, 70), Size = new Size(100, 30), DialogResult = DialogResult.OK };

                form.Controls.Add(label);
                form.Controls.Add(numeric);
                form.Controls.Add(okBtn);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _maxMisses = (int)numeric.Value;
                    UpdateStats();
                }
            }
        }

        private void ShowHelp()
        {
            MessageBox.Show(
                "🎯 МОРСКОЙ БОЙ\n\n" +
                "ПРАВИЛА:\n" +
                "• Найдите и уничтожьте все 10 кораблей\n" +
                "• Кликните по клетке, чтобы выстрелить\n" +
                "• Число = количество кораблей рядом\n\n" +
                "УПРАВЛЕНИЕ:\n" +
                "• ЛКМ - выстрел\n" +
                "• ПКМ - пометить клетку\n\n" +
                "ПОБЕДА: уничтожить все корабли\n" +
                "ПРОИГРЫШ: превысить лимит промахов",
                "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
