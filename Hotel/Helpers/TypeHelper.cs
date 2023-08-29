using Hotel.Models;
using System;

namespace Hotel.Helpers
{
    public class TypeHelper
    {
        public static bool InfoTypeCheck(int o)
        {
            var a = Enum.GetValues(typeof(InfoType)).Length-1;
            return a >= o;
        }
    }
}
