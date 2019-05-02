namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceOffer1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceOffers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FromName = c.String(nullable: false),
                        FromEmail = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Sadnas = c.String(nullable: false),
                        NumOfPpl = c.Int(nullable: false),
                        Message = c.String(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PriceOffers");
        }
    }
}
