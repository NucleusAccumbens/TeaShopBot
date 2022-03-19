using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Enums
{
    public enum TeaForms
    {
        Loose,
        Pressed
    }
    public enum TeaTypes
    {
        ShenPuer,
        ShuPuer,
        Oolong,
        Red,
        Green,
        White
    }
    public enum TeaWeight
    {
        Fifty = 50,
        OneHundred = 100,
        OneHundredFifty = 150,
        TwoHundred = 200,
        TwoHundredFifty = 250,
        ThreeHundredSeventyFive = 375
    }

    public class TeaEnumParser
    {
        public static string TeaTypeToString(TeaTypes teaType)
        {
            if (teaType == TeaTypes.Red) return "Красный";
            if (teaType == TeaTypes.Green) return "Зелёный";
            if (teaType == TeaTypes.White) return "Белый";
            if (teaType == TeaTypes.Oolong) return "Улун";
            if (teaType == TeaTypes.ShuPuer) return "Шу Пуэр";
            else return "Шен Пуэр";
        }

        public static string TeaFormToString(TeaForms teaForm)
        {
            if (teaForm == TeaForms.Pressed) return "Пресованный";
            else return "Рассыпной";
        }
        public static string TeaWeightToString(TeaWeight teaWeight)
        {
            if (teaWeight == TeaWeight.Fifty) return "50";
            if (teaWeight == TeaWeight.OneHundred) return "100";
            if (teaWeight == TeaWeight.OneHundredFifty) return "150";
            if (teaWeight == TeaWeight.TwoHundred) return "200";
            if (teaWeight == TeaWeight.TwoHundredFifty) return "250";
            else return "375";
        }
    }
}
