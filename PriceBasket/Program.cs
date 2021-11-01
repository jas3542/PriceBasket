using System;
using System.Collections.Generic;
using System.Linq;

namespace PriceBasket
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new Dictionary<string, IList<Item>>();
            ILogsService logsService = new LogsService();
            IPricingCalculatorService princingCalculator = new PricingCalculatorService(logsService);

            if (args.Length > 0)
            {
                // Build a Dictionary to use later on:
                items = buildDictionaryOfProducts(args);
                
                // Calculates the subTotal and Total after applying the discounts:
                princingCalculator.CalculatePrice(items);
            
                // Logs to Console:
                var consoleLogs = logsService.GetLogs();
                foreach(var log in consoleLogs)
                {
                    Console.WriteLine(log);
                }
            }
            else
            {
                Console.WriteLine("Basket is empty");
            }
            Console.ReadLine();
        }

        private static Dictionary<string, IList<Item>> buildDictionaryOfProducts(string[] args)
        {
            var items = new Dictionary<string, IList<Item>>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "beans")
                {
                    if (!items.ContainsKey("beans"))
                    {
                        items.Add("beans", new List<Item> { new Bean() });
                    }
                    else
                    {
                        items["beans"].Add(new Bean());
                    }

                }
                if (args[i].ToLower() == "bread")
                {
                    if (!items.ContainsKey("bread"))
                    {
                        items.Add("bread", new List<Item> { new Bread() });
                    }
                    else
                    {
                        items["bread"].Add(new Bread());
                    }
                }
                if (args[i].ToLower() == "milk")
                {
                    if (!items.ContainsKey("milk"))
                    {
                        items.Add("milk", new List<Item> { new Milk() });
                    }
                    else
                    {
                        items["milk"].Add(new Milk());
                    }
                }
                if (args[i].ToLower() == "oranges")
                {
                    if (!items.ContainsKey("oranges"))
                    {
                        items.Add("oranges", new List<Item> { new Orange() });
                    }
                    else
                    {
                        items["oranges"].Add(new Orange());
                    }
                }
            }

            return items;
        }
    }
}
