using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Entityes
{
    public abstract class Product : BaseModel
    {
        public long ProductId { get; set; }

        [MaxLength(100)]
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public byte[]? ProductImage { get; set; }
        public bool InStock { get; set; }

        public virtual List<Order>? Orders { get; set; }
    }
}
