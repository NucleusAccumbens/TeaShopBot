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
        public long ChatId { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public bool IsAdmin { get; set; }
    }
}
