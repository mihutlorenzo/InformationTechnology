namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentityModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "UserTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.AspNetUsers", "UserTypeId");
            AddForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes", "UserTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.AspNetUsers", new[] { "UserTypeId" });
            DropColumn("dbo.AspNetUsers", "UserTypeId");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
