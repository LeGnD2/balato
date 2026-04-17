using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace balato
{
    internal class ViewModel
    {
        private Model Model;
        private int DeckcardsTotal, DeckcardsRemaining = 0;
        private IEnumerable<Card> CardsInHand = new List<Card>();
        private IEnumerable<int> SelectedCards = new List<int>();
        private int CursorIndex = 0;
        private int DiscardsLeft = 3;
        private int TotalScore = 0;
        private string Message = "";

        public ViewModel(Model model)
        {
            this.Model = model;
        }

        public void UpdateFromModel()
        {
            this.DeckcardsTotal = this.Model.Deck.CardsTotalCount;
            this.DeckcardsRemaining = this.Model.Deck.CardsRemainingCount;
            this.CardsInHand = this.Model.PlayerHand.CardsInHand;
            this.SelectedCards = this.Model.PlayerHand.SelectedCards;
        }

        public void RenderUI()
        {
            Console.Clear();

            Console.WriteLine("=== BALATO ===");
            Console.WriteLine($"Deck: {this.DeckcardsRemaining}/{this.DeckcardsTotal}  |  Discards left: {DiscardsLeft}  |  Score: {TotalScore}");
            Console.WriteLine();

            for (int i = 0; i < this.CardsInHand.Count(); i++)
            {
                Card card = this.CardsInHand.ElementAt(i);
                string selected = this.SelectedCards.Contains(i) ? "[x]" : "[ ]";
                string cursor = i == CursorIndex ? " > " : "   ";
                Console.WriteLine(cursor + selected + " " + card.MakeAsString());
            }

            Console.WriteLine();
            if (Message != "")
            {
                Console.WriteLine(">> " + Message);
                Console.WriteLine();
            }
            Console.WriteLine("Controls: Up/Down = navigate | Space = select | D = discard & redraw | Enter = play hand");
        }

        public void HandleUserInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.UpArrow)
            {
                if (CursorIndex > 0) CursorIndex--;
                Message = "";
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (CursorIndex < this.CardsInHand.Count() - 1) CursorIndex++;
                Message = "";
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                if (this.SelectedCards.Contains(CursorIndex))
                    this.Model.PlayerHand.DeselectCard(CursorIndex);
                else
                    this.Model.PlayerHand.SelectCard(CursorIndex);

                this.UpdateFromModel();
                Message = "";
            }
            else if (key.Key == ConsoleKey.D)
            {
                if (DiscardsLeft <= 0)
                {
                    Message = "No discards left!";
                }
                else if (!this.SelectedCards.Any())
                {
                    Message = "Select cards to discard first.";
                }
                else
                {
                    int discardCount = this.SelectedCards.Count();
                    this.Model.PlayerHand.RemoveSelected();

                    for (int i = 0; i < discardCount; i++)
                        this.Model.PlayerHand.AddCard(this.Model.Deck.TakeCard());

                    DiscardsLeft--;
                    CursorIndex = 0;
                    this.UpdateFromModel();
                    Message = $"Discarded {discardCount} card(s) and drew new ones.";
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                PlayHand();
            }
        }

        private void PlayHand()
        {
            var comb = new Combinatie();
            var cards = this.CardsInHand.ToList();

            string handType;
            int score;

            if (comb.HasThreeOfAKind(cards)) { handType = "Three of a Kind"; score = 30; }
            else if (comb.HasTwoPair(cards)) { handType = "Two Pair"; score = 20; }
            else if (comb.HasPair(cards)) { handType = "Pair"; score = 10; }
            else { handType = "High Card"; score = 5; }

            TotalScore += score;

            Console.Clear();
            Console.WriteLine("=== HAND RESULT ===");
            Console.WriteLine();
            Console.WriteLine($"Hand: {handType}");
            Console.WriteLine($"Score this round: +{score}");
            Console.WriteLine($"Total score: {TotalScore}");
            Console.WriteLine();
            Console.WriteLine("Press Enter to play next hand, or Q to quit.");

            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    NewHand();
                    break;
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    Console.Clear();
                    Console.WriteLine($"Thanks for playing! Final score: {TotalScore}");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
        }

        private void NewHand()
        {
            if (this.Model.Deck.CardsRemainingCount < 5)
            {
                Console.Clear();
                Console.WriteLine($"Deck is out of cards! Final score: {TotalScore}");
                Console.ReadKey();
                Environment.Exit(0);
            }

            this.Model.PlayerHand = new PlayerHand(5);
            for (int i = 0; i < 5; i++)
                this.Model.PlayerHand.AddCard(this.Model.Deck.TakeCard());

            DiscardsLeft = 3;
            CursorIndex = 0;
            Message = "New hand dealt!";
            this.UpdateFromModel();
        }

        public void Run()
        {
            while (true)
            {
                this.RenderUI();
                this.HandleUserInput();
            }
        }
    }
}