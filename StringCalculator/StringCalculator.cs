using System;
using System.Collections.Generic;

namespace StringCalculator
{
    public class StringCalculator
    {
        
        public Int32 Add(String input)
        {
            if (String.IsNullOrEmpty(input))
                return 0;

            if (input.Length == 1)
                return Int32.Parse(input);

            var numbers = ExtractNumbers(input);
            return CalculateResults(numbers);
        }

        private List<Int32> ExtractNumbers(String input)
        {
            var extractedNumbers = new List<Int32>();
            var delimitedNumbers = input.Replace('\n', ',');
            var rawNumbers = delimitedNumbers.Split(',');

            return ConvertRawNumbersToList(rawNumbers);
        }

        private List<int> ConvertRawNumbersToList(string[] rawNumbers)
        {
            List<Int32> numbers = new List<Int32>();
            foreach(var number in rawNumbers)
            {
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
