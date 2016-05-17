namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentResultchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentResults", "GradeName", c => c.String());
            DropColumn("dbo.StudentResults", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentResults", "Grade", c => c.String());
            DropColumn("dbo.StudentResults", "GradeName");
        }
    }
}
