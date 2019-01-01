namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasket1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "ProductId", c => c.String());
            DropColumn("dbo.BasketItems", "ProuductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BasketItems", "ProuductId", c => c.String());
            DropColumn("dbo.BasketItems", "ProductId");
        }
    }
}
