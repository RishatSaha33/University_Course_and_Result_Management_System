namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departmrnts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptCode = c.String(),
                        DeptName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departmrnts");
        }
    }
}
