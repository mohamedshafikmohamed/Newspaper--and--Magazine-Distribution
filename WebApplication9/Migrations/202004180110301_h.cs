namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "shop_id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "shop_id");
        }
    }
}
