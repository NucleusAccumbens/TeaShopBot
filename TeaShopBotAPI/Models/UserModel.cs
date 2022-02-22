using System.ComponentModel.DataAnnotations;
using TeaShopBLL;

namespace TeaShopBotAPI.Models
{
    public class AppUserViewModel
    {
        public UserDTO User { get; set; }
    }

    public class AppUserEditModel
    {
        [Required]
        public long UserId { get; set; }
        public long ChatId { get; set; }
        public string? UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
