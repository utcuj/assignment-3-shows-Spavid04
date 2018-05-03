using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Shows.Server.Notifications
{
    public class Broadcaster
    {
        private readonly TimeSpan _updateInterval =
            TimeSpan.FromSeconds(1);
        private Timer _timer;

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
            _timer = new Timer(Broadcast, null, _updateInterval, _updateInterval);
        }

        public void Broadcast(object state)
        {
            Clients.All.Broadcast(DateTime.Now); //todo change
        }
    }
}