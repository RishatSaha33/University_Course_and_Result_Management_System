namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "TeacherId", c => c.Int());
            AddColumn("dbo.Students", "Course_Id", c => c.Int());
            CreateIndex("dbo.Students", "Course_Id");
            CreateIndex("dbo.Courses", "TeacherId");
            AddForeignKey("dbo.Students", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "Course_Id" });
            DropColumn("dbo.Students", "Course_Id");
            DropColumn("dbo.Courses", "TeacherId");
        }
    }
}
