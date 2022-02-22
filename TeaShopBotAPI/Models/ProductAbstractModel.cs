using System.ComponentModel.DataAnnotations;
using TeaShopBLL.DTO;

namespace TeaShopBotAPI.Models
{
    public abstract class ProductEditModel
    {
        [Required]
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public bool InStock { get; set; }
    }
}
