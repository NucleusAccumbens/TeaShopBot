using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaShopBLL
{
    // Data Transfer Object (DTO) - специальная модель для передачи данных.
    public class UserDTO
    {
        public long UserId { get; set; }
        public long ChatId { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
