namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Codes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        CodeValue = c.String(),
                        TeacherEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false, maxLength: 100),
                        CourseDescription = c.String(),
                        SpecializationId = c.Byte(nullable: false),
                        SemesterId = c.Byte(nullable: false),
                        YearId = c.Byte(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Years", t => t.YearId, cascadeDelete: true)
                .Index(t => t.SpecializationId)
                .Index(t => t.SemesterId)
                .Index(t => t.YearId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Size = c.Double(nullable: false),
                        FileURI = c.String(),
                        FileContent = c.Binary(),
                        ContentType = c.String(),
                        UploadedDate = c.DateTime(nullable: false),
                        ProjectId = c.Int(),
                        CourseId = c.Int(),
                        StudentId = c.Int(),
                        ProjectStatementId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.ProjectStatements", t => t.ProjectStatementId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId)
                .Index(t => t.ProjectStatementId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectStatementId = c.Int(),
                        StudentId = c.Int(),
                        ProjectMark = c.Double(),
                        UploadedDate = c.DateTime(),
                        ProjectSize = c.Double(),
                        AdditionalInfo = c.String(),
                        FileName = c.String(),
                        FileSize = c.Double(nullable: false),
                        FileContent = c.Binary(),
                        ContentType = c.String(),
                        Course_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.ProjectStatements", t => t.ProjectStatementId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .Index(t => t.ProjectStatementId)
                .Index(t => t.StudentId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.ProjectStatements",
                c => new
                    {
                        ProjectStatementId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 30),
                        ProjectDescription = c.String(),
                        ProjectMaxSize = c.Double(),
                        ProjectDeadline = c.DateTime(nullable: false),
                        ProjectCreationDate = c.DateTime(nullable: false),
                        CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectStatementId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        SpecializationId = c.Byte(),
                        GroupId = c.Byte(),
                        SemesterId = c.Byte(),
                        YearId = c.Byte(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Semesters", t => t.SemesterId)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId)
                .ForeignKey("dbo.Years", t => t.YearId)
                .Index(t => t.SpecializationId)
                .Index(t => t.GroupId)
                .Index(t => t.SemesterId)
                .Index(t => t.YearId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        UserTypeId = c.Byte(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        UserTypeId = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Byte(nullable: false),
                        GroupName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterId = c.Byte(nullable: false),
                        SemesterName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.SemesterId);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        SpecializationId = c.Byte(nullable: false),
                        SpecializationName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.SpecializationId);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        YearId = c.Byte(nullable: false),
                        YearName = c.String(),
                    })
                .PrimaryKey(t => t.YearId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        RegisterCode = c.String(),
                        InfoAboutTeacher = c.String(),
                        URIToPersonalPage = c.String(),
                        TeacherStatus = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_StudentId = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_StudentId, t.Course_CourseId })
                .ForeignKey("dbo.Students", t => t.Student_StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Course_CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Courses", "YearId", "dbo.Years");
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Courses", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Projects", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Files", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Students", "YearId", "dbo.Years");
            DropForeignKey("dbo.Files", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Students", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Projects", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.StudentCourses", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "ProjectStatementId", "dbo.ProjectStatements");
            DropForeignKey("dbo.Files", "ProjectStatementId", "dbo.ProjectStatements");
            DropForeignKey("dbo.ProjectStatements", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Files", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "Course_CourseId" });
            DropIndex("dbo.StudentCourses", new[] { "Student_StudentId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Teachers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "UserTypeId" });
            DropIndex("dbo.Students", new[] { "ApplicationUserId" });
            DropIndex("dbo.Students", new[] { "YearId" });
            DropIndex("dbo.Students", new[] { "SemesterId" });
            DropIndex("dbo.Students", new[] { "GroupId" });
            DropIndex("dbo.Students", new[] { "SpecializationId" });
            DropIndex("dbo.ProjectStatements", new[] { "CourseId" });
            DropIndex("dbo.Projects", new[] { "Course_CourseId" });
            DropIndex("dbo.Projects", new[] { "StudentId" });
            DropIndex("dbo.Projects", new[] { "ProjectStatementId" });
            DropIndex("dbo.Files", new[] { "ProjectStatementId" });
            DropIndex("dbo.Files", new[] { "StudentId" });
            DropIndex("dbo.Files", new[] { "CourseId" });
            DropIndex("dbo.Files", new[] { "ProjectId" });
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropIndex("dbo.Courses", new[] { "YearId" });
            DropIndex("dbo.Courses", new[] { "SemesterId" });
            DropIndex("dbo.Courses", new[] { "SpecializationId" });
            DropTable("dbo.StudentCourses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Teachers");
            DropTable("dbo.Years");
            DropTable("dbo.Specializations");
            DropTable("dbo.Semesters");
            DropTable("dbo.Groups");
            DropTable("dbo.UserTypes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Students");
            DropTable("dbo.ProjectStatements");
            DropTable("dbo.Projects");
            DropTable("dbo.Files");
            DropTable("dbo.Courses");
            DropTable("dbo.Codes");
        }
    }
}
