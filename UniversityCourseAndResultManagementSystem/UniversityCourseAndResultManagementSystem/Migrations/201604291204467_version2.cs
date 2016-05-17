namespace UniversityCourseAndResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departmrnts", "DeptCode", c => c.String(nullable: false, maxLength: 7));
            AlterColumn("dbo.Departmrnts", "DeptName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departmrnts", "DeptName", c => c.String());
            AlterColumn("dbo.Departmrnts", "DeptCode", c => c.String());
        }
    }
}
