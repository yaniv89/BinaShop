namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(),
                        ProductId = c.String(maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "ProductId", "dbo.Products");
            DropIndex("dbo.Photos", new[] { "ProductId" });
            DropTable("dbo.Photos");
        }
    }
}
