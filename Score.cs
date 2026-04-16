using System;
using System.Collections.Generic;
using System.Text;

namespace balato
{
     class Score
    {
        public int Chips { get; set; }
        public int Multiplier { get; set; }

        public Score()
        {
            Chips = 0;
            Multiplier = 1;
        }
        public int Calculatescore()
        {
            return Chips * Multiplier;
        }

        public void Reset()
        {
            Chips = 0;
            Multiplier = 1;
        }

        
    }
}
