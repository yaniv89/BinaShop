namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Total", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Tax", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Subtotal", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Shipping", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PayPalReference", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PayPalReference");
            DropColumn("dbo.Orders", "Shipping");
            DropColumn("dbo.Orders", "Subtotal");
            DropColumn("dbo.Orders", "Tax");
            DropColumn("dbo.Orders", "Total");
        }
    }
}
