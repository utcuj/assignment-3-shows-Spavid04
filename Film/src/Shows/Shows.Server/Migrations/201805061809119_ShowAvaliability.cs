namespace Shows.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowAvaliability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shows", "Available", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shows", "Available");
        }
    }
}
