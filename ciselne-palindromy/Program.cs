using System;

public class Program
{
    public static bool IsPalindrome(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        string reversed = new string(charArray);
        return s == reversed;
    }

    public static int FindNextPalindrome(int number, int baseValue)
    {
        while (true)
        {
            number++;
            string convertedNumber = baseValue == 2 ? Convert.ToString(number, 2) : number.ToString();

            if (IsPalindrome(convertedNumber))
            {
                return number;
            }
        }
    }

    public static void Main(string[] args)
    {
        try
        {
            Console.Write("Zadejte číslo: ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Zadejte číselnou soustavu. 2 pro binární soustavu, 10 pro decimální: ");
            int baseValue = Convert.ToInt32(Console.ReadLine());

            if (number < 0 || baseValue < 2)
            {
                Console.WriteLine("Prosím zadejte pozitivní číslo se základem vyšším než 2.");
                return;
            }

            int nextPalindrome = FindNextPalindrome(number, baseValue);

            Console.WriteLine($"Nejbližší vyšší palindromické číslo v soustavě {baseValue} je: {nextPalindrome}");
        }
        catch (FormatException)
        {
            Console.WriteLine("neplatne");
        }
    }
}