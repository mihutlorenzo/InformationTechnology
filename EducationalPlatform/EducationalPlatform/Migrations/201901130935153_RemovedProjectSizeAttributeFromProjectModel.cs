namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedProjectSizeAttributeFromProjectModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Projects", "ProjectSize");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "ProjectSize", c => c.Double());
        }
    }
}
