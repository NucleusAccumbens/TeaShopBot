using DATABASE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Entityes
{

    [Table("Herbs")]
    public class Herb : Product
    {
        public HerbsRegion Region { get; set; }
        public HerbsWeight Weight { get; set; }
    }
}
