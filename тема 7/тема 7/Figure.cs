using System;
using System.Drawing;

namespace тема_7
{
    [Serializable]
    public abstract class Figure
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Stroke Stroke { get; set; }
        public int Id { get; set; }

        protected Figure(Point location, Size size, Stroke stroke)
        {
            Location = location;
            Size = size;
            Stroke = stroke?.Clone() ?? new Stroke();
            Id = GetHashCode();
        }

        public Rectangle Bounds => new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
        public abstract void Draw(Graphics g);
        public virtual void Move(int dx, int dy) => Location = new Point(Location.X + dx, Location.Y + dy);

        public void ShiftRight(int pixels) => Move(pixels, 0);
        public void ShiftLeft(int pixels) => Move(-pixels, 0);
        public void ShiftUp(int pixels) => Move(0, -pixels);
        public void ShiftDown(int pixels) => Move(0, pixels);

        public virtual bool ContainsPoint(Point point) => Bounds.Contains(point);
        public virtual Figure Clone() { var f = (Figure)MemberwiseClone(); f.Stroke = Stroke.Clone(); return f; }
        public virtual void Resize(int w, int h) => Size = new Size(Math.Max(20, w), Math.Max(20, h));

        public void AlignLeft()
        {
            if (Id > 0)
            {
                int minX = Id;
                Location = new Point(minX, Location.Y);
            }
        }

        public void AlignRight(int canvasWidth)
        {
            Location = new Point(canvasWidth - Size.Width, Location.Y);
        }
    }
}
