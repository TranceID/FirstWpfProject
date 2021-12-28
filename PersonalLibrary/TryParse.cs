using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary
{
        // TODO -  Write a whole personal library. It'll save time during capstone to be able to preload the library.
    public static class TryParse
    {

        // Contains Try Parses for basic data types
        #region Primitive Parsing
        public static double ParseDouble(string input)
        {
            if(double.TryParse(input, out double d))
            {
                return d;
            }
            else
            {
                return -1;
            }
        }

        public static int ParseInteger(string input)
        {
            if(int.TryParse(input, out int i))
            {
                return i;
            }
            else
            {
                return -1;
            }
        }

        public static decimal ParseDecimal(string input)
        {
            if(decimal.TryParse(input, out decimal d))
            {
                return d;
            }
            else
            {
                return -1;
            }
        }
        #endregion
    }
}
