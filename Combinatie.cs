using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace balato
{
    class Combinatie
    {
        public bool HasPair(List<Card> Cards)
        {
            var counts = new Dictionary<CardValue, int>();
            foreach (var c in Cards)
            {
                if (counts.ContainsKey(c.Value))
                    counts[c.Value]++;
                else
                    counts[c.Value] = 1;
            }

            foreach (var kv in counts)
            {
                if (kv.Value == 2) return true;
            }
            return false;
        }

        public bool HasTwoPair(List<Card> Cards)
        {
            var counts = new Dictionary<CardValue, int>();
            foreach (var c in Cards)
            {
                if (counts.ContainsKey(c.Value))
                    counts[c.Value]++;
                else
                    counts[c.Value] = 1;
            }

            int pairCount = 0;
            foreach (var kv in counts)
            {
                if (kv.Value == 2) pairCount++;
            }
            return pairCount == 2;
        }

        public bool HasThreeOfAKind(List<Card> Cards)
        {
            var counts = new Dictionary<CardValue, int>();
            foreach (var c in Cards)
            {
                if (counts.ContainsKey(c.Value))
                    counts[c.Value]++;
                else
                    counts[c.Value] = 1;
            }

            foreach (var kv in counts)
            {
                if (kv.Value == 3) return true;
            }
            return false;
        }
    }
}
