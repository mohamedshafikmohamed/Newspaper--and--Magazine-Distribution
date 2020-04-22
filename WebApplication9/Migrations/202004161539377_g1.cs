namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class g1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.orders", "deleveryboyId", "dbo.deleveryboys");
            DropIndex("dbo.orders", new[] { "deleveryboyId" });
            AlterColumn("dbo.orders", "deleveryboyId", c => c.String(maxLength: 128));
            CreateIndex("dbo.orders", "deleveryboyId");
            AddForeignKey("dbo.orders", "deleveryboyId", "dbo.AspNetUsers", "Id");
            DropTable("dbo.deleveryboys");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.deleveryboys",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        line = c.String(),
                        phone = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.orders", "deleveryboyId", "dbo.AspNetUsers");
            DropIndex("dbo.orders", new[] { "deleveryboyId" });
            AlterColumn("dbo.orders", "deleveryboyId", c => c.Int(nullable: false));
            CreateIndex("dbo.orders", "deleveryboyId");
            AddForeignKey("dbo.orders", "deleveryboyId", "dbo.deleveryboys", "id", cascadeDelete: true);
        }
    }
}
