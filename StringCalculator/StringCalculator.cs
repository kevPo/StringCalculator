using System;
using System.Collections.Generic;
using System.Linq;

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

            return CalculateForMultipleNumbers(input);
        }

        private Int32 CalculateForMultipleNumbers(String input)
        {
            var delimiter = ExtractDelimiter(input);
            var formattedInput = FormatData(input, delimiter);
            var numbers = ExtractNumbersFromRawString(formattedInput, delimiter);
            ValidateNoNegatives(numbers);
            
            return CalculateResults(numbers);
        }

        private String ExtractDelimiter(String input)
        {
            if (MultipleDefinedDelimiters(input))
                return input.Substring(3, input.IndexOf(']') - 3);
            else if (SingleDefinedDelimiter(input))
                return input.Substring(2, 1);
            else
                return ",";
        }

        private Boolean SingleDefinedDelimiter(String input)
        {
            return input.StartsWith("//");
        }

        private Boolean MultipleDefinedDelimiters(String input)
        {
            return input.StartsWith("//[");
        }

        private String FormatData(String input, String delimiter)
        {
            input = input.Replace("\n", delimiter);

            if (MultipleDefinedDelimiters(input))
                return input.Substring(input.IndexOf(']') + 1);
            else if (SingleDefinedDelimiter(input))
                return input.Substring(3);
            else return input;
        }

        private List<Int32> ExtractNumbersFromRawString(String input, String delimiter)
        {
            var rawNumbers = input.Split(new[] { delimiter }, StringSplitOptions.None);

            return ConvertRawNumbersToList(rawNumbers);
        }

        private List<Int32> ConvertRawNumbersToList(String[] rawNumbers)
        {
            List<Int32> numbers = new List<Int32>();

            foreach (var number in rawNumbers)
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
            foreach (var number in rawNumbers)
            {
                if (number.Contains("-"))
                    errorMessage += number + " ";
            }

            return errorMessage.Trim();
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

        private Int32 CalculateResults(List<Int32> numbers)
        {
            Int32 total = 0;
            
            foreach(var number in numbers)
                total += number;

            return total;
        }
    }
}
