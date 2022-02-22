using DATABASE.Enums;
using TeaShopBLL.DTO;

namespace TeaShopBotAPI.Models
{
    public class HoneyViewModel
    {
        public HoneyDTO Honey { get; set; }
    }

    public class HoneyEditModel : ProductEditModel
    {
        public HoneyWeight HoneyWeight { get; set; }
    }
}
