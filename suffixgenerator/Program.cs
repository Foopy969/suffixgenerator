using System;
using System.Linq;

namespace suffixgenerator
{
    class Program
    {
        private static readonly string[] UNIT = { "", "m", "b", "tr", "quadr", "quint", "sext", "sept", "oct", "non" };
        private static readonly string[] UNITS = { "", "un", "duo", "tre", "quattuor", "quin", "sex", "septen", "octo", "novem"};
        private static readonly string[] TENS = { "", "dec", "vigint", "trigint", "quadragint", "quinquagint", "sexagint", "septuagint", "octogint", "nonagint"};
        private static readonly string[] HUNDREDS = { "", "centi", "ducent", "tricent", "quadringent", "quingent", "sescent", "septingent", "octigent", "nongent" };

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Too little parameter");
                return;
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("Too many parameter");
                return;
            }
            else
            {
                double result;
                if (double.TryParse(args[0], out result))
                {
                    Console.WriteLine(Format(result));
                }
                else
                {
                    Console.WriteLine("Not a number");
                }
            }
        }

        private static string Format (double number)
        {
            if (number < 1000.0)
            {
                return number.ToString();
            }
            int num = (int)Math.Log(number, 1000.0);
            return (number / Math.Pow(1000.0, num)).ToString("###.000 ") + GetSuffix(num);
        }

        private static string GetSuffix (int n)
        {
            if (n == 0)
                return "";
            else if (n == 1)
                return "Thousand";
            else
                return string.Concat(GetSuffixHidden(n - 1).Select((x, i) => i == 0 ? char.ToUpper(x) : x)) + "ion";
        }

        private static string GetSuffixHidden (int n)
        {
            if (n < 10)
                return UNIT[n] + "ill";
            else if (n < 1000)
                return UNITS[n % 10] + TENS[n % 100 / 10] + HUNDREDS[n / 100] + "ill";
            else
                return GetSuffixHidden(n % 1000) + "ill";
        }
    }
}
