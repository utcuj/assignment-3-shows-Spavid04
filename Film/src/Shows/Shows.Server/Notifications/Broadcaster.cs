using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Shows.Core.Models;

namespace Shows.Server.Notifications
{
    public class Broadcaster
    {
        private static TimeSpan interval = TimeSpan.FromSeconds(10);
        private static Timer timer = null;

        private static readonly Lazy<Broadcaster> _instance =
            new Lazy<Broadcaster>(() => new Broadcaster(GlobalHost.ConnectionManager.GetHubContext<NotificationHandler>().Clients));
        public static Broadcaster Instance
        {
            get { return _instance.Value; }
        }

        public IHubConnectionContext<dynamic> Clients { get; set; }

        public Broadcaster(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            if (timer == null)
            {
                timer = new Timer(NotificationHandler.NotificationCheckEvent, null, TimeSpan.Zero, interval);
            }
        }

        public void Broadcast(object state)
        {
            Clients.All.Broadcast(state); //mega-inefficient, but meh!
        }
    }
}