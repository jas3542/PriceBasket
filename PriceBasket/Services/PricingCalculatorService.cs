using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceBasket
{
    public class PricingCalculatorService : IPricingCalculatorService
    {
        private decimal _subTotal;
        private decimal _Total;

        private bool _apply20PercentDiscount;
        private bool _apply50PercentDiscount; // For multi-buy offer
        private decimal _20PercentDiscountAmount;
        private decimal _50PercentDiscountAmount;

        private bool _noDiscount;

        private ILogsService _logsService;

        public PricingCalculatorService() { }
        public PricingCalculatorService(ILogsService logsService)
        {
            _subTotal = 0.00M;
            _Total = 0.00M;

            _noDiscount = true;

            _apply50PercentDiscount = false;
            _apply20PercentDiscount = true;
            _20PercentDiscountAmount = 0.00M;
            _50PercentDiscountAmount = 0.00M;

            _logsService = logsService;
        }

        /// <summary>
        /// Calculates the SubTotal, Amount with discount & Total
        /// </summary>
        /// <param name="items">Dictionary with type of product as key and a list of objects as value</param>
        public void CalculatePrice(IDictionary<string,IList<Item>> items)
        {
            if (items == null)
            {
                _logsService.AddToLogs("Invalid list of items");
            }else if (!items.Values.Any())
            {
                _logsService.AddToLogs("The list of items is empty");
            }else { 
                // Check for multi-buy offer with beans:
                if (items.ContainsKey("beans"))
                {
                    var totalBinTins = items["beans"].Count();
                    if (totalBinTins == 2) { 
                        _apply50PercentDiscount = true;
                    }
                }

                // Calculate subtotal:
                foreach(var i in items)
                {
                    _subTotal += calculateSubTotal(i.Value);
            
                }
                _logsService.AddToLogs($"Subtotal: £{_subTotal.ToString("0.00")}");

                // Apply discounts: 
                foreach(var item in items)
                {
                    applyDiscounts(item.Value);
                }
                if (_noDiscount)
                    _logsService.AddToLogs("(no offers available)");

                _Total = _subTotal - (_20PercentDiscountAmount + _50PercentDiscountAmount);
                _logsService.AddToLogs($"Total: £{_Total.ToString("0.00")}");
            }

        }

        private decimal calculateSubTotal(IList<Item> itemsList)
        {
            decimal result = 0.00M;
            foreach (var i in itemsList)
            {
                result += i.Price;
            }
            
            return result;
        }

        private void applyDiscounts(IList<Item> itemsList)
        {
            bool oranges = false, beans = false, bread = false, milk = false;
            decimal subTotal = 0.00M;

            foreach (var i in itemsList)
            {
                if (i is Orange)
                {
                    oranges = true;
                    subTotal += i.Price;
                }

                if (i is Bread)
                {
                    bread = true;
                    subTotal += i.Price;
                }
            }

            if (oranges)
            {
                if (_apply20PercentDiscount)
                {
                    _noDiscount = false;

                    var discountToApply = apply20PercentDiscount(subTotal);
                    
                    _20PercentDiscountAmount = discountToApply;
                    _logsService.AddToLogs($"Oranges 20% off: -{discountToApply.ToString("0.00")}");
                }
            }

            if (bread)
            {
                if (_apply50PercentDiscount)
                {
                    _noDiscount = false;

                    var discountToApply = apply50PercentDiscount(subTotal);

                    _50PercentDiscountAmount = discountToApply;
                    _logsService.AddToLogs($"Half price loaf of bread with 2 tins of beans: -{discountToApply.ToString("0.00")}");
                }
            }

            
        }

        private decimal apply20PercentDiscount(decimal subTotal)
        {
            return subTotal * 0.20M;
        }

        private decimal apply50PercentDiscount(decimal subTotal)
        {
            return subTotal * 0.50M;
        }

        /// <summary>
        /// Getter for SubTotal
        /// </summary>
        /// <returns>SubTotal amount</returns>
        public decimal getSubTotal()
        {
            return _subTotal;
        }

        /// <summary>
        /// Getter for Total
        /// </summary>
        /// <returns>Total amount</returns>
        public decimal getTotal()
        {
            return _Total;
        }
    }
}
