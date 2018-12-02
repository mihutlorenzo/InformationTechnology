namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Semester_Year_Added_Group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Byte(nullable: false),
                        GroupName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.GroupId);
            
            AddColumn("dbo.Students", "GroupId", c => c.Byte());
            AddColumn("dbo.Semesters", "SemesterName", c => c.String(nullable: false, maxLength: 60));
            AddColumn("dbo.Years", "YearName", c => c.String());
            CreateIndex("dbo.Students", "GroupId");
            AddForeignKey("dbo.Students", "GroupId", "dbo.Groups", "GroupId");
            DropColumn("dbo.Semesters", "SemesterValue");
            DropColumn("dbo.Years", "YearValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Years", "YearValue", c => c.Byte(nullable: false));
            AddColumn("dbo.Semesters", "SemesterValue", c => c.Byte(nullable: false));
            DropForeignKey("dbo.Students", "GroupId", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "GroupId" });
            DropColumn("dbo.Years", "YearName");
            DropColumn("dbo.Semesters", "SemesterName");
            DropColumn("dbo.Students", "GroupId");
            DropTable("dbo.Groups");
        }
    }
}
