using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CinkciarzCoin
{
    class Coin
    {
        public double dPurchasePrice { get; private set; }
        public double dSalesPrice { get; private set; }
        public DateTime dtDateOfRate { get; private set; }

        public Coin(double PurchasePrice, double SalesPrice)
        {
            this.dPurchasePrice = PurchasePrice;
            this.dSalesPrice = SalesPrice;
            this.dtDateOfRate = DateTime.Now;
        }

        public override string ToString()
        {
            return  "Purchase price of Cinkciarz Coin is " + dPurchasePrice.ToString("0.0000") + ", Sales price is " + dSalesPrice.ToString("0.0000") + ", Date " + dtDateOfRate.ToString("yyyy-MM-dd HH:mm");
        }
    }
}