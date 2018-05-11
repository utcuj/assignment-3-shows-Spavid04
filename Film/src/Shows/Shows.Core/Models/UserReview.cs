using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shows.Core.Models
{
    public class UserReview
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }

        public string Review { get; set; }
        public int Rating { get; set; }

        public virtual User User { get; set; }
        public virtual Show Show { get; set; }

        public override string ToString()
        {
            return $"{User.Username} - {Rating}";
        }
    }
}
