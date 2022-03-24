using DATABASE.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShopBLL.BisnessModels;
using TeaShopDAL.Enums;

namespace TeaShopBLL.DTO
{
    // Data Transfer Object (DTO) - специальная модель для передачи данных.
    public class OrderDTO
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long OrderId { get; set; }
        public int OrderNumber { get; set; }
        public bool OrderStatus { get; set; }
        public string? Comment { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public ReceiptMethods ReceiptMethod { get; set; }

        public Discount Discount { get; set; }
        
        private decimal _totalProductPrice;
        public decimal TotalProductPrice
        {
            get
            {
                foreach (var product in Products)
                {
                    _totalProductPrice += product.ProductPrice;
                }
                return _totalProductPrice - Discount.GetDiscount();
            }
        }

        public long UserChatId { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
