namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "Product_Id", "dbo.Products");
            DropIndex("dbo.Photos", new[] { "Product_Id" });
            DropTable("dbo.Photos");
        }
    }
}
