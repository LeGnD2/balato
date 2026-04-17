using System;
using System.Collections.Generic;
using System.Text;

namespace balato
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.shuffel();

            PlayerHand hand = new PlayerHand(5);
            for (int i = 0; i < 5; i++)
                hand.AddCard(deck.TakeCard());

            Model model = new Model(deck, hand);

            ViewModel viewModel = new ViewModel(model);
            viewModel.UpdateFromModel();
            viewModel.Run();
        }
    }
}