namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvenBetterShoppingCart : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PendantProducts", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.PendantProducts", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PendantProducts", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.PendantProducts", name: "User_Id", newName: "ApplicationUser_Id");
        }
    }
}
