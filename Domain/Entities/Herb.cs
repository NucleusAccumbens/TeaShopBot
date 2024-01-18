using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    [Table("Herbs")]
    public class Herb : Product
    {
        public HerbRegion Region { get; set; }
        public HerbWeight Weight { get; set; }
    }
}
