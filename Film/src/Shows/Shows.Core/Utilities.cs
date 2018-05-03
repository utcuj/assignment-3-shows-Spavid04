using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shows.Core
{
    public static class Utilities
    {
        public static string FlattenString(this string input)
        {
            return input.Replace(" ", "").ToUpper();
        }
    }
}