using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    class Regal
    {
        public int Number { get; set; }
        public List<string> Items { get; set; } = new List<string>();
    }

    class ShoppingItem
    {
        public string OriginalName { get; set; }
        public int RegalNumber { get; set; } = -1;
        public string SupermarketName { get; set; } = "";
    }

    static void Main(string[] args)
    {
        var regals = new List<Regal>();
        string fileName = "0001_in.txt";
        string[] lines = File.ReadAllLines(fileName);
        bool readingRegals = true;

        int lineIndex = 0;


        while (lineIndex < lines.Length && lines[lineIndex] != "")
        {
            if (lines[lineIndex].StartsWith("#"))
            {
                if (!readingRegals)
                {
                    Console.WriteLine("Chyba: Očekává se prázdný řádek po regálech.");
                    return;
                }
                if (int.TryParse(lines[lineIndex].Substring(1), out int regalNumber))
                {
                    regals.Add(new Regal { Number = regalNumber });
                }
                else
                {
                    Console.WriteLine("Chyba: Neplatné číslo regálu.");
                    return;
                }
            }
            else
            {
                if (regals.Count == 0)
                {
                    Console.WriteLine("Chyba: Chybí číslo regálu.");
                    return;
                }
                regals.Last().Items.Add(lines[lineIndex]);
            }
            lineIndex++;
        }

        if (regals.Count == 0 || regals.First().Number != 0)
        {
            Console.WriteLine("Chyba: Neplatné číslování regálů.");
            return;
        }

        for (int i = 0; i < regals.Count; i++)
        {
            if (regals[i].Number != i)
            {
                Console.WriteLine("Chyba: Regály netvoří posloupnost.");
                return;
            }
        }

        if (lineIndex >= lines.Length || lines[lineIndex] != "")
        {
            Console.WriteLine("Chyba: Chybí prázdný řádek po regálech.");
            return;
        }

        lineIndex++;

        var shoppingLists = new List<List<string>>();
        var currentList = new List<string>();

        while (lineIndex < lines.Length)
        {
            if (lines[lineIndex] == "")
            {
                if (currentList.Count > 0)
                {
                    shoppingLists.Add(currentList);
                    currentList = new List<string>();
                }
            }
            else
            {
                currentList.Add(lines[lineIndex]);
            }
            lineIndex++;
        }

        if (currentList.Count > 0)
        {
            shoppingLists.Add(currentList);
        }

        foreach (var shoppingList in shoppingLists)
        {
            var optimizedList = OptimizeShoppingList(shoppingList, regals);
            foreach (var item in optimizedList)
            {
                Console.WriteLine($"{item.OriginalName} -> {item.RegalNumber}: {item.SupermarketName}");
            }
            Console.WriteLine();
        }
    }

    static List<ShoppingItem> OptimizeShoppingList(List<string> shoppingList, List<Regal> regals)
    {
        var optimizedList = new List<ShoppingItem>();
        var remainingItems = new List<string>(shoppingList);

        foreach (var regal in regals)
        {
            foreach (var regalItem in regal.Items)
            {
                for (int i = 0; i < remainingItems.Count; i++)
                {
                    if (IsMatch(remainingItems[i], regalItem))
                    {
                        optimizedList.Add(new ShoppingItem
                        {
                            OriginalName = remainingItems[i],
                            RegalNumber = regal.Number,
                            SupermarketName = regalItem
                        });
                        remainingItems.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        foreach (var item in remainingItems)
        {
            optimizedList.Add(new ShoppingItem
            {
                OriginalName = item,
                RegalNumber = -1,
                SupermarketName = "Nenalezeno"
            });
        }

        return optimizedList;
    }

    static bool IsMatch(string shoppingItem, string regalItem)
    {
        return regalItem.IndexOf(shoppingItem, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}