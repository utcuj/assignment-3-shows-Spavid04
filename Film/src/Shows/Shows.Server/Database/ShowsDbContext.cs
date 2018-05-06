using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Shows.Core.Models;

namespace Shows.Server.Database
{
    public class ShowsDbContext : DbContext
    {
        public ShowsDbContext()
        {
            this.Database.Connection.ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=shows;Integrated Security=True;Connect Timeout=5;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; //HARDCODED
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<UserShowHistory> UserHistory { get; set; }

        public DbSet<Notification> Notifications { get; set; }
    }
}