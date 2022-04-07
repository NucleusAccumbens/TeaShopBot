using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Entityes
{
    public abstract class BaseModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
