namespace Shows.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserShowHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublicId = c.Guid(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Show_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.Show_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Show_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserShowHistories", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserShowHistories", "Show_Id", "dbo.Shows");
            DropIndex("dbo.UserShowHistories", new[] { "User_Id" });
            DropIndex("dbo.UserShowHistories", new[] { "Show_Id" });
            DropTable("dbo.UserShowHistories");
        }
    }
}
