using System;

class Program
{
    static void Main()
    {
        double[][] points = { new double[] { 10, 10 }, new double[] { 0, 10 }, new double[] { 10, 0 } };

        double u1 = Dist(points[0], points[1]);
        double u2 = Dist(points[1], points[2]);
        double u3 = Dist(points[0], points[2]);

        int middle;
        string middlepoint;

        if (u1 >= u2 && u1 >= u3)
        {
            middle = 2;
            middlepoint = "C";
        }
        else if (u2 >= u1 && u2 >= u3)
        {
            middle = 0;
            middlepoint = "A";
        }
        else
        {
            middle = 1;
            middlepoint = "B";
        }

        if (middle == 0)
        {
            if (u1 + u3 == u2)
            {
                Console.WriteLine("Body jsou v přímce a bod uprostřed je: " + middlepoint);
            }
            else
            {
                Console.WriteLine("Body nejsou v přímce");
            }
        }
        else if (middle == 1)
        {
            if (u1 + u2 == u3)
            {
                Console.WriteLine("Body jsou v přímce a bod uprostřed je: " + middlepoint);
            }
            else
            {
                Console.WriteLine("Body nejsou v přímce");
            }
        }
        else
        {
            if (u2 + u3 == u1)
            {
                Console.WriteLine("Body jsou v přímce a bod uprostřed je: " + middlepoint);
            }
            else
            {
                Console.WriteLine("Body nejsou v přímce");
            }
        }
    }

    static double Dist(double[] a, double[] b)
    {
        return Math.Sqrt(Math.Pow(a[0] - b[0], 2) + Math.Pow(a[1] - b[1], 2));
    }
}