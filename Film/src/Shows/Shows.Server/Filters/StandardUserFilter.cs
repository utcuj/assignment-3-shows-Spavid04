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
        public StandardUserFilter(string byUsername)
        {
            if (!String.IsNullOrEmpty(byUsername))
            {
                this.AddFilter(x => x.Username.FlattenString().Contains(byUsername));
            }
        }
    }
}