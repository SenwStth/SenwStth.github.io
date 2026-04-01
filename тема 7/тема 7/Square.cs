using System;
using System.Drawing;

namespace тема_7
{
    [Serializable]
    public class Square : Figure
    {
        public Square(Point location, Size size, Stroke stroke) : base(location, size, stroke) { }

        public override void Draw(Graphics g)
        {
            int side = Math.Min(Size.Width, Size.Height);
            var rect = new Rectangle(Location.X, Location.Y, side, side);

            if (Stroke.HasFill && Stroke.FillColor != Color.Transparent)
                using (var brush = new SolidBrush(Stroke.FillColor)) g.FillRectangle(brush, rect);

            using (var pen = Stroke.GetPen()) g.DrawRectangle(pen, rect);
        }

        public override void Resize(int w, int h)
        {
            int side = Math.Max(w, h);
            Size = new Size(side, side);
        }

        public override bool ContainsPoint(Point point)
        {
            int side = Math.Min(Size.Width, Size.Height);
            return new Rectangle(Location.X, Location.Y, side, side).Contains(point);
        }
    }
}
