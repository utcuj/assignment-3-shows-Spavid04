using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Server.Database;

namespace Shows.Server.Controllers
{
    public class NotificationController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public void Delete(int notificationId)
        {
            var notification = dbContext.Notifications.FirstOrDefault(x => x.Id == notificationId);

            if (notification == null)
                return;

            dbContext.Notifications.Remove(notification);
            dbContext.SaveChanges();
        }
    }
}