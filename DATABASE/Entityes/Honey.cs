using DATABASE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Entityes
{
    [Table("Honey")]
    public class Honey : Product
    {
        public HoneyWeight HoneyWeight { get; set; }
    }
}
