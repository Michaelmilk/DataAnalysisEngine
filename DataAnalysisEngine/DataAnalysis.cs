using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private bool ContainNumber()
        {
            
        }

        private bool ContainSpecialLetter()
        {
            
        }
    }
}
