using DATABASE.Enums;
using TeaShopBLL.DTO;

namespace TeaShopBotAPI.Models
{
    public class TeaViewModel
    {
        public TeaDTO Tea { get; set; }
    }

    public class TeaEditModel : ProductEditModel
    {
        public TeaWeight TeaWeight { get; set; }
        public TeaForms TeaForm { get; set; }
        public TeaTypes TeaType { get; set; }
    }
}
