using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Shows.Core.Models;
using Shows.Server.Database;

namespace Shows.Server.Controllers
{
    public class GroupController : ApiController
    {
        private ShowsDbContext dbContext = new ShowsDbContext();

        public List<UserGroup> Get(int userId)
        {
            return dbContext.Users.First(x => x.Id == userId).Groups.ToList();
        }

        public void Post(int userId, [FromBody] string groupName)
        {
            var user = dbContext.Users.First(x => x.Id == userId);

            var group = dbContext.UserGroups.FirstOrDefault(x => x.Description == groupName);

            if (group == null)
            {
                //new group
                group = new UserGroup()
                {
                    PublicId = Guid.NewGuid(),
                    Description = groupName
                };
                group = dbContext.UserGroups.Add(group);
                dbContext.SaveChanges();

                group = dbContext.UserGroups.First(x => x.Id == group.Id);

                group.Users.Add(user);
                dbContext.SaveChanges();
            }
            else
            {
                //add to group
                group.Users.Add(user);
                dbContext.SaveChanges();
            }
        }

        public void Delete(int userId, int groupId)
        {
            var user = dbContext.Users.First(x => x.Id == userId);

            var group = dbContext.UserGroups.First(x => x.Id == groupId);

            group.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
