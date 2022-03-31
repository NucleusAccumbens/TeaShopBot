using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Enums
{
    public enum HoneyWeight
    {
        ThreeHundredFifty = 350,
        NineHundredFifty = 950
    }

    public class HoneyEnumParser
    {
        public static string HoneyWeightToString(HoneyWeight honeyWeight)
        {
            if (honeyWeight == HoneyWeight.ThreeHundredFifty) return "350";
            else return "950";
        }
    }
}
