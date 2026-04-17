namespace balato
{
    internal class Hand : PlayerHand
    {
        public Hand() : base(5) { }

        public Hand(int maxCards) : base(maxCards) { }
    }
}