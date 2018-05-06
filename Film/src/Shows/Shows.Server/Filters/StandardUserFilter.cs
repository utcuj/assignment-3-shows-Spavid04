using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shows.Core;
using Shows.Core.Models;

namespace Shows.Server.Filters
{
    public class StandardUserFilter : Filter<User>
    {
        public StandardUserFilter(string byUsername, UserLevel? byUserLevel)
        {
            if (!String.IsNullOrEmpty(byUsername))
            {
                this.AddFilter(x => x.Username.FlattenString().Contains(byUsername));
            }
            if (byUserLevel != null)
            {
                this.AddFilter(x => x.UserLevel == byUserLevel);
            }
        }
    }
}