using System;
using System.Diagnostics.CodeAnalysis;

namespace Model.Data.Players
{
    public class Player : IComparable<Player>
    {
        public string PlayerID { get; set; }
        public string PlayerName { get;  set; }
        public int CurrentPosition { get; set; }
        public int CurrentScore { get; set; }
        public PlayerStatus Status { get; set; }
        public bool HighestScore { get; set; }

        public int CompareTo([AllowNull] Player other)
        {
            if (CurrentScore == other.CurrentScore)
                return 0;
            else if (CurrentScore < other.CurrentScore)
                return -1;
            return 1;
        }
    }
}
