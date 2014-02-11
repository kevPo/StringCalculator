using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            var delimiters = ExtractDelimiters(input);
            var formattedInput = FormatData(input, delimiters);
            var numbers = ExtractNumbersFromRawString(formattedInput, delimiters);
            
            return CalculateResults(numbers);
        }

        private List<String> ExtractDelimiters(String input)
        {
            if (ComplexDefinedDelimiters(input))
                return GetComplexDefinedDelimiters(input);
            else if (SingleDefinedDelimiter(input))
                return new List<String> { input.Substring(2, 1) };
            else
                return new List<String> { "," };
        }

        private List<String> GetComplexDefinedDelimiters(String input)
        {
            //http://stackoverflow.com/questions/1811183/how-to-extract-the-contents-of-square-brackets-in-a-string-of-text-in-c-sharp-us
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

        private String FormatData(String input, List<String> delimiters)
        {
            input = input.Replace("\n", delimiters[0]);

            if (ComplexDefinedDelimiters(input))
                return input.Substring(input.LastIndexOf(']') + 1);
            else if (SingleDefinedDelimiter(input))
                return input.Substring(3);
            else return input;
        }

        private List<Int32> ExtractNumbersFromRawString(String input, List<String> delimiters)
        {
            var rawNumbers = input.Split(delimiters.ToArray(), StringSplitOptions.None);

            return ConvertRawNumbersToList(rawNumbers);
        }

        private List<Int32> ConvertRawNumbersToList(String[] rawNumbers)
        {
            var numbers = new List<Int32>();

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
                    
                    if (numberToAdd <= 1000)
                        numbers.Add(numberToAdd);
                }
            }

            return numbers;
        }

        private String CreateNegativeNumberErrorMessage(String[] rawNumbers)
        {
            var errorMessage = "negatives not allowed: ";
            var listedNegativeNumbers = String.Join(",", rawNumbers.Where(number => number.Contains("-")));
            
            return errorMessage += listedNegativeNumbers;
        }

        private Int32 CalculateResults(List<Int32> numbers)
        {
            var total = 0;
            
            foreach(var number in numbers)
                total += number;

            return total;
        }
    }
}
