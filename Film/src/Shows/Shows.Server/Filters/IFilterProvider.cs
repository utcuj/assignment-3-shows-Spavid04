using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shows.Server.Filters
{
    public interface IFilterProvider<T>
    {
        List<Predicate<T>> GetFilters();
    }
}