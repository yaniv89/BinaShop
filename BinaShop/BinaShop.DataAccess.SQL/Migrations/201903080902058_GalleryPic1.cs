namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryPic1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "GalleryPic_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Photos", "GalleryPic_Id");
            AddForeignKey("dbo.Photos", "GalleryPic_Id", "dbo.GalleryPics", "Id");
            DropColumn("dbo.GalleryPics", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryPics", "ImageUrl", c => c.String());
            DropForeignKey("dbo.Photos", "GalleryPic_Id", "dbo.GalleryPics");
            DropIndex("dbo.Photos", new[] { "GalleryPic_Id" });
            DropColumn("dbo.Photos", "GalleryPic_Id");
        }
    }
}
