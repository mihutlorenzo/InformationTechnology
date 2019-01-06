namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTheNameOfPrimaryKeyForProjectAndProjectStatement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Files", "ProjectStatement_Id", "dbo.ProjectStatements");
            DropForeignKey("dbo.Projects", "ProjectStatementId", "dbo.ProjectStatements");
            RenameColumn(table: "dbo.Files", name: "ProjectStatement_Id", newName: "ProjectStatementId");
            RenameIndex(table: "dbo.Files", name: "IX_ProjectStatement_Id", newName: "IX_ProjectStatementId");
            DropPrimaryKey("dbo.Files");
            DropPrimaryKey("dbo.Projects");
            DropPrimaryKey("dbo.ProjectStatements");
            DropColumn("dbo.Files", "Id");
            DropColumn("dbo.Projects", "Id");
            DropColumn("dbo.ProjectStatements", "Id");
            AddColumn("dbo.Files", "FileId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Projects", "ProjectId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ProjectStatements", "ProjectStatementId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Files", "FileId");
            AddPrimaryKey("dbo.Projects", "ProjectId");
            AddPrimaryKey("dbo.ProjectStatements", "ProjectStatementId");
            AddForeignKey("dbo.Files", "ProjectId", "dbo.Projects", "ProjectId");
            AddForeignKey("dbo.Files", "ProjectStatementId", "dbo.ProjectStatements", "ProjectStatementId");
            AddForeignKey("dbo.Projects", "ProjectStatementId", "dbo.ProjectStatements", "ProjectStatementId");
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectStatements", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Projects", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Files", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Projects", "ProjectStatementId", "dbo.ProjectStatements");
            DropForeignKey("dbo.Files", "ProjectStatementId", "dbo.ProjectStatements");
            DropForeignKey("dbo.Files", "ProjectId", "dbo.Projects");
            DropPrimaryKey("dbo.ProjectStatements");
            DropPrimaryKey("dbo.Projects");
            DropPrimaryKey("dbo.Files");
            DropColumn("dbo.ProjectStatements", "ProjectStatementId");
            DropColumn("dbo.Projects", "ProjectId");
            DropColumn("dbo.Files", "FileId");
            AddPrimaryKey("dbo.ProjectStatements", "Id");
            AddPrimaryKey("dbo.Projects", "Id");
            AddPrimaryKey("dbo.Files", "Id");
            RenameIndex(table: "dbo.Files", name: "IX_ProjectStatementId", newName: "IX_ProjectStatement_Id");
            RenameColumn(table: "dbo.Files", name: "ProjectStatementId", newName: "ProjectStatement_Id");
            AddForeignKey("dbo.Projects", "ProjectStatementId", "dbo.ProjectStatements", "Id");
            AddForeignKey("dbo.Files", "ProjectStatement_Id", "dbo.ProjectStatements", "Id");
            AddForeignKey("dbo.Files", "ProjectId", "dbo.Projects", "Id");
        }
    }
}
