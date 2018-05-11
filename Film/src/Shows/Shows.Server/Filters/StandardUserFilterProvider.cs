using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shows.Core;
using Shows.Core.Models;

namespace Shows.Server.Filters
{
    public class StandardUserFilterProvider : IFilterProvider<User>
    {
        private List<Predicate<User>> Predicates = new List<Predicate<User>>();

        public StandardUserFilterProvider(string byUsername, UserLevel? byUserLevel)
        {
            if (!String.IsNullOrEmpty(byUsername))
            {
                Predicates.Add(x => x.Username.FlattenString().Contains(byUsername));
            }
            if (byUserLevel != null)
            {
                Predicates.Add(x => x.UserLevel == byUserLevel);
            }
        }

        public List<Predicate<User>> GetFilters()
        {
            return Predicates;
        }
    }
}