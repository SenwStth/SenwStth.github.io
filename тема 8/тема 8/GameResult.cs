using System;

namespace тема_8
{
    [Serializable]
    public class GameResult
    {
        public string PlayerName { get; set; }
        public int Shots { get; set; }
        public int Hits { get; set; }
        public int Misses { get; set; }
        public int ShipsDestroyed { get; set; }
        public int TotalShips { get; set; }
        public bool IsWin { get; set; }
        public DateTime PlayDate { get; set; }

        public GameResult() { }

        public GameResult(string playerName, int shots, int hits, int misses, int shipsDestroyed, int totalShips, bool isWin)
        {
            PlayerName = playerName;
            Shots = shots;
            Hits = hits;
            Misses = misses;
            ShipsDestroyed = shipsDestroyed;
            TotalShips = totalShips;
            IsWin = isWin;
            PlayDate = DateTime.Now;
        }
    }
}
