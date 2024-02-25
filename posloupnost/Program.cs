using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static List<Tuple<int, int, int>> FindIntervalSums(List<int> input)
    {
        List<Tuple<int, int, int>> intervals = new List<Tuple<int, int, int>>();
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = i + 1; j <= input.Count; j++)
            {
                if (j - i >= 2)
                {
                    List<int> interval = input.GetRange(i, j - i);
                    int intervalSum = interval.Sum();
                    intervals.Add(new Tuple<int, int, int>(i, j - 1, intervalSum));
                }
            }
        }
        return intervals;
    }

    static int FindSameSumPairs(List<Tuple<int, int, int>> intervals)
    {
        int count = 0;
        for (int i = 0; i < intervals.Count; i++)
        {
            for (int j = i + 1; j < intervals.Count; j++)
            {
                if (intervals[i].Item3 == intervals[j].Item3)
                {
                    count++;
                }
            }
        }
        return count;
    }

    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.Write("Zadejte řadu čísel rozdělené mezerou: ");
                string userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    throw new ArgumentException("prázdný vstup");
                }

                List<int> input = userInput.Split(' ').Select(int.Parse).ToList();

                if (input.Count > 2000)
                {
                    throw new ArgumentException("moc dlouhá řada, zadejte prosím řadu kratší než 2000 znaků");
                }

                List<Tuple<int, int, int>> intervals = FindIntervalSums(input);
                int pairs = FindSameSumPairs(intervals);

                Console.WriteLine("výsledek: " + pairs);
                break;
            }
            catch (ArgumentException e)
            {
                if (e.Message.Contains("vstup"))
                {
                    Console.WriteLine(e.Message);
                }
                else
                {
                    Console.WriteLine("zadávejte pouze čísla");
                }
            }
        }
    }
}
