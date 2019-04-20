using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinkciarzCoin.Model
{
    class CurrencyGenerator
    {
        static public double dMaxSpread = 0.4;
        static public double dMeanExchangeRate = 3.5;
        static public int iNumberOfGenerationPerSec = 1;


        public Dictionary<string, double> GenerateCurrencyRate()
        {
            Random rand = new Random();
            double purchaseRate = rand.NextDouble() * dMaxSpread + (dMeanExchangeRate - dMaxSpread);
            double salesRate = rand.NextDouble() * ((purchaseRate + dMaxSpread) - dMeanExchangeRate) + dMeanExchangeRate;
            return new Dictionary<string, double> { { "Purchase", purchaseRate }, { "Sales", salesRate } };
        }
    }
}
