namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentresult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentGrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentResults",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StudentdId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentdId, cascadeDelete: true)
                .ForeignKey("dbo.StudentGrades", t => t.GradeId, cascadeDelete: true)
                .Index(t => t.StudentdId)
                .Index(t => t.GradeId);
            
            AddColumn("dbo.Students", "StudentGrade_Id", c => c.Int());
            CreateIndex("dbo.Students", "StudentGrade_Id");
            AddForeignKey("dbo.Students", "StudentGrade_Id", "dbo.StudentGrades", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentResults", "GradeId", "dbo.StudentGrades");
            DropForeignKey("dbo.StudentResults", "StudentdId", "dbo.Students");
            DropForeignKey("dbo.Students", "StudentGrade_Id", "dbo.StudentGrades");
            DropIndex("dbo.StudentResults", new[] { "GradeId" });
            DropIndex("dbo.StudentResults", new[] { "StudentdId" });
            DropIndex("dbo.Students", new[] { "StudentGrade_Id" });
            DropColumn("dbo.Students", "StudentGrade_Id");
            DropTable("dbo.StudentResults");
            DropTable("dbo.StudentGrades");
        }
    }
}
