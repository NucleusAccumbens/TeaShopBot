using Domain.Enums;

namespace TeaShopTelegramBot.Models;
public class HerbDto : ProductDto
{   
    public HerbRegion? Region { get; set; }
    public HerbWeight? Weight { get; set; }
}
