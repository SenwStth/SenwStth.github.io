using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace тема_7
{
    [Serializable]
    public class ChamferedRectangle : Figure
    {
        public int ChamferSize { get; set; } = 20;
        public int Type { get; set; } = 0;

        public ChamferedRectangle(Point location, Size size, Stroke stroke) : base(location, size, stroke) { }

        public override void Draw(Graphics g)
        {
            int w = Size.Width;
            int h = Size.Height;
            int c = Math.Min(ChamferSize, Math.Min(w, h) / 2);
            var path = new GraphicsPath();

            if (Type == 0)
            {
                path.AddLine(Location.X + c, Location.Y, Location.X + w - c, Location.Y);
                path.AddLine(Location.X + w, Location.Y + c, Location.X + w, Location.Y + h - c);
                path.AddLine(Location.X + w - c, Location.Y + h, Location.X + c, Location.Y + h);
                path.AddLine(Location.X, Location.Y + h - c, Location.X, Location.Y + c);
            }
            else if (Type == 1)
            {
                path.AddLine(Location.X + c, Location.Y, Location.X + w - c, Location.Y);
                path.AddLine(Location.X + w, Location.Y, Location.X + w, Location.Y + h);
                path.AddLine(Location.X + w - c, Location.Y + h, Location.X + c, Location.Y + h);
                path.AddLine(Location.X, Location.Y + h, Location.X, Location.Y);
            }
            else
            {
                path.AddLine(Location.X, Location.Y + c, Location.X + w, Location.Y + c);
                path.AddLine(Location.X + w, Location.Y + c, Location.X + w, Location.Y + h - c);
                path.AddLine(Location.X + w, Location.Y + h - c, Location.X, Location.Y + h - c);
                path.AddLine(Location.X, Location.Y + h - c, Location.X, Location.Y + c);
            }
            path.CloseFigure();

            if (Stroke.HasFill && Stroke.FillColor != Color.Transparent)
                using (var brush = new SolidBrush(Stroke.FillColor)) g.FillPath(brush, path);

            using (var pen = Stroke.GetPen()) g.DrawPath(pen, path);
            path.Dispose();
        }
    }
}
