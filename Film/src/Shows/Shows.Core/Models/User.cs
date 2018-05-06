using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shows.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }
        public UserLevel UserLevel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore] public virtual List<UserShowHistory> UserShowHistory { get; set; }
        [JsonIgnore] public virtual List<UserGroup> Groups { get; set; }
        [JsonIgnore] public virtual List<UserReview> Reviews { get; set; }
        [JsonIgnore] public virtual List<UserInterest> Interests { get; set; }

        public override string ToString()
        {
            return $"{Username} - {UserLevel.ToString()}";
        }
    }

    public enum UserLevel
    {
        Regular = 0,
        Premium = 1,
        Administrator = 2
    }
}
