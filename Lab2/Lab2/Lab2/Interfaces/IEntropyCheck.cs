using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    interface IEntropyCheck
    {
        public void getSymbolsCounts(string text, Dictionary<char, int> alphabet);
        public Dictionary<char, double> getSymbolsChances(string text, Dictionary<char, int> counts);
        public void computeTextEntropy(Dictionary<char, double> chances);



    }
}
