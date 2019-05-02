namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopBanner4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopBanners", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopBanners", "Link");
        }
    }
}
