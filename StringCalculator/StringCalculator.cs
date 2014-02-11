using System;
using System.Collections.Generic;

namespace StringCalculator
{
    public class StringCalculator
    {
        private Char delimiter;

        public Int32 Add(String input)
        {
            if (String.IsNullOrEmpty(input))
                return 0;

            if (input.Length == 1)
                return Int32.Parse(input);

            return CalculateForMultipleNumbers(input);
        }

        private int CalculateForMultipleNumbers(String input)
        {
            var inputDefinedDelimiter = input.StartsWith("//");

            if (inputDefinedDelimiter)
                delimiter = input.Substring(2, 1).ToCharArray()[0];
            else
                delimiter = ',';

            var numbers = ExtractNumbersFromRawString(inputDefinedDelimiter ? input.Substring(3) : input);
            return CalculateResults(numbers);
        }

        private List<Int32> ExtractNumbersFromRawString(String input)
        {
            var delimitedNumbers = input.Replace('\n', delimiter);
            var rawNumbers = delimitedNumbers.Split(delimiter);

            return ConvertRawNumbersToList(rawNumbers);
        }

        private List<int> ConvertRawNumbersToList(String[] rawNumbers)
        {
            List<Int32> numbers = new List<Int32>();
            foreach(var number in rawNumbers)
            {
                if (!String.IsNullOrEmpty(number))
                    numbers.Add(Int32.Parse(number));
            }

            return numbers;
        }

        private int CalculateResults(List<Int32> numbers)
        {
            Int32 total = 0;
            
            foreach(var number in numbers)
            {
                total += number;
            }

            return total;
        }
    }
}
