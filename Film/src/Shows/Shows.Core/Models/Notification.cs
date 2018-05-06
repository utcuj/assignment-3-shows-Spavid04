using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shows.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public virtual User ForUser { get; set; }
        public virtual Show ForShow { get; set; }
        
        public string Details { get; set; }
        public NotificationType Type { get; set; }

        public override string ToString()
        {
            string[] lines =
            {
                $"New notification about {ForShow.Title}:",
                Details
            };
            return String.Join(Environment.NewLine, lines);
        }
    }

    public enum NotificationType
    {
        ShowAvailability = 0,
        UserRecommendation = 1
    }
}
