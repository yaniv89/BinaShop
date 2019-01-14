namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductListViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "currency", c => c.String());
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Products", "currency");
        }
    }
}
