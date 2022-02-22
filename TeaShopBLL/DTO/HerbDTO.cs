using DATABASE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaShopBLL.DTO
{
    // Data Transfer Object (DTO) - специальная модель для передачи данных.
    public class HerbDTO : ProductDTO
    {
        public HerbsRegion Region { get; set; }
        public HerbsWeight Weight { get; set; }
    }
}
