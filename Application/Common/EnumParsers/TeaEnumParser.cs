using Domain.Enums;

namespace Application.Common.EnumParsers;

public class TeaEnumParser
{    
    public static string GetTeaTypeStringValueInRussian(TeaType? teaType)
    {
        if (teaType == null) return "значение не указано";
        if (teaType == TeaType.Red) return "Красный";
        if (teaType == TeaType.Green) return "Зелёный";
        if (teaType == TeaType.White) return "Белый";
        if (teaType == TeaType.Oolong) return "Улун";
        if (teaType == TeaType.ShuPuer) return "Шу Пуэр";
        if (teaType == TeaType.ShenPuer) return "Шен Пуэр";
        else return "Авторский чай";
    }

    public static string GetTeaFormStringValueInRussian(TeaForm? teaForm)
    {
        if (teaForm == null) return "значение не указано";
        if (teaForm == TeaForm.Pressed) return "Пресованный";
        else return "Рассыпной";
    }

    public static string GetTeaWeightStringValue(TeaWeight? teaWeight)
    {
        if (teaWeight == TeaWeight.Fifty) return "50";
        if (teaWeight == TeaWeight.OneHundred) return "100";
        if (teaWeight == TeaWeight.OneHundredFifty) return "150";
        if (teaWeight == TeaWeight.TwoHundred) return "200";
        if (teaWeight == TeaWeight.TwoHundredFifty) return "250";
        else return "357";
    }

    public static string GetTeaTypeStringValueInEnglish(TeaType? teaType)
    {
        if (teaType == null) return "no value specified";
        if (teaType == TeaType.Red) return "Red";
        if (teaType == TeaType.Green) return "Green";
        if (teaType == TeaType.White) return "White";
        if (teaType == TeaType.Oolong) return "Oolong";
        if (teaType == TeaType.ShuPuer) return "Shu puer";
        if (teaType == TeaType.ShenPuer) return "Shen puer";
        else return "Craft";
    }

    public static string GetTeaFormStringValueInEnglish(TeaForm? teaForm)
    {
        if (teaForm == null) return "no value specified";
        if (teaForm == TeaForm.Pressed) return "Pressed";
        else return "Loose";
    }

    public static string GetTeaWeightStringValueInEnglish(TeaWeight? teaWeight)
    {
        if (teaWeight == null) return "no value specified";
        if (teaWeight == TeaWeight.Fifty) return "50";
        if (teaWeight == TeaWeight.OneHundred) return "100";
        if (teaWeight == TeaWeight.OneHundredFifty) return "150";
        if (teaWeight == TeaWeight.TwoHundred) return "200";
        if (teaWeight == TeaWeight.TwoHundredFifty) return "250";
        else return "357";
    }

    public static string GetTeaTypeStringValueInHebrew(TeaType? teaType)
    {
        if (teaType == null) return "no value specified";
        if (teaType == TeaType.Red) return "Red";
        if (teaType == TeaType.Green) return "Green";
        if (teaType == TeaType.White) return "White";
        if (teaType == TeaType.Oolong) return "Oolong";
        if (teaType == TeaType.ShuPuer) return "Shu puer";
        if (teaType == TeaType.ShenPuer) return "Shen puer";
        else return "Craft";
    }

    public static string GetTeaFormStringValueInHebrew(TeaForm? teaForm)
    {
        if (teaForm == null) return "no value specified";
        if (teaForm == TeaForm.Pressed) return "Pressed";
        else return "Loose";
    }

    public static string GetTeaWeightStringValueInHebrew(TeaWeight? teaWeight)
    {
        if (teaWeight == null) return "no value specified";
        if (teaWeight == TeaWeight.Fifty) return "50";
        if (teaWeight == TeaWeight.OneHundred) return "100";
        if (teaWeight == TeaWeight.OneHundredFifty) return "150";
        if (teaWeight == TeaWeight.TwoHundred) return "200";
        if (teaWeight == TeaWeight.TwoHundredFifty) return "250";
        else return "357";
    }
}
