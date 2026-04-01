using System;
using System.Drawing;

namespace тема_7
{
    [Serializable]
    public class SelectionFrame
    {
        private const int MarkerSize = 8;
        public Rectangle Bounds { get; set; }
        public bool Visible { get; set; } = false;

        public SelectionFrame() { }

        public void Draw(Graphics g)
        {
            if (!Visible) return;

            using (var pen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                g.DrawRectangle(pen, Bounds);

            Brush brush = new SolidBrush(Color.Blue);
            try
            {
                g.FillRectangle(brush, Bounds.X - MarkerSize / 2, Bounds.Y - MarkerSize / 2, MarkerSize, MarkerSize);
                g.FillRectangle(brush, Bounds.Right - MarkerSize / 2, Bounds.Y - MarkerSize / 2, MarkerSize, MarkerSize);
                g.FillRectangle(brush, Bounds.X - MarkerSize / 2, Bounds.Bottom - MarkerSize / 2, MarkerSize, MarkerSize);
                g.FillRectangle(brush, Bounds.Right - MarkerSize / 2, Bounds.Bottom - MarkerSize / 2, MarkerSize, MarkerSize);
                g.FillRectangle(brush, Bounds.X + Bounds.Width / 2 - MarkerSize / 2, Bounds.Y - MarkerSize / 2, MarkerSize, MarkerSize);
                g.FillRectangle(brush, Bounds.X + Bounds.Width / 2 - MarkerSize / 2, Bounds.Bottom - MarkerSize / 2, MarkerSize, MarkerSize);
                g.FillRectangle(brush, Bounds.X - MarkerSize / 2, Bounds.Y + Bounds.Height / 2 - MarkerSize / 2, MarkerSize, MarkerSize);
                g.FillRectangle(brush, Bounds.Right - MarkerSize / 2, Bounds.Y + Bounds.Height / 2 - MarkerSize / 2, MarkerSize, MarkerSize);
            }
            finally { brush.Dispose(); }
        }

        public void Update(Figure figure)
        {
            if (figure != null) { Bounds = figure.Bounds; Visible = true; }
            else Visible = false;
        }
    }
}
