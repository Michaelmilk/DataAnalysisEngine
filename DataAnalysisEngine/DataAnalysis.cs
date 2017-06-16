using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAnalysisEngine
{
    class DataAnalysis
    {
        static public void RangeAnalysis()
        {

        }

        static public void RegexAnalysis(string input, string pattern)
        {

        }

        static public void ValueAnalysis()
        {

        }

        private bool IsInteger(string input)
        {
            int output = -1;
            return int.TryParse(input, out output);
        }

        private bool IsDouble(string input)
        {
            Double output = 0;
            return double.TryParse(input, out output);
        }

        private bool ContainNumber(string input)
        {
            //return input.Any(t => char.IsDigit(t));
            return input.All(char.IsDigit);
        }

        private bool ContainSpecialLetters(string input, char[] specialLetters)
        {
            Regex pattern = new Regex(@"^[a-zA-Z0-9_@.-]*$");
            return pattern.IsMatch(input);

            //int indexOf = input.IndexOfAny(specialLetters);
            //return indexOf == -1;
        }

        private bool ContainWords(string input, string word)
        {
            return input.Contains(word);
        }
    }
}
