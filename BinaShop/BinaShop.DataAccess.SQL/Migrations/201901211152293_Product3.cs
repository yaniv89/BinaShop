namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "Product_Id", "dbo.Products");
            DropIndex("dbo.Photos", new[] { "Product_Id" });
            DropTable("dbo.Photos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NameOfPhoto = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Product_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Photos", "Product_Id");
            AddForeignKey("dbo.Photos", "Product_Id", "dbo.Products", "Id");
        }
    }
}
