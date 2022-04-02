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
            if (method == PaymentMethods.Cash) return "Оплата наличными";
            if (method == PaymentMethods.Remittance) return "Оплата денежным переводом";
            else return "Оплата по ссылке";
        }

        public static string ReceiptMethodToString(ReceiptMethods method)
        {
            if (method == ReceiptMethods.Pickup) return "Самовывоз";
            else return "СДЭК";
        }
    }
}
