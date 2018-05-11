using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shows.Server.Filters
{
    public class OrFilter<T> : Filter<T>
    {
        public override IEnumerable<T> ApplyFilter(IEnumerable<T> source)
        {
            IEnumerable<T> result = source;

            result = result.Where(x => Predicates.Any(y => y(x) == true));

            return result;
        }

        public OrFilter(IFilterProvider<T> filterProvider) : base(filterProvider)
        {
        }
    }
}