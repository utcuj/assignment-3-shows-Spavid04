using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;
using Shows.Server.Notifications;

namespace Shows.Server.Controllers
{
    public class RecommendationController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public void Post(int userId, int showId, bool toGroup, [FromBody] string friendNameOrGroupId)
        {
            if (!toGroup)
            {
                //recommended by friend

                var sender = dbContext.Users.First(x => x.Id == userId);
                var show = dbContext.Shows.First(x => x.Id == showId);
                var friend = dbContext.Users.FirstOrDefault(x => x.Username == friendNameOrGroupId);

                if (friend == null)
                    return;

                var notification = new Notification()
                {
                    ForUser = friend,
                    ForShow = show,
                    Details = $"{sender.Username} has recommended you this show!",
                    Type = NotificationType.UserRecommendation
                };
                NotificationHandler.Subscribe(dbContext, notification);
            }
            else
            {
                //recommended by group

                var sender = dbContext.Users.First(x => x.Id == userId);
                var show = dbContext.Shows.First(x => x.Id == showId);
                int groupId = Convert.ToInt32(friendNameOrGroupId);
                var group = dbContext.UserGroups.First(x => x.Id == groupId);

                var targetUsers = group.Users.Where(x => x.Id != userId);

                foreach (var user in targetUsers)
                {
                    var notification = new Notification()
                    {
                        ForUser = user,
                        ForShow = show,
                        Details =
                            $"{sender.Username} has recommended this show to one of your groups! ({group.Description})",
                        Type = NotificationType.UserRecommendation
                    };
                    NotificationHandler.Subscribe(dbContext, notification);
                }
            }
        }
    }
}