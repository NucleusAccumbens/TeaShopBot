using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopDAL.Enums;

namespace DATABASE.Entityes
{
    public class Order : BaseModel
    {
        public long OrderId { get; set; }
        public long UserChatId { get; set; }
        public bool OrderStatus { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public ReceiptMethods ReceiptMethod { get; set; }

        public List<Product> Products { get; set; }
    }
}
