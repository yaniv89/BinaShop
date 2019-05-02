namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryPic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryPics",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GalleryPics");
        }
    }
}
