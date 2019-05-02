namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceOffer2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PriceOffers", "Date", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PriceOffers", "Date", c => c.DateTime(nullable: false));
        }
    }
}
