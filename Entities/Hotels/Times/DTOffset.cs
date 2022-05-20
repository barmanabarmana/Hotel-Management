using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Hotels.Times
{
    [Table ("BookedDays")]
    public class DTOffset
    {
        public int Id { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
