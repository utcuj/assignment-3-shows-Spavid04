namespace Shows.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                        Type = c.Int(nullable: false),
                        ForShow_Id = c.Int(),
                        ForUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.ForShow_Id)
                .ForeignKey("dbo.Users", t => t.ForUser_Id)
                .Index(t => t.ForShow_Id)
                .Index(t => t.ForUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ForUser_Id", "dbo.Users");
            DropForeignKey("dbo.Notifications", "ForShow_Id", "dbo.Shows");
            DropIndex("dbo.Notifications", new[] { "ForUser_Id" });
            DropIndex("dbo.Notifications", new[] { "ForShow_Id" });
            DropTable("dbo.Notifications");
        }
    }
}
