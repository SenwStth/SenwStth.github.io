using System;
using System.Drawing;

namespace тема_7
{
    [Serializable]
    public class MyRectangle : Figure
    {
        public MyRectangle(Point location, Size size, Stroke stroke) : base(location, size, stroke) { }

        public override void Draw(Graphics g)
        {
            var rect = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);

            if (Stroke.HasFill && Stroke.FillColor != Color.Transparent)
                using (var brush = new SolidBrush(Stroke.FillColor)) g.FillRectangle(brush, rect);

            using (var pen = Stroke.GetPen()) g.DrawRectangle(pen, rect);
        }
    }
}
