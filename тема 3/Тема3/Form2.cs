using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private ColorDialog colorDialog;
        private Timer speedPreviewTimer;
        private int currentSpeed;
        private Color currentColor;

        public Color SelectedColor { get; private set; }
        public int Speed { get; private set; }

        public Form2(Color initialColor, int initialSpeed)
        {
            InitializeComponent();

            currentColor = initialColor;
            currentSpeed = initialSpeed;

            SelectedColor = initialColor;
            Speed = initialSpeed;

            // Отображение текущих значений
            UpdateColorDisplay();
            trackBarSpeed.Value = initialSpeed;
            lblSpeedValue.Text = $"{initialSpeed} мс";

            // Таймер для предпросмотра
            speedPreviewTimer = new Timer();
            speedPreviewTimer.Interval = initialSpeed;
            speedPreviewTimer.Tick += SpeedPreviewTimer_Tick;

            colorDialog = new ColorDialog();
        }

        private void UpdateColorDisplay()
        {
            panelColor.BackColor = currentColor;
            lblColor.Text = $"Текущий цвет: {currentColor.Name}";
        }

        private void SpeedPreviewTimer_Tick(object sender, EventArgs e)
        {
            // Мигание панели для предпросмотра скорости
            panelPreview.BackColor = panelPreview.BackColor == Color.Red ?
                Color.Transparent : Color.Red;
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = currentColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
                UpdateColorDisplay();
            }
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            currentSpeed = trackBarSpeed.Value;
            lblSpeedValue.Text = $"{currentSpeed} мс";

            // Обновление предпросмотра
            speedPreviewTimer.Interval = currentSpeed;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (btnPreview.Text == "Предпросмотр")
            {
                speedPreviewTimer.Start();
                btnPreview.Text = "Стоп";
            }
            else
            {
                speedPreviewTimer.Stop();
                panelPreview.BackColor = Color.Transparent;
                btnPreview.Text = "Предпросмотр";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedColor = currentColor;
            Speed = currentSpeed;

            speedPreviewTimer.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            speedPreviewTimer.Stop();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            speedPreviewTimer.Stop();
        }
    }
}