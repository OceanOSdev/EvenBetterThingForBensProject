namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pendants", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pendants", "ApplicationUser_Id");
            AddForeignKey("dbo.Pendants", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pendants", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Pendants", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Pendants", "ApplicationUser_Id");
        }
    }
}
