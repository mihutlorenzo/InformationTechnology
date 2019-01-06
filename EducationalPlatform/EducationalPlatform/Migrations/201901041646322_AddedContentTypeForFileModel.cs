namespace EducationalPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedContentTypeForFileModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "ContentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "ContentType");
        }
    }
}
