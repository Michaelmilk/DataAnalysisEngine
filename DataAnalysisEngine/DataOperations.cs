using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAnalysisEngine
{
    public static class DataOperations
    {
        public static bool IsDecimal(string input)
        {
            decimal output;
            return !string.IsNullOrEmpty(input) && decimal.TryParse(input, out output);
        }

        public static bool IsInteger(string input)
        {
            int output = -1;
            return !string.IsNullOrEmpty(input) && int.TryParse(input, out output);
        }

        public static bool IsDouble(string input)
        {
            Double output = 0;
            return !string.IsNullOrEmpty(input) && double.TryParse(input, out output);
        }

        public static bool IsCapitalization(string input)
        {
            return !string.IsNullOrEmpty(input) && char.IsUpper(input[0]);
        }

        public static bool IsUpper(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsUpper);
        }

        public static bool IsLower(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsLower);
        }

        public static bool ContainNumber(string input)
        {
            //return input.Any(t => char.IsDigit(t));
            return !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
        }

        public static bool ContainSpecialLetters(string input, char[] specialLetters)
        {
            Regex pattern = new Regex(@"^[a-zA-Z0-9_@.-]*$");
            return !string.IsNullOrEmpty(input) && pattern.IsMatch(input);

            //int indexOf = input.IndexOfAny(specialLetters);
            //return indexOf == -1;
        }

        public static bool ContainWords(string input, string word)
        {
            return !string.IsNullOrEmpty(input) && input.Contains(word);
        }
    }
}
