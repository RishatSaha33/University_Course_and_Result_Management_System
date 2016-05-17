namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resultmodelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentResults", "CourseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentResults", "CourseId");
        }
    }
}
