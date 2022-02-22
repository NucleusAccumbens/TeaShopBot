using DATABASE.Enums;
using TeaShopBLL.DTO;

namespace TeaShopBotAPI.Models
{
    public class HerbViewModel
    {
        public HerbDTO Herb { get; set; }
    }

    public class HerbEditModel : ProductEditModel
    {
        public HerbsRegion HerbRegion { get; set; }
        public HerbsWeight HerbWeight { get; set; }
    }
}
