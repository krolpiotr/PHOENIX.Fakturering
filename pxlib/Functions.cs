using System;
using System.Text.RegularExpressions;

namespace pxlib
{
    public class Functions
    {
        public static int CountWords(string s)
        {
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }
    }
}
