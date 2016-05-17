namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentresultView : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentResults", "StudentdId", "dbo.Students");
            DropForeignKey("dbo.StudentResults", "GradeId", "dbo.StudentGrades");
            DropIndex("dbo.StudentResults", new[] { "StudentdId" });
            DropIndex("dbo.StudentResults", new[] { "GradeId" });
            AddColumn("dbo.StudentResults", "CourseCode", c => c.String());
            AddColumn("dbo.StudentResults", "CourseName", c => c.String());
            AddColumn("dbo.StudentResults", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentResults", "Grade");
            DropColumn("dbo.StudentResults", "CourseName");
            DropColumn("dbo.StudentResults", "CourseCode");
            CreateIndex("dbo.StudentResults", "GradeId");
            CreateIndex("dbo.StudentResults", "StudentdId");
            AddForeignKey("dbo.StudentResults", "GradeId", "dbo.StudentGrades", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudentResults", "StudentdId", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
