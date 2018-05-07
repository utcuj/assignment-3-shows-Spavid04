using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Shows.Client.Forms;
using Shows.Core.Models;

namespace Shows.Client
{
    public static class NotificationReceiever
    {
        public static async void InitilizeHub(User currentUser)
        {
            var hubConnection = new HubConnection(ConfigurationManager.AppSettings["signalrUri"]);
            var hubProxy = hubConnection.CreateHubProxy("NotificationHub");

            hubProxy
                .On<Notification>("Broadcast",
                    async data =>
                    {
                        UserForm.TryShowNotification(currentUser, data);
                    });
            await hubConnection.Start();
        }
    }
}
