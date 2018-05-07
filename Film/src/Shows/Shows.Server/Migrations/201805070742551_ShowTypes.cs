namespace Shows.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shows", "ShowType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shows", "ShowType");
        }
    }
}
