using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Enums
{

    public enum HerbsWeight
    {
        Fifty = 50,
        OneHundred = 100,
        OneHundredFifty = 150,
        TwoHundred = 200,
        TwoHundredFifty = 250
    }

    public enum HerbsRegion
    {
        Caucasus,
        Altai,
        Karelia,
        Siberia
    }

    public class HerbsEnumParser
    {
        public static string HerbsWeightToString(HerbsWeight herbsWeight)
        {
            if (herbsWeight == HerbsWeight.Fifty) return "50";
            if (herbsWeight == HerbsWeight.OneHundred) return "100";
            if (herbsWeight == HerbsWeight.OneHundredFifty) return "150";
            if (herbsWeight == HerbsWeight.TwoHundred) return "200";
            else return "250";
        }

        public static string HerbsRegionToString(HerbsRegion herbsRegion)
        {
            if (herbsRegion == HerbsRegion.Karelia) return "Карелия";
            if (herbsRegion == HerbsRegion.Altai) return "Алтай";
            if (herbsRegion == HerbsRegion.Caucasus) return "Кавказ";
            else return "Сибирь";
        }
    }
}
