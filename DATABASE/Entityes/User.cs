using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE.Entityes
{
    public class User : BaseModel
    {
        public long UserId  { get; set; }
        
        [Key]
        public long ChatId { get; set; }
        public string? Name { get; set; }
        public bool IsAdmin { get; set; }

        public virtual List<Order> Orders { get; set; } // модификатор virtual нужен для lazy loading
                                                        // - при обращении к сущности из бд не будут тянуться
                                                        // связанные с ней данные 
    }
}
