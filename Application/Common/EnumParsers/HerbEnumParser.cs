using Domain.Enums;

namespace Application.Common.EnumParsers;

public class HerbEnumParser
{
    public static string GetHerbWeightStringValue(HerbWeight? herbsWeight)
    {
        if (herbsWeight == HerbWeight.Fifty) return "50";
        if (herbsWeight == HerbWeight.OneHundred) return "100";
        if (herbsWeight == HerbWeight.OneHundredFifty) return "150";
        if (herbsWeight == HerbWeight.TwoHundred) return "200";
        else return "250";
    }

    public static string GetHerbRegionStringValueInRussian(HerbRegion? herbsRegion)
    {
        if (herbsRegion == HerbRegion.Karelia) return "Карелия";
        if (herbsRegion == HerbRegion.Altai) return "Алтай";
        if (herbsRegion == HerbRegion.Caucasus) return "Кавказ";
        else return "Сибирь";
    }

    public static string GetHerbRegionStringValueInEnglish(HerbRegion? herbsRegion)
    {
        if (herbsRegion == HerbRegion.Karelia) return "Karelia";
        if (herbsRegion == HerbRegion.Altai) return "Altai";
        if (herbsRegion == HerbRegion.Caucasus) return "Caucasus";
        else return "Siberia";
    }

    public static string GetHerbRegionStringValueInHebrew(HerbRegion? herbsRegion)
    {
        if (herbsRegion == HerbRegion.Karelia) return "Karelia";
        if (herbsRegion == HerbRegion.Altai) return "Altai";
        if (herbsRegion == HerbRegion.Caucasus) return "Caucasus";
        else return "Siberia";
    }
}
