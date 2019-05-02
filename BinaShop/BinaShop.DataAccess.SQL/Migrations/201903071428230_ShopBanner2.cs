namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopBanner2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShopBanners", "Sorting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopBanners", "Sorting", c => c.Int(nullable: false));
        }
    }
}
