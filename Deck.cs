using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;

namespace balato
{
    class Deck
    {
        private List<Card> Cards;
        private List<Card> CardsTaken;

        public int CardsTotalCount { get { return this.Cards.Count + this.CardsTaken.Count; } }
        public int CardsRemainingCount { get { return this.Cards.Count; } }


        public Deck()
        {
            this.Cards = new List<Card>();
            this.CardsTaken = new List<Card>();


            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Card card = new Card(value, suit);
                    this.Cards.Add(card);
                    //Console.WriteLine(card.Suit.ToString() + "" + card.Value.ToString());

                }
            }
        }
        public Card TakeCard()
            {
            if(this.Cards.Count == 0) { 

            }

          Card taken = this.Cards.First();  
            this.Cards.RemoveAt(0);
            this.CardsTaken.Add(taken);
            return taken;


              }




            public void Reset()
        {
            this.Cards=
                this.Cards
                .Concat(this.CardsTaken)
                .ToList();
            this.CardsTaken = new List<Card>();
        }
        public void shuffel()
        {
            this.Cards = this.Cards .Shuffle().ToList();
        }

        public int AantalKaarten()
        {
            return this.Cards.Count();
        }

        internal Card Draw()
        {
            throw new NotImplementedException();
        }
    }
}

