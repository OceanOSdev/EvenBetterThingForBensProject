namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceIsNotSecure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendantProducts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PendantProducts", "Price");
        }
    }
}
