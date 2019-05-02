namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo5 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Photos", name: "ProductId", newName: "Product_Id");
            RenameIndex(table: "dbo.Photos", name: "IX_ProductId", newName: "IX_Product_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Photos", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameColumn(table: "dbo.Photos", name: "Product_Id", newName: "ProductId");
        }
    }
}
