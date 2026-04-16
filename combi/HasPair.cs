using System;
using System.Collections.Generic;
using System.Text;

namespace balato.combi
{
    public class OnePair
    {
        internal static bool Check(List<Card> Cards)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                for (int j = i + 1; j < Cards.Count; j++)
                {
                    if (Cards[i].Value == Cards[j].Value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}