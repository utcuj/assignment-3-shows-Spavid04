namespace Shows.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicId = c.Guid(nullable: false),
                        User_Id = c.Int(),
                        User1_Id = c.Int(),
                        User2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User1_Id)
                .ForeignKey("dbo.Users", t => t.User2_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User1_Id)
                .Index(t => t.User2_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicId = c.Guid(nullable: false),
                        UserLevel = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicId = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInterests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicId = c.Guid(nullable: false),
                        Show_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.Show_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Show_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicId = c.Guid(nullable: false),
                        Title = c.String(),
                        Actors = c.String(),
                        Description = c.String(),
                        Genre = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        ImdbId = c.String(),
                        ImdbRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicId = c.Guid(nullable: false),
                        Review = c.String(),
                        Rating = c.Int(nullable: false),
                        Show_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.Show_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Show_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserGroupUsers",
                c => new
                    {
                        UserGroup_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_Id, t.User_Id })
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendships", "User2_Id", "dbo.Users");
            DropForeignKey("dbo.Friendships", "User1_Id", "dbo.Users");
            DropForeignKey("dbo.UserInterests", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserInterests", "Show_Id", "dbo.Shows");
            DropForeignKey("dbo.UserReviews", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserReviews", "Show_Id", "dbo.Shows");
            DropForeignKey("dbo.UserGroupUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserGroupUsers", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.Friendships", "User_Id", "dbo.Users");
            DropIndex("dbo.UserGroupUsers", new[] { "User_Id" });
            DropIndex("dbo.UserGroupUsers", new[] { "UserGroup_Id" });
            DropIndex("dbo.UserReviews", new[] { "User_Id" });
            DropIndex("dbo.UserReviews", new[] { "Show_Id" });
            DropIndex("dbo.UserInterests", new[] { "User_Id" });
            DropIndex("dbo.UserInterests", new[] { "Show_Id" });
            DropIndex("dbo.Friendships", new[] { "User2_Id" });
            DropIndex("dbo.Friendships", new[] { "User1_Id" });
            DropIndex("dbo.Friendships", new[] { "User_Id" });
            DropTable("dbo.UserGroupUsers");
            DropTable("dbo.UserReviews");
            DropTable("dbo.Shows");
            DropTable("dbo.UserInterests");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Users");
            DropTable("dbo.Friendships");
        }
    }
}
