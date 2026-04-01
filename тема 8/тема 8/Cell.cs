using System;

namespace тема_8
{
    [Serializable]
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool HasShip { get; set; }
        public bool IsOpened { get; set; }
        public bool IsMarked { get; set; }
        public int ShipIndex { get; set; } = -1;

        public Cell() { }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
            HasShip = false;
            IsOpened = false;
            IsMarked = false;
        }
    }
}
