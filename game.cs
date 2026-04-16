using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace balato
{
    class Game
    {
        private Deck deck = new Deck();
        private Hand hand = new Hand();
        private int totalScore;

        public void Start()
        {
            Console.WriteLine("=== Balatro Simple ===");

            totalScore = 0;

            for (int round = 1; round <= 3; round++)
            {
                Console.WriteLine($"--- Round {round} ---");
                PlayRound();
            }

            Console.WriteLine($"Total score: {totalScore}");
        }

        private void PlayRound()
        {
            deck = new Deck();
            deck.shuffel();

            hand = new Hand();

            for (int i = 0; i < 5; i++)
            {
                hand.AddCard(deck.Draw());
            }

            Console.WriteLine("Your hand:");
            hand.ShowHand();

            string handType = EvaluateHand();
            int score = CalculateScore(handType);

            Console.WriteLine($"Hand type: {handType}");
            Console.WriteLine($"Round score: {score}");

            totalScore += score;
        }

        private string EvaluateHand()
        {
            var comb = new Combinatie();

            if (comb.HasThreeOfAKind(hand.Cards))
                return "Three of a Kind";
            else if (comb.HasTwoPair(hand.Cards))
                return "Two Pair";
            else if (comb.HasPair(hand.Cards))
                return "Pair";
            else
                return "High Card";
        }

        private int CalculateScore(string handType)
        {
            switch (handType)
            {
                case "Three of a Kind":
                    return 30;
                case "Two Pair":
                    return 20;
                case "Pair":
                    return 10;
                default:
                    return 5;
            }
        }
    }
}