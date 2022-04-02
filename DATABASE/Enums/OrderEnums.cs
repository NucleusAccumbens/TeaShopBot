using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaShopDAL.Enums
{
    public enum PaymentMethods
    {
        Cash,
        Remittance,
        Link
    }

    public enum ReceiptMethods
    {
        Pickup,
        CDEK
    }

    public class OrderEnumParser
    {
        public static string PaymentMethodToString(PaymentMethods method)
        {
            if (method == PaymentMethods.Cash) return "Наличными";
            if (method == PaymentMethods.Remittance) return "Переводом";
            else return "По ссылке";
        }

        public static string ReceiptMethodToString(ReceiptMethods method)
        {
            if (method == ReceiptMethods.Pickup) return "Самовывоз";
            else return "СДЭК";
        }
    }
}
