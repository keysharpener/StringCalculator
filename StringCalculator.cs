using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorTest
{
    public class StringCalculator
    {
        public static int Add(string numbers)
        {
            var integers = ExtractNumbersFromStringNumbers(numbers);
            var allowedIntegers = integers.Where(number=>number <=1000).ToList();

            var negativeNumbers = allowedIntegers.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new Exception($"negatives not allowed: {String.Join(",",negativeNumbers)}" );
            }
            return allowedIntegers.Sum();
        }

        private static IEnumerable<int> ExtractNumbersFromStringNumbers(string inputStringNumbers)
        {
            string[] delimiters = {",","\n"};
            string numbersLine = inputStringNumbers;
            if (inputStringNumbers.StartsWith("//"))
            {
                var delimiterLine = inputStringNumbers.Split('\n')[0];
                delimiters = delimiterLine.Split(new[] { "[", "]", "//" }, StringSplitOptions.RemoveEmptyEntries);
                numbersLine = inputStringNumbers.Replace(delimiterLine, String.Empty);
            }
            var elements = numbersLine.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return elements.Select(int.Parse);
        }
    }
}