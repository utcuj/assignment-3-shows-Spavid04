namespace Shows.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFriendship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friendships", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Friendships", "User1_Id", "dbo.Users");
            DropForeignKey("dbo.Friendships", "User2_Id", "dbo.Users");
            DropIndex("dbo.Friendships", new[] { "User_Id" });
            DropIndex("dbo.Friendships", new[] { "User1_Id" });
            DropIndex("dbo.Friendships", new[] { "User2_Id" });
            DropTable("dbo.Friendships");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Friendships", "User2_Id");
            CreateIndex("dbo.Friendships", "User1_Id");
            CreateIndex("dbo.Friendships", "User_Id");
            AddForeignKey("dbo.Friendships", "User2_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Friendships", "User1_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Friendships", "User_Id", "dbo.Users", "Id");
        }
    }
}
