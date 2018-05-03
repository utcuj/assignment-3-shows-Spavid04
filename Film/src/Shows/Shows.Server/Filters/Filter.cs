using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shows.Server.Filters
{
    public abstract class Filter<T>
    {
        protected List<Predicate<T>> Filters = new List<Predicate<T>>();

        public IEnumerable<T> ApplyFilter(IEnumerable<T> source)
        {
            IEnumerable<T> result = source;

            foreach (var filter in Filters)
            {
                result = result.Where(x => filter(x) == true);
            }

            return result;
        }

        public void AddFilter(Predicate<T> predicate)
        {
            Filters.Add(predicate);
        }

        public bool Matches(T item)
        {
            if (Filters.Count == 0) return true;

            return Filters
                .Select(x => x(item))
                .All(x => x == true);
        }
    }
}