using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shows.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        public Guid PublicId { get; set; }
        public UserLevel UserLevel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual List<UserGroup> Groups { get; set; }
        public virtual List<UserReview> Reviews { get; set; }
        public virtual List<UserInterest> Interests { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
        public virtual List<Friendship> Friendships { get; set; }
    }

    public enum UserLevel
    {
        Regular = 0,
        Premium = 1,
        Administrator = 2
    }
}
