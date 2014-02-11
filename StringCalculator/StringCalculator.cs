using System;

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

            var rawNumbers = input.Split(',');
            return CalculateResults(rawNumbers);
        }

        private int CalculateResults(string[] rawNumbers)
        {
            Int32 total = 0;
            
            foreach(var number in rawNumbers)
            {
                total += Int32.Parse(number);
            }

            return total;
        }
    }
}
