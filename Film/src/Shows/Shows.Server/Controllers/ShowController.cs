using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;
using Shows.Server.Filters;
using Shows.Server.Notifications;

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
            DateTime date1, DateTime date2, int imdbRating, bool? available)
        {
            var filter = new StandardShowFilter(String.IsNullOrEmpty(imdbId) ? title : imdbId, description, actors,
                genre, date1, date2, imdbRating, available);

            var shows = filter.ApplyFilter(dbContext.Shows).ToList();

            return shows;
        }

        public void Post([FromBody] Show show)
        {
            bool shouldNotify = false;
            if (Get(show.Id) != null)
            {
                var oldShow = Get(show.Id);

                if (oldShow.Available == false && show.Available == true)
                {
                    shouldNotify = true;
                }
            }

            if (show.PublicId == Guid.Empty)
            {
                //new
                show.PublicId = Guid.NewGuid();
            }

            dbContext.Shows.AddOrUpdate(show);
            dbContext.SaveChanges();

            show = dbContext.Shows.First(x => x.Id == show.Id);

            if (shouldNotify)
            {
                foreach (var interest in show.UserInterests)
                {
                    var notification = new Notification()
                    {
                        ForUser = interest.User,
                        ForShow = show,
                        Details = "It has now become available!",
                        Type = NotificationType.ShowAvailability
                    };
                    NotificationHandler.AddNewNotification(dbContext, notification);
                }
            }
        }

        public void Delete(int showId)
        {
            var show = dbContext.Shows.First(x => x.Id == showId);

            dbContext.Shows.Remove(show);
            dbContext.SaveChanges();
        }
    }
}