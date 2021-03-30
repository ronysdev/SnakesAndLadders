using System;

namespace Model.Data.Dice
{
    public class Dice : IDice
    {
        private readonly Random _random;

        public Dice()
        {
            _random = new Random();
        }

        public int Roll()
        {
            return _random.Next(1, 7);
        }
    }
}
