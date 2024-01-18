using Domain.Enums;

namespace TeaShopTelegramBot.Models;
public class TeaDto : ProductDto
{
    public TeaWeight? TeaWeight { get; set; }
    public TeaForm? TeaForm { get; set; }
    public TeaType? TeaType { get; set; }
}
