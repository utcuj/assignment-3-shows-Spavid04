using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Shows.Core.Models;
using Shows.Server.Database;

namespace Shows.Server.Notifications
{
    [HubName("NotificationHub")]
    public class NotificationHandler : Hub
    {
        private readonly Broadcaster _broadcaster = Broadcaster.Instance;

        private static Mutex mutex = new Mutex();
        public static void NotificationCheckEvent(object state)
        {
            //resends all unsent notifications
            bool free = mutex.WaitOne(10);
            if (!free) return;

            ShowsDbContext dbContext = new ShowsDbContext();
            foreach (var notification in dbContext.Notifications.ToList())
            {
                SendNotification(dbContext, notification);
            }

            mutex.ReleaseMutex();
        }

        public static void AddNewNotification(ShowsDbContext dbContext, Notification notification)
        {
            dbContext.Notifications.Add(notification);
            dbContext.SaveChanges();
        }

        public static void SendNotification(ShowsDbContext dbContext, Notification n)
        {
            n = dbContext.Notifications.First(x => x.Id == n.Id);
            Broadcaster.Instance.Broadcast(n);
        }
    }
}