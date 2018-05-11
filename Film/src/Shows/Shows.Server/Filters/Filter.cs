using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shows.Server.Filters
{
    public abstract class Filter<T>
    {
        protected List<Predicate<T>> Predicates;

        protected Filter(IFilterProvider<T> filterProvider)
        {
            Predicates = filterProvider.GetFilters();
        }

        public abstract IEnumerable<T> ApplyFilter(IEnumerable<T> source);
    }
}