using System;

namespace StringCalculator
{
    public class StringCalculator
    {
        public Int32 Add(String input)
        {
            if (String.IsNullOrEmpty(input))
                return 0;

            return Int32.Parse(input);
        }
    }
}
