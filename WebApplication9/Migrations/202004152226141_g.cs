namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class g : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.products", "stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.products", "stock");
        }
    }
}
