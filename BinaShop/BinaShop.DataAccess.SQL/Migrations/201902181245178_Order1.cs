namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Total", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Tax", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Subtotal", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Shipping", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Shipping", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Subtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
