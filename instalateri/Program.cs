using System;
using System.IO.Compression;

class Program
{
    static bool stena(int[] bod, int hrana)
    {
        if (((bod[0] == 0 || bod[0] == hrana) && bod[1] >= 20 && bod[1] <= hrana - 20 && bod[2] >= 20 && bod[2] <= hrana - 20) ||
            ((bod[1] == 0 || bod[1] == hrana) && bod[0] >= 20 && bod[0] <= hrana - 20 && bod[2] >= 20 && bod[2] <= hrana - 20) ||
            ((bod[2] == 0 || bod[2] == hrana) && bod[1] >= 20 && bod[1] <= hrana - 20 && bod[0] >= 20 && bod[0] <= hrana - 20))
        {
            return true;
        }

        return false;
    }

    static int minimumPotrubi(int[] seznam, int pocet, bool chciIndex)
    {
        int minimum = seznam[0];
        int minIn = 0;
        for (int i = 0; i < pocet; i++)
        {
            if (seznam[i] < minimum)
            {
                minimum = seznam[i];
                minIn = i;
            }
        }

        return chciIndex ? minIn : minimum;
    }

    static double minimumHadice(double[] seznam, int pocet, bool chciIndex)
    {
        double minimum = seznam[0];
        int min_in = 0;
        for (int i = 0; i < pocet; i++)
        {
            if (seznam[i] < minimum)
            {
                minimum = seznam[i];
                min_in = i;
            }
        }

        return chciIndex ? min_in : minimum;
    }

    static void Main()
    {
        int[][] body = new int[2][];
        for (int i = 0; i < 2; i++)
        {
            body[i] = new int[3];
        }

        int hrana, protejsi = 0, potrubi = 0;
        double hadice = 0;

        Console.WriteLine("Rozmer mistnosti:");
        if (!int.TryParse(Console.ReadLine(), out hrana) || hrana < 0)
        {
            Console.WriteLine("Nespravny vstup.");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine($"Bod #{i + 1}:");
            string[] input = Console.ReadLine().Split();
            if (input.Length != 3 || !int.TryParse(input[0], out body[i][0]) || !int.TryParse(input[1], out body[i][1]) || !int.TryParse(input[2], out body[i][2]) || stena(body[i], hrana) == false)
            {
                Console.WriteLine("Nespravny vstup.");
                return;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if ((body[0][i] == 0 && body[1][i] == hrana) ||
               (body[0][i] == hrana && body[1][i] == 0))
            {
                protejsi = 1;
                break;
            }
        }

        int stn = 0;

        if (protejsi == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (body[0][i] == hrana || body[0][i] == 0)
                {
                    stn = i;
                    break;
                }
            }

            int[][][] delky = new int[3][][];
            for (int i = 0; i < 3; i++)
            {
                delky[i] = new int[2][];
                for (int j = 0; j < 2; j++)
                {
                    delky[i][j] = new int[4];
                }
            }

            delky[0][0] = new int[]
            {
                hrana - body[0][1] + hrana + hrana - body[1][1],
                hrana - body[0][2] + hrana + hrana - body[1][2],
                body[0][1] + hrana + body[1][1],
                body[0][2] + hrana + body[1][2]
            };

            delky[0][1] = new int[]
            {
                Math.Abs(body[0][2] - body[1][2]), Math.Abs(body[0][1] - body[1][1]), Math.Abs(body[0][2] - body[1][2]), Math.Abs(body[0][1] - body[1][1])
            };

            delky[1][0] = new int[]
            {
                hrana - body[0][0] + hrana + hrana - body[1][0],
                hrana - body[0][2] + hrana + hrana - body[1][2],
                body[0][2] + hrana + body[1][2],
                body[0][0] + hrana + body[1][0] 
            };

            delky[1][1] = new int[]
            {
                Math.Abs(body[0][2] - body[1][2]), Math.Abs(body[0][0] - body[1][0]), Math.Abs(body[0][0] - body[1][0]), Math.Abs(body[0][2] - body[1][2])
            };

            delky[2][0] = new int[]
            {
                hrana - body[0][1] + hrana + hrana - body[1][1], 
                hrana - body[0][0] + hrana + hrana - body[1][0],
                body[0][1] + hrana + body[1][1], 
                body[0][0] + hrana + body[1][0]
            };

            delky[2][1] = new int[]
            {
                Math.Abs(body[0][0] - body[1][0]), Math.Abs(body[0][1] - body[1][1]), Math.Abs(body[0][0] - body[1][0]), Math.Abs(body[0][1] - body[1][1])
            };

            int[] c = new int[]
            {
                delky[stn][0][0] + delky[stn][1][0], delky[stn][0][1] + delky[stn][1][1], delky[stn][0][2] + delky[stn][1][2], delky[stn][0][3] + delky[stn][1][3]
            };

            double[] t = new double[]
            {
                Math.Sqrt(Math.Pow(delky[stn][0][0], 2.0) + Math.Pow(delky[stn][1][0], 2.0)),
                Math.Sqrt(Math.Pow(delky[stn][0][1], 2.0) + Math.Pow(delky[stn][1][1], 2.0)),
                Math.Sqrt(Math.Pow(delky[stn][0][2], 2.0) + Math.Pow(delky[stn][1][2], 2.0)),
                Math.Sqrt(Math.Pow(delky[stn][0][3], 2.0) + Math.Pow(delky[stn][1][3], 2.0))
            };

            potrubi = minimumPotrubi(c, 4, false);
            hadice = minimumHadice(t, 4, false);
        }
        else
        {
            if (body[0][0] != 0 && body[0][0] != hrana && body[1][0] != 0 && body[1][0] != hrana)
                hadice = Math.Sqrt((body[1][0] - body[0][0]) * (body[1][0] - body[0][0]) + (Math.Abs(body[1][1] - body[0][1]) + Math.Abs(body[1][2] - body[0][2])) * (Math.Abs(body[1][1] - body[0][1]) + Math.Abs(body[1][2] - body[0][2])));

            else if (body[1][1] != 0 && body[1][1] != hrana && body[0][1] != 0 && body[0][1] != hrana)
                hadice = Math.Sqrt((body[1][1] - body[0][1]) * (body[1][1] - body[0][1]) + (Math.Abs(body[1][0] - body[0][0]) + Math.Abs(body[1][2] - body[0][2])) * (Math.Abs(body[1][0] - body[0][0]) + Math.Abs(body[1][2] - body[0][2])));

            else if (body[1][2] != 0 && body[1][2] != hrana && body[0][2] != 0 && body[0][2] != hrana)
                hadice = Math.Sqrt((body[1][2] - body[0][2]) * (body[1][2] - body[0][2]) + (Math.Abs(body[1][0] - body[0][0]) + Math.Abs(body[1][1] - body[0][1])) * (Math.Abs(body[1][0] - body[0][0]) + Math.Abs(body[1][1] - body[0][1])));

            potrubi = Math.Abs(body[0][0] - body[1][0]) + Math.Abs(body[0][1] - body[1][1]) + Math.Abs(body[0][2] - body[1][2]);
        }

        Console.WriteLine($"Delka potrubi: {potrubi}\nDelka hadice: {hadice}");
        Console.ReadKey();
    }
}