namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProjectStatementModelAndChangedProjectModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentProjects", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentProjects", "Project_Id", "dbo.Projects");
            DropIndex("dbo.StudentProjects", new[] { "Student_StudentId" });
            DropIndex("dbo.StudentProjects", new[] { "Project_Id" });
            RenameColumn(table: "dbo.Projects", name: "CourseId", newName: "Course_CourseId");
            RenameIndex(table: "dbo.Projects", name: "IX_CourseId", newName: "IX_Course_CourseId");
            CreateTable(
                "dbo.ProjectStatements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 30),
                        ProjectDescription = c.String(),
                        ProjectMaxSize = c.Double(),
                        ProjectDeadline = c.DateTime(nullable: false),
                        ProjectCreationDate = c.DateTime(nullable: false),
                        CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId);
            
            AddColumn("dbo.Files", "ProjectStatement_Id", c => c.Int());
            AddColumn("dbo.Projects", "ProjectStatementId", c => c.Int());
            AddColumn("dbo.Projects", "StudentId", c => c.Int());
            AddColumn("dbo.Projects", "ProjectMark", c => c.Double());
            AddColumn("dbo.Projects", "UploadedDate", c => c.DateTime());
            AddColumn("dbo.Projects", "AdditionalInfo", c => c.String());
            AddColumn("dbo.Projects", "FileName", c => c.String());
            AddColumn("dbo.Projects", "FileSize", c => c.Double(nullable: false));
            AddColumn("dbo.Projects", "FileContent", c => c.Binary());
            AddColumn("dbo.Projects", "ContentType", c => c.String());
            CreateIndex("dbo.Files", "ProjectStatement_Id");
            CreateIndex("dbo.Projects", "ProjectStatementId");
            CreateIndex("dbo.Projects", "StudentId");
            AddForeignKey("dbo.Files", "ProjectStatement_Id", "dbo.ProjectStatements", "Id");
            AddForeignKey("dbo.Projects", "ProjectStatementId", "dbo.ProjectStatements", "Id");
            AddForeignKey("dbo.Projects", "StudentId", "dbo.Students", "StudentId");
            DropColumn("dbo.Projects", "ProjectName");
            DropColumn("dbo.Projects", "ProjectDescription");
            DropColumn("dbo.Projects", "ProjectUploadingDate");
            DropColumn("dbo.Projects", "ProjectDeadline");
            DropColumn("dbo.Projects", "Mark");
            DropTable("dbo.StudentProjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentProjects",
                c => new
                    {
                        Student_StudentId = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_StudentId, t.Project_Id });
            
            AddColumn("dbo.Projects", "Mark", c => c.Double());
            AddColumn("dbo.Projects", "ProjectDeadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "ProjectUploadingDate", c => c.DateTime());
            AddColumn("dbo.Projects", "ProjectDescription", c => c.String());
            AddColumn("dbo.Projects", "ProjectName", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.Projects", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Projects", "ProjectStatementId", "dbo.ProjectStatements");
            DropForeignKey("dbo.Files", "ProjectStatement_Id", "dbo.ProjectStatements");
            DropForeignKey("dbo.ProjectStatements", "CourseId", "dbo.Courses");
            DropIndex("dbo.ProjectStatements", new[] { "CourseId" });
            DropIndex("dbo.Projects", new[] { "StudentId" });
            DropIndex("dbo.Projects", new[] { "ProjectStatementId" });
            DropIndex("dbo.Files", new[] { "ProjectStatement_Id" });
            DropColumn("dbo.Projects", "ContentType");
            DropColumn("dbo.Projects", "FileContent");
            DropColumn("dbo.Projects", "FileSize");
            DropColumn("dbo.Projects", "FileName");
            DropColumn("dbo.Projects", "AdditionalInfo");
            DropColumn("dbo.Projects", "UploadedDate");
            DropColumn("dbo.Projects", "ProjectMark");
            DropColumn("dbo.Projects", "StudentId");
            DropColumn("dbo.Projects", "ProjectStatementId");
            DropColumn("dbo.Files", "ProjectStatement_Id");
            DropTable("dbo.ProjectStatements");
            RenameIndex(table: "dbo.Projects", name: "IX_Course_CourseId", newName: "IX_CourseId");
            RenameColumn(table: "dbo.Projects", name: "Course_CourseId", newName: "CourseId");
            CreateIndex("dbo.StudentProjects", "Project_Id");
            CreateIndex("dbo.StudentProjects", "Student_StudentId");
            AddForeignKey("dbo.StudentProjects", "Project_Id", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudentProjects", "Student_StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
