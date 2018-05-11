using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shows.Server.Filters
{
    public class AndFilter<T> : Filter<T>
    {
        public override IEnumerable<T> ApplyFilter(IEnumerable<T> source)
        {
            IEnumerable<T> result = source;

            foreach (var filter in Predicates)
            {
                result = result.Where(x => filter(x) == true);
            }

            return result;
        }

        public AndFilter(IFilterProvider<T> filterProvider) : base(filterProvider)
        {
        }
    }
}