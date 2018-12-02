namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedColumnNameFromCodes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Codes", "TeacherEmail", c => c.String());
            DropColumn("dbo.Codes", "TeacherLastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Codes", "TeacherLastName", c => c.String());
            DropColumn("dbo.Codes", "TeacherEmail");
        }
    }
}
