namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopBanner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopBanners",
                c => new
                    {
                        BannerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Sorting = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BannerId);
            
        }
        
        public override void Down()
        {
            
            
            DropTable("dbo.ShopBanners");
        }
    }
}
