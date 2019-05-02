namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopBanner1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ShopBanners");
            AddColumn("dbo.ShopBanners", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ShopBanners", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddPrimaryKey("dbo.ShopBanners", "Id");
            DropColumn("dbo.ShopBanners", "BannerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopBanners", "BannerId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ShopBanners");
            DropColumn("dbo.ShopBanners", "CreatedAt");
            DropColumn("dbo.ShopBanners", "Id");
            AddPrimaryKey("dbo.ShopBanners", "BannerId");
        }
    }
}
