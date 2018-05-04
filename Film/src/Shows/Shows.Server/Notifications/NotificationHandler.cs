using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Shows.Server.Notifications
{
    public class NotificationHandler : Hub
    {
        private readonly Broadcaster _broadcaster = Broadcaster.Instance;
    }
}