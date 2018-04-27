using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shows.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual User User { get; set; }
        public virtual ShowEvent Event { get; set; }
    }
}
