using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shows.Core.Models
{
    public class UserGroup
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }
        public string Description { get; set; }

        public virtual List<User> Users { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
