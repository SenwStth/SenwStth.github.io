using System;
using System.Collections.Generic;
using System.Drawing;

namespace тема_8
{
    [Serializable]
    public class Ship
    {
        public List<Point> Cells { get; set; } = new List<Point>();
        public int Size { get; set; }
        public bool IsDestroyed { get; set; }
        public Color Color { get; set; } = Color.DarkBlue;

        public Ship() { }

        public Ship(List<Point> cells)
        {
            Cells = cells;
            Size = cells.Count;
            IsDestroyed = false;
        }

        public bool ContainsCell(int row, int col)
        {
            foreach (var cell in Cells)
            {
                if (cell.X == row && cell.Y == col)
                    return true;
            }
            return false;
        }

        public void CheckDestroyed()
        {
            IsDestroyed = true;
        }
    }
}
