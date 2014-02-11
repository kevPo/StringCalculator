using System;
using System.Collections.Generic;
using System.Linq;

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

        private Int32 CalculateForMultipleNumbers(String input)
        {
            var inputDefinedDelimiter = input.StartsWith("//");

            if (inputDefinedDelimiter)
                delimiter = input.Substring(2, 1).ToCharArray()[0];
            else
                delimiter = ',';

            var numbers = ExtractNumbersFromRawString(inputDefinedDelimiter ? input.Substring(3) : input);
            ValidateNoNegatives(numbers);
            return CalculateResults(numbers);
        }

        private void ValidateNoNegatives(List<Int32> numbers)
        {
            List<Int32> negativeNumbers = new List<Int32>();
            foreach(var number in numbers)
            {
                if (number < 0)
                    negativeNumbers.Add(number);
            }

            if (negativeNumbers.Any())
            {
                var errorMessage = "negatives not allowed: ";
                foreach (var negativeNumber in negativeNumbers)
                    errorMessage += negativeNumber + " ";
                
                throw new Exception(errorMessage);
            }
        }

        private List<Int32> ExtractNumbersFromRawString(String input)
        {
            var delimitedNumbers = input.Replace('\n', delimiter);
            var rawNumbers = delimitedNumbers.Split(delimiter);
            
            return ConvertRawNumbersToList(rawNumbers);
        }

        private List<Int32> ConvertRawNumbersToList(String[] rawNumbers)
        {
            List<Int32> numbers = new List<Int32>();

            foreach(var number in rawNumbers)
            {
                if (!String.IsNullOrEmpty(number))
                {
                    var numberToAdd = Int32.Parse(number);
                    
                    if (numberToAdd < 0)
                    {
                        var errorMessage = CreateNegativeNumberErrorMessage(rawNumbers);
                        throw new Exception(errorMessage);
                    }
                    else if (numberToAdd <= 1000)
                        numbers.Add(numberToAdd);
                }
            }

            return numbers;
        }

        private String CreateNegativeNumberErrorMessage(String[] rawNumbers)
        {
            var errorMessage = "negatives not allowed: ";
            foreach(var number in rawNumbers)
            {
                if (number.Contains("-"))
                    errorMessage += number + " ";
            }

            return errorMessage.Trim();
        }

        private Int32 CalculateResults(List<Int32> numbers)
        {
            Int32 total = 0;
            
            foreach(var number in numbers)
                total += number;

            return total;
        }
    }
}
