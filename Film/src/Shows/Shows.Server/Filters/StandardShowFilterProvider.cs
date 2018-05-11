using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shows.Core;
using Shows.Core.Models;

namespace Shows.Server.Filters
{
    public class StandardShowFilterProvider : IFilterProvider<Show>
    {
        private List<Predicate<Show>> Predicates = new List<Predicate<Show>>();

        public StandardShowFilterProvider(string byTitleOrImdb, string byDescription, string byActors, string byGenre, DateTime byDateTimeFrom, DateTime byDateTimeTo, int byMinImdbRating, bool? byAvailability, ShowType byShowType)
        {
            if (!String.IsNullOrEmpty(byTitleOrImdb))
            {
                Predicates.Add(x => x.Title.FlattenString().Contains(byTitleOrImdb) || x.ImdbId.FlattenString() == byTitleOrImdb);
            }
            if (!String.IsNullOrEmpty(byDescription))
            {
                Predicates.Add(x => x.Description.FlattenString().Contains(byDescription));
            }
            if (!String.IsNullOrEmpty(byActors))
            {
                string[] actors = byActors.FlattenString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Predicates.Add(x => x.Actors.Split(',').Any(y => actors.Any(z => z.Contains(y.FlattenString()))));
            }
            if (!String.IsNullOrEmpty(byGenre))
            {
                Predicates.Add(x => x.Genre.FlattenString().Contains(byGenre));
            }
            Predicates.Add(x => x.ReleaseDate >= byDateTimeFrom);
            Predicates.Add(x => x.ReleaseDate <= byDateTimeTo);
            Predicates.Add(x => x.ImdbRating >= byMinImdbRating);
            if (byAvailability != null)
            {
                Predicates.Add(x => x.Available == byAvailability);
            }
            Predicates.Add(x => x.ShowType == byShowType);
        }

        public List<Predicate<Show>> GetFilters()
        {
            return Predicates;
        }
    }
}