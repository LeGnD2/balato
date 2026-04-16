using System;
using System.Collections.Generic;
using System.Text;

namespace balato.combi
{
    class ThreeOfAKind
    {
        public bool Check(List<Card> Cards)
        {
            int pairCount = 0;

            for (int i = 0; i < Cards.Count; i++)
            {
                for (int j = i + 1; j < Cards.Count; j++)
                {
                    if (Cards[i].Value == Cards[j].Value)
                    {
                        pairCount++;
                    }
                }
            }

            return pairCount == 3;
        }
    }
}