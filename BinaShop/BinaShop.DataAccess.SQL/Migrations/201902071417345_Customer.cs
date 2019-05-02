namespace BinaShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "FirstName");
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "Street");
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Customers", "ZipCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "ZipCode", c => c.String());
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "Street", c => c.String());
            AddColumn("dbo.Customers", "LastName", c => c.String());
            AddColumn("dbo.Customers", "FirstName", c => c.String());
        }
    }
}
