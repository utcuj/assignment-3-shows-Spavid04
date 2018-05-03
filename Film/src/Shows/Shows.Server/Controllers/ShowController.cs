using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;
using Shows.Server.Filters;

namespace Shows.Server.Controllers
{
    public class ShowController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public Show Get(int id)
        {
            var show = dbContext.Shows.FirstOrDefault(x => x.Id == id);

            return show;
        }

        public List<Show> Get(string title, string actors, string description, string genre, string imdbId,
            DateTime date1, DateTime date2, int imdbRating)
        {
            var filter = new StandardShowFilter(String.IsNullOrEmpty(imdbId) ? title : imdbId, description, actors,
                genre, date1, date2, imdbRating);

            var shows = filter.ApplyFilter(dbContext.Shows).ToList();

            return shows;
        }
    }
}