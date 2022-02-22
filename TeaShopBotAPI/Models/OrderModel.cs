using DATABASE.Entityes;
using System.Collections.Generic;
using TeaShopBLL.BisnessModels;
using TeaShopBLL.DTO;
using TeaShopDAL.Enums;

namespace TeaShopBotAPI.Models
{
    public class OrderViewModel
    {
        public OrderDTO Order { get; set; }
    }

    public class OrderEditModel
    {
        public long OrderId { get; set; }
        public int OrderNumber { get; set; }
        public bool OrderStatus { get; set; }
        public string? Comment { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public ReceiptMethods ReceiptMethod { get; set; }
        public Discount Discount { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
