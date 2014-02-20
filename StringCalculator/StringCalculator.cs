using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        private const Int32 maxSize = 1000;

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
            var delimiters = ExtractDelimiters(input);
            var formattedInput = FormatData(input, delimiters);
            var numbers = ExtractNumbersFromRawString(formattedInput, delimiters);
            
            return CalculateResults(numbers);
        }

        private IEnumerable<String> ExtractDelimiters(String input)
        {
            if (ComplexDefinedDelimiters(input))
                return GetComplexDefinedDelimiters(input);
            else if (SingleDefinedDelimiter(input))
                return new [] { input.Substring(2, 1) };
            else
                return new [] { "," };
        }

        private IEnumerable<String> GetComplexDefinedDelimiters(String input)
        {
            // http://stackoverflow.com/questions/1811183/how-to-extract-the-contents-of-square-brackets-in-a-string-of-text-in-c-sharp-us
            var delimiters = Regex.Matches(input, @"\[([^]]*)\]").Cast<Match>().Select(x => x.Groups[1].Value).ToList();

            return delimiters;
        }

        private Boolean ComplexDefinedDelimiters(String input)
        {
            return input.StartsWith("//[");
        }

        private Boolean SingleDefinedDelimiter(String input)
        {
            return input.StartsWith("//");
        }

        private String FormatData(String input, IEnumerable<String> delimiters)
        {
            input = input.Replace("\n", delimiters.ToArray()[0]);

            if (ComplexDefinedDelimiters(input))
                return input.Substring(input.LastIndexOf(']') + 1);
            else if (SingleDefinedDelimiter(input))
                return input.Substring(3);
            else return input;
        }

        private IEnumerable<Int32> ExtractNumbersFromRawString(String input, IEnumerable<String> delimiters)
        {
            var rawNumbers = input.Split(delimiters.ToArray(), StringSplitOptions.None);
            var extractedNumbers = ConvertRawNumbersToList(rawNumbers);
            var numbersLessThanMax = RemoveNumbersGreaterThanMaxNumber(extractedNumbers);
            ThrowExceptionForNegativeNumbers(numbersLessThanMax);
            
            return numbersLessThanMax;
        }

        private IEnumerable<Int32> ConvertRawNumbersToList(String[] rawNumbers)
        {
            var numbers = new List<Int32>();

            foreach (var number in rawNumbers)
            {
                if (!String.IsNullOrEmpty(number))
                    numbers.Add(Int32.Parse(number));
            }

            return numbers;
        }

        private IEnumerable<Int32> RemoveNumbersGreaterThanMaxNumber(IEnumerable<Int32> numbers)
        {
            return numbers.ToList().Except(numbers.Where(n => n > maxSize).Select(n => n));
        }

        private void ThrowExceptionForNegativeNumbers(IEnumerable<Int32> numbers)
        {
            if (numbers.Any(n => n < 0))
            {
                var errorMessage = CreateNegativeNumberErrorMessage(numbers);
                throw new Exception(errorMessage);
            }
        }

        private String CreateNegativeNumberErrorMessage(IEnumerable<Int32> rawNumbers)
        {
            return String.Format("negatives not allowed: {0}", 
                String.Join(",", rawNumbers.Where(number => number < 0))); 
        }


        private Int32 CalculateResults(IEnumerable<Int32> numbers)
        {
            return numbers.Sum();
        }
    }
}
