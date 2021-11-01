using System;
using System.Collections.Generic;
using System.Text;

namespace PriceBasket
{
    public interface IPricingCalculatorService
    {
        public void CalculatePrice(IDictionary<string,IList<Item>> items);
        public decimal getSubTotal();
        public decimal getTotal();
    }
}
