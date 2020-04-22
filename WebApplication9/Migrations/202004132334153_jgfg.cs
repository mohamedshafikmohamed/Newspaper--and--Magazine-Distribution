namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jgfg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        deleveryboyId = c.Int(nullable: false),
                        custId = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.deleveryboys", t => t.deleveryboyId, cascadeDelete: true)
                .ForeignKey("dbo.products", t => t.productId, cascadeDelete: true)
                .Index(t => t.deleveryboyId)
                .Index(t => t.productId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.orders", "productId", "dbo.products");
            DropForeignKey("dbo.orders", "deleveryboyId", "dbo.deleveryboys");
            DropIndex("dbo.orders", new[] { "productId" });
            DropIndex("dbo.orders", new[] { "deleveryboyId" });
            DropTable("dbo.orders");
        }
    }
}
