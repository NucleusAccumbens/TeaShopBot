using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaShopBLL.DTO
{
    // Data Transfer Object (DTO) - специальная модель для передачи данных.
    public abstract class ProductDTO
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string? ProductPathToImage { get; set; }
        public bool InStock
        {
            get
            {
                if(ProductCount > 0) return true;
                else return false;
            }
        }
        public List<OrderDTO>? Orders { get; set; }
    }
}
