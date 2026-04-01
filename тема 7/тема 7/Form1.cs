using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace тема_7
{
    public partial class Form1 : Form
    {
        private List<Figure> _figures = new List<Figure>();
        private List<Figure> _clipboard = new List<Figure>();
        private Figure _selectedFigure;
        private SelectionFrame _selectionFrame = new SelectionFrame();
        private StackMemory _undoStack = new StackMemory(20);
        private StackMemory _redoStack = new StackMemory(20);
        private Color _currentColor = Color.Black;
        private Color _currentFillColor = Color.Transparent;
        private bool _hasFill = false;
        private float _currentStrokeWidth = 2f;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void SaveState()
        {
            _undoStack.Push(ObjectToStream(_figures));
            _redoStack.Clear();
        }

        private MemoryStream ObjectToStream(List<Figure> figures)
        {
            var ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, figures);
            ms.Position = 0;
            return ms;
        }

        private List<Figure> StreamToObject(MemoryStream ms)
        {
            ms.Position = 0;
            return (List<Figure>)new BinaryFormatter().Deserialize(ms);
        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            foreach (var figure in _figures)
                figure.Draw(g);

            if (_selectedFigure != null)
            {
                _selectionFrame.Update(_selectedFigure);
                _selectionFrame.Draw(g);
            }
        }

        private void PanelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            Figure clicked = FindFigureAt(e.Location);
            if (clicked != null)
            {
                if (_selectedFigure != clicked)
                {
                    SaveState();
                    _selectedFigure = clicked;
                }
            }
            else
            {
                _selectedFigure = null;
            }
            panelCanvas.Invalidate();
        }

        private Figure FindFigureAt(Point point)
        {
            for (int i = _figures.Count - 1; i >= 0; i--)
                if (_figures[i].ContainsPoint(point))
                    return _figures[i];
            return null;
        }

        private void CreateFigure(FigureType type)
        {
            if (_figures.Count > 0)
            {
                MessageBox.Show("Сначала удалите или переместите существующую фигуру!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var stroke = new Stroke(_currentColor, _currentStrokeWidth)
            {
                FillColor = _currentFillColor,
                HasFill = _hasFill
            };

            Figure figure;
            Point loc = new Point(panelCanvas.Width / 2 - 75, panelCanvas.Height / 2 - 50);

            switch (type)
            {
                case FigureType.Square:
                    figure = new Square(loc, new Size(100, 100), stroke);
                    break;
                case FigureType.Rectangle:
                    figure = new MyRectangle(loc, new Size(150, 80), stroke);
                    break;
                case FigureType.ChamferedTopBottom:
                    figure = new ChamferedRectangle(loc, new Size(150, 100), stroke) { Type = 0 };
                    break;
                case FigureType.ChamferedOpposite:
                    figure = new ChamferedRectangle(loc, new Size(150, 100), stroke) { Type = 1 };
                    break;
                case FigureType.ChamferedSide:
                    figure = new ChamferedRectangle(loc, new Size(150, 100), stroke) { Type = 2 };
                    break;
                default:
                    figure = new MyRectangle(loc, new Size(150, 80), stroke);
                    break;
            }

            SaveState();
            _figures.Add(figure);
            _selectedFigure = figure;
            panelCanvas.Invalidate();
            lblStatus.Text = "Добавлено: " + figure.GetType().Name;
        }

        private int _figureOffset = 0;
        private enum FigureType { Square, Rectangle, ChamferedTopBottom, ChamferedOpposite, ChamferedSide }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_selectedFigure == null) return;
            int pixels = e.Shift ? 1 : 5;

            SaveState();

            switch (e.KeyCode)
            {
                case Keys.Left: _selectedFigure.ShiftLeft(pixels); break;
                case Keys.Right: _selectedFigure.ShiftRight(pixels); break;
                case Keys.Up: _selectedFigure.ShiftUp(pixels); break;
                case Keys.Down: _selectedFigure.ShiftDown(pixels); break;
                case Keys.Delete: DeleteSelected(); return;
                default: return;
            }
            panelCanvas.Invalidate();
        }

        private void DeleteSelected()
        {
            if (_selectedFigure != null)
            {
                SaveState();
                _figures.Remove(_selectedFigure);
                _selectedFigure = null;
                panelCanvas.Invalidate();
                lblStatus.Text = "Удалено";
            }
        }

        private void AlignSelectedFigures(string alignment)
        {
            if (_figures.Count < 1) return;

            SaveState();

            if (alignment == "Left")
            {
                int minX = int.MaxValue;
                foreach (var f in _figures) if (f.Location.X < minX) minX = f.Location.X;
                foreach (var f in _figures) f.Location = new Point(minX, f.Location.Y);
            }
            else if (alignment == "Right")
            {
                int maxX = int.MinValue;
                foreach (var f in _figures) if (f.Location.X + f.Size.Width > maxX) maxX = f.Location.X + f.Size.Width;
                foreach (var f in _figures) f.Location = new Point(maxX - f.Size.Width, f.Location.Y);
            }
            else if (alignment == "Top")
            {
                int minY = int.MaxValue;
                foreach (var f in _figures) if (f.Location.Y < minY) minY = f.Location.Y;
                foreach (var f in _figures) f.Location = new Point(f.Location.X, minY);
            }
            else if (alignment == "Bottom")
            {
                int maxY = int.MinValue;
                foreach (var f in _figures) if (f.Location.Y + f.Size.Height > maxY) maxY = f.Location.Y + f.Size.Height;
                foreach (var f in _figures) f.Location = new Point(f.Location.X, maxY - f.Size.Height);
            }

            panelCanvas.Invalidate();
            lblStatus.Text = "Выровнено по " + alignment;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_figures.Count > 0 && MessageBox.Show("Очистить холст?", "Новый", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            SaveState();
            _figures.Clear();
            _selectedFigure = null;
            panelCanvas.Invalidate();
            lblStatus.Text = "Новый холст";
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var fs = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                        new BinaryFormatter().Serialize(fs, _figures);
                    lblStatus.Text = "Сохранено";
                }
                catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
                        _figures = (List<Figure>)new BinaryFormatter().Deserialize(fs);
                    _selectedFigure = null;
                    panelCanvas.Invalidate();
                    lblStatus.Text = "Загружено";
                }
                catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_undoStack.Count > 0)
            {
                var ms = new MemoryStream();
                _undoStack.Pop(ms);
                _redoStack.Push(ObjectToStream(_figures));
                _figures = StreamToObject(ms);
                _selectedFigure = null;
                panelCanvas.Invalidate();
                lblStatus.Text = "Отменено";
            }
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_redoStack.Count > 0)
            {
                var ms = new MemoryStream();
                _redoStack.Pop(ms);
                _undoStack.Push(ObjectToStream(_figures));
                _figures = StreamToObject(ms);
                _selectedFigure = null;
                panelCanvas.Invalidate();
                lblStatus.Text = "Возврат";
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedFigure != null)
            {
                _clipboard.Clear();
                _clipboard.Add(_selectedFigure.Clone());
                lblStatus.Text = "Скопировано";
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedFigure != null)
            {
                _clipboard.Clear();
                _clipboard.Add(_selectedFigure.Clone());
                SaveState();
                _figures.Remove(_selectedFigure);
                _selectedFigure = null;
                panelCanvas.Invalidate();
                lblStatus.Text = "Вырезано";
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_clipboard.Count > 0)
            {
                SaveState();
                var pasted = _clipboard[0].Clone();
                pasted.Location = new Point(pasted.Location.X + 20, pasted.Location.Y + 20);
                _figures.Add(pasted);
                _selectedFigure = pasted;
                panelCanvas.Invalidate();
                lblStatus.Text = "Вставлено";
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) => DeleteSelected();

        private void SquareToolStripMenuItem_Click(object sender, EventArgs e) => CreateFigure(FigureType.Square);
        private void RectangleToolStripMenuItem_Click(object sender, EventArgs e) => CreateFigure(FigureType.Rectangle);
        private void ChamferedTopBottomToolStripMenuItem_Click(object sender, EventArgs e) => CreateFigure(FigureType.ChamferedTopBottom);
        private void ChamferedOppositeToolStripMenuItem_Click(object sender, EventArgs e) => CreateFigure(FigureType.ChamferedOpposite);
        private void ChamferedSideToolStripMenuItem_Click(object sender, EventArgs e) => CreateFigure(FigureType.ChamferedSide);

        private void AlignLeftToolStripMenuItem_Click(object sender, EventArgs e) => AlignSelectedFigures("Left");
        private void AlignRightToolStripMenuItem_Click(object sender, EventArgs e) => AlignSelectedFigures("Right");
        private void AlignTopToolStripMenuItem_Click(object sender, EventArgs e) => AlignSelectedFigures("Top");
        private void AlignBottomToolStripMenuItem_Click(object sender, EventArgs e) => AlignSelectedFigures("Bottom");

        private void BtnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _currentColor = colorDialog1.Color;
                if (_selectedFigure != null)
                {
                    SaveState();
                    _selectedFigure.Stroke.Color = _currentColor;
                    panelCanvas.Invalidate();
                }
            }
        }

        private void BtnFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _currentFillColor = colorDialog1.Color;
                _hasFill = true;
                if (_selectedFigure != null)
                {
                    SaveState();
                    _selectedFigure.Stroke.FillColor = _currentFillColor;
                    _selectedFigure.Stroke.HasFill = true;
                    panelCanvas.Invalidate();
                }
            }
        }

        private void BtnStrokeWidth_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.Text = "Толщина линии";
                form.StartPosition = FormStartPosition.CenterParent;
                form.ClientSize = new Size(200, 60);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;

                var numeric = new NumericUpDown { Location = new Point(10, 15), Size = new Size(80, 25), Value = (decimal)_currentStrokeWidth, Minimum = 1, Maximum = 20 };
                var okBtn = new Button { Text = "OK", Location = new Point(100, 15), Size = new Size(80, 25), DialogResult = DialogResult.OK };

                form.Controls.Add(numeric);
                form.Controls.Add(okBtn);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _currentStrokeWidth = (float)numeric.Value;
                    if (_selectedFigure != null)
                    {
                        SaveState();
                        _selectedFigure.Stroke.Width = _currentStrokeWidth;
                        panelCanvas.Invalidate();
                    }
                }
            }
        }

        private void PanelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            lblCoords.Text = string.Format("X: {0}, Y: {1}", e.X, e.Y);
        }
    }
}
