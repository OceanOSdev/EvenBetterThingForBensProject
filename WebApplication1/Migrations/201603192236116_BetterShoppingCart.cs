namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BetterShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyUserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PendantProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        PendantSize = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "MyUserInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "MyUserInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "MyUserInfo_Id", "dbo.MyUserInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PendantProducts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "MyUserInfo_Id", "dbo.MyUserInfoes");
            DropIndex("dbo.PendantProducts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "MyUserInfo_Id" });
            DropColumn("dbo.AspNetUsers", "MyUserInfo_Id");
            DropTable("dbo.PendantProducts");
            DropTable("dbo.MyUserInfoes");
        }
    }
}
