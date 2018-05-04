using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Owin;

namespace Shows.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true, //todo remove when unneeded
                EnableJavaScriptProxies = false
            };
            app.MapSignalR(hubConfiguration);
        }
    }
}