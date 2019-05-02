namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceOffer3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceOffers", "PhoneNum", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceOffers", "PhoneNum");
        }
    }
}
