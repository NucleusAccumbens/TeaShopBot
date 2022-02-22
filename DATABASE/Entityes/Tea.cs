using DATABASE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Entityes
{
    [Table("Teas")]
    public class Tea : Product
    {
        public TeaWeight TeaWeight { get; set; }
        public TeaForms TeaForm { get; set; }
        public TeaTypes TeaType { get; set; }
    }
}
