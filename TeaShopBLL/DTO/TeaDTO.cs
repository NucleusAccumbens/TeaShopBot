using DATABASE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaShopBLL.DTO
{
    // Data Transfer Object (DTO) - специальная модель для передачи данных.
    public class TeaDTO : ProductDTO
    {
        public TeaWeight TeaWeight { get; set; }
        public TeaForms TeaForm { get; set; }
        public TeaTypes TeaType { get; set; }
    }
}
