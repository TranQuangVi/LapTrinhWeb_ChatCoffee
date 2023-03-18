namespace ChatCoffee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Viewcount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.COFFEE", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.COFFEE", "ViewCount");
        }
    }
}
