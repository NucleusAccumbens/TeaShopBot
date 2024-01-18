using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Teas")]
public class Tea : Product
{
    public TeaWeight? TeaWeight { get; set; }
    public TeaForm? TeaForm { get; set; }
    public TeaType? TeaType { get; set; }
}

