using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shows.Core;
using Shows.Core.Models;

namespace Shows.Server.Filters
{
    public class StandardShowFilter : Filter<Show>
    {
        public StandardShowFilter(string byTitleOrImdb, string byDescription, string byActors, string byGenre, DateTime byDateTimeFrom, DateTime byDateTimeTo, int byMinImdbRating)
        {
            if (!String.IsNullOrEmpty(byTitleOrImdb))
            {
                this.AddFilter(x => x.Title.FlattenString().Contains(byTitleOrImdb) || x.ImdbId.FlattenString() == byTitleOrImdb);
            }
            if (!String.IsNullOrEmpty(byDescription))
            {
                this.AddFilter(x => x.Description.FlattenString().Contains(byDescription));
            }
            if (!String.IsNullOrEmpty(byActors))
            {
                string[] actors = byActors.FlattenString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                this.AddFilter(x => x.Actors.Split(',').Any(y => actors.Any(z => z.Contains(y.FlattenString()))));
            }
            if (!String.IsNullOrEmpty(byGenre))
            {
                this.AddFilter(x => x.Genre.FlattenString().Contains(byGenre));
            }
            this.AddFilter(x => x.ReleaseDate >= byDateTimeFrom);
            this.AddFilter(x => x.ReleaseDate <= byDateTimeTo);
            this.AddFilter(x => x.ImdbRating >= byMinImdbRating);
        }
    }
}