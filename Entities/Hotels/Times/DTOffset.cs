using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Hotels.Times
{
    [Table("BookedDays")]
    public class DTOffset
    {
        public int Id { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
