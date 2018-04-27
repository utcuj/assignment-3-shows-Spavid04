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
            this.Database.Connection.ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=shows;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<ShowEvent> ShowEvents { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
    }
}