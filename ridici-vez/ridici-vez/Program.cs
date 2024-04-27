using System;
using System.Collections.Generic;

namespace ridici_vez
{
    class Program
    {
        static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        static (double, List<(string, string)>) FindClosestAirplanes(List<(double, double, string)> airplanes)
        {
            double closestDistance = double.PositiveInfinity;
            List<(string, string)> closestPairs = new List<(string, string)>();

            for (int i = 0; i < airplanes.Count; i++)
            {
                for (int j = i + 1; j < airplanes.Count; j++)
                {
                    var (x1, y1, name1) = airplanes[i];
                    var (x2, y2, name2) = airplanes[j];

                    double dist = Distance(x1, y1, x2, y2);

                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        closestPairs = new List<(string, string)> { (name1, name2) };
                    }
                    else if (dist == closestDistance)
                    {
                        closestPairs.Add((name1, name2));
                    }
                }
            }

            return (closestDistance, closestPairs);
        }

        static List<(double, double, string)> ParseInput()
        {
            List<(double, double, string)> airplanes = new List<(double, double, string)>();

            try
            {
                while (true)
                {
                    Console.Write("Napiste souradnice v tomto formatu: (x,y:jmeno), nebo zmacknete enter pro ukonceni zadavani");
                    string userInput = Console.ReadLine();

                    if (userInput.ToLower() == "")
                    {
                        break;
                    }

                    try
                    {
                        string[] parts = userInput.Split(':');
                        string name = parts[1].Trim();
                        string[] coords = parts[0].Split(',');
                        double x = double.Parse(coords[0]);
                        double y = double.Parse(coords[1]);
                        airplanes.Add((x, y, name));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("spatny format souradnic");
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception
            }

            if (airplanes.Count < 2)
            {
                Console.WriteLine("minimalne dve souradnice");
                Environment.Exit(1);
            }

            return airplanes;
        }

        static void Main()
        {
            List<(double, double, string)> airplanes = ParseInput();

            if (airplanes.Count == 0)
            {
                Console.WriteLine("nebyly zadana zadna letadla");
                Environment.Exit(1);
            }

            var (closestDistance, closestPairs) = FindClosestAirplanes(airplanes);

            Console.WriteLine($"vzdalenost nejblizsich letadel: {closestDistance}");
            Console.WriteLine($"nalezenych dvojic: {closestPairs.Count}");

            foreach (var pair in closestPairs)
            {
                Console.WriteLine($"{pair.Item1} - {pair.Item2}");
            }
            Console.ReadKey();
        }
    }
}
