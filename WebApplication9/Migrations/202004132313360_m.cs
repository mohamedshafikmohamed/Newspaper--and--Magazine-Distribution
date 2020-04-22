namespace WebApplication9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.products", "delvaryboyId", "dbo.deleveryboys");
            DropIndex("dbo.products", new[] { "delvaryboyId" });
            DropColumn("dbo.products", "supplierId");
            RenameColumn(table: "dbo.products", name: "supplier_Id", newName: "supplierId");
            AddColumn("dbo.products", "description", c => c.String());
            AddColumn("dbo.products", "image", c => c.String());
            AlterColumn("dbo.products", "price", c => c.Double(nullable: false));
            AlterColumn("dbo.products", "supplierId", c => c.String(maxLength: 128));
            DropColumn("dbo.deleveryboys", "productsId");
            DropColumn("dbo.products", "discription");
            DropColumn("dbo.products", "delvaryboyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.products", "delvaryboyId", c => c.Int(nullable: false));
            AddColumn("dbo.products", "discription", c => c.String());
            AddColumn("dbo.deleveryboys", "productsId", c => c.Int(nullable: false));
            AlterColumn("dbo.products", "supplierId", c => c.Int(nullable: false));
            AlterColumn("dbo.products", "price", c => c.Int(nullable: false));
            DropColumn("dbo.products", "image");
            DropColumn("dbo.products", "description");
            RenameColumn(table: "dbo.products", name: "supplierId", newName: "supplier_Id");
            AddColumn("dbo.products", "supplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.products", "delvaryboyId");
            AddForeignKey("dbo.products", "delvaryboyId", "dbo.deleveryboys", "id", cascadeDelete: true);
        }
    }
}
