using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;

namespace Shows.Server.Controllers
{
    public class UserReviewController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public UserReview Get(int userId, int historyId)
        {
            var history = dbContext.UserHistory.First(x => x.Id == historyId);
            var show = history.Show;

            var review = show.UserReviews.FirstOrDefault(x => x.User.Id == userId);

            return review;
        }

        public void Post(int userId, int showId, [FromBody] Tuple<int, string> ratingAndReview)
        {
            var user = dbContext.Users.First(x => x.Id == userId);
            var show = dbContext.Shows.First(x => x.Id == showId);

            dbContext.UserReviews.Add(new UserReview()
            {
                PublicId = Guid.NewGuid(),
                Rating = ratingAndReview.Item1,
                Review = ratingAndReview.Item2,
                User = user,
                Show = show
            });
            dbContext.SaveChanges();
        }
    }
}