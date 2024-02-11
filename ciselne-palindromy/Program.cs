using System;

class Program
{

    static bool IsPalindrome(string num)
    {
        char[] charArray = num.ToCharArray();
        Array.Reverse(charArray);
        string reversedNum = new string(charArray);
        return num == reversedNum;
    }

    static string ConvertNumber(ulong number, int radix)
    {
        if (number == 0)
            return "0";

        string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string result = "";

        while (number > 0)
        {
            ulong remainder = number % (ulong)radix;
            result = digits[(int)remainder] + result;
            number /= (ulong)radix;
        }

        return result;
    }

    static int NextPalindrome(ulong number, int radix, ulong[] result)
    {
        if (number < 0 || radix < 2 || radix > 36)
            return 0;

        while (true)
        {
            number++;
            if (number > ulong.MaxValue)
                return 0;

            string convertedNumber = ConvertNumber(number, radix);
            if (IsPalindrome(convertedNumber))
            {
                result[0] = number;
                return 1;
            }
        }
    }

    static void Main(string[] args)
    {
        ulong[] next = new ulong[1];
        if (NextPalindrome(123, 10, next) == 1 && next[0] == 131 &&
            NextPalindrome(188, 10, next) == 1 && next[0] == 191 &&
            NextPalindrome(1441, 10, next) == 1 && next[0] == 1551 &&
            NextPalindrome(95, 15, next) == 1 && next[0] == 96 &&
            NextPalindrome(45395, 36, next) == 1 && next[0] == 45431 &&
            NextPalindrome(1027, 2, next) == 1 && next[0] == 1057 &&
            NextPalindrome(1000, 100, next) == 0 && next[0] == 1057 &&
            NextPalindrome(18446744073709551614, 2, next) == 1 && next[0] == 18446744073709551615 &&
            NextPalindrome(18446744073709551615, 2, next) == 0 && next[0] == 18446744073709551615)
        {
            Console.WriteLine("All assertions passed!");
        }
    }
}