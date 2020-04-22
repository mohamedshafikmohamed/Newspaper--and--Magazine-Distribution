namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.products", "price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.products", "price", c => c.Double(nullable: false));
        }
    }
}
