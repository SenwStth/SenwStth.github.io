using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Параметры фигуры
        private RectangleF shapeRect;           // Прямоугольник фигуры
        private float shapeWidth = 50;          // Ширина фигуры
        private float shapeHeight = 50;         // Высота фигуры
        private Color shapeColor = Color.Blue;  // Цвет фигуры

        // Параметры движения
        private float stepX = 2;                 // Шаг движения по X
        private float minX = 10;                  // Минимальная позиция по X
        private float maxX;                        // Максимальная позиция по X

        // Параметры трансформации
        private float currentHeight = 50;         // Текущая высота
        private float minHeight = 50;              // Минимальная высота (квадрат)
        private float maxHeight = 100;             // Максимальная высота (прямоугольник)
        private float heightStep = 0.5f;           // Шаг изменения высоты
        private bool expanding = true;              // Растягиваемся или сжимаемся

        // Таймер
        private Timer animationTimer;

        // Флаг для завершения программы
        private bool isRunning = true;

        public Form1()
        {
            InitializeComponent();

            // Настройка таймера
            animationTimer = new Timer();
            animationTimer.Interval = 30;
            animationTimer.Tick += AnimationTimer_Tick;

            // Подписка на события клавиш
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            // Инициализация позиции
            InitializeShape();
        }

        private void InitializeShape()
        {
            // Начальная позиция - левая часть формы
            minX = shapeWidth / 2 + 5;
            maxX = this.ClientSize.Width - shapeWidth / 2 - 5;

            float startX = minX;
            float startY = this.ClientSize.Height / 2;

            shapeRect = new RectangleF(
                startX - shapeWidth / 2,
                startY - currentHeight / 2,
                shapeWidth,
                currentHeight
            );
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Движение по горизонтали
            shapeRect.X += stepX;

            // Проверка достижения границ
            if (shapeRect.X <= minX)
            {
                shapeRect.X = minX;
                stepX = Math.Abs(stepX);
            }
            else if (shapeRect.X + shapeRect.Width >= maxX + shapeRect.Width / 2)
            {
                shapeRect.X = maxX + shapeRect.Width / 2 - shapeRect.Width;
                stepX = -Math.Abs(stepX);
            }

            // Трансформация высоты
            if (expanding)
            {
                currentHeight += heightStep;
                if (currentHeight >= maxHeight)
                {
                    currentHeight = maxHeight;
                    expanding = false;
                }
            }
            else
            {
                currentHeight -= heightStep;
                if (currentHeight <= minHeight)
                {
                    currentHeight = minHeight;
                    expanding = true;
                }
            }

            // Обновление прямоугольника
            shapeRect.Height = currentHeight;
            shapeRect.Y = this.ClientSize.Height / 2 - currentHeight / 2;

            // Перерисовка
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Рисование фигуры
            using (SolidBrush brush = new SolidBrush(shapeColor))
            {
                e.Graphics.FillRectangle(brush, shapeRect);
            }

            // Рисование рамки
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawRectangle(pen, Rectangle.Round(shapeRect));
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Завершение при нажатии любой клавиши
            if (isRunning)
            {
                animationTimer.Stop();
                isRunning = false;
                this.Close();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
            {
                maxX = this.ClientSize.Width - shapeWidth / 2 - 10;

                if (shapeRect != null)
                {
                    if (shapeRect.X < minX)
                        shapeRect.X = minX;
                    if (shapeRect.X + shapeRect.Width > this.ClientSize.Width - minX)
                        shapeRect.X = this.ClientSize.Width - shapeRect.Width - minX;

                    shapeRect.Y = this.ClientSize.Height / 2 - currentHeight / 2;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            animationTimer.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            animationTimer.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form2 settingsForm = new Form2(shapeColor, animationTimer.Interval);

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                shapeColor = settingsForm.SelectedColor;
                animationTimer.Interval = settingsForm.Speed;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            animationTimer.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }
    }
}