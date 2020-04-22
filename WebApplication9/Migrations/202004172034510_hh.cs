namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.subscripes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        customerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.customerId)
                .ForeignKey("dbo.products", t => t.productId, cascadeDelete: true)
                .Index(t => t.customerId)
                .Index(t => t.productId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.subscripes", "productId", "dbo.products");
            DropForeignKey("dbo.subscripes", "customerId", "dbo.AspNetUsers");
            DropIndex("dbo.subscripes", new[] { "productId" });
            DropIndex("dbo.subscripes", new[] { "customerId" });
            DropTable("dbo.subscripes");
        }
    }
}
