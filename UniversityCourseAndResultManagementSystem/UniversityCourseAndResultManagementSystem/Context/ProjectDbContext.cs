using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Migrations;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Context
{
    public class ProjectDbContext:DbContext
    {
        public DbSet<Departmrnt> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Semister> Semisters { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<RoomAllocation> RoomAllocations { get; set; }


    }
}