using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class StudentResult
    {
        public string Id { get; set; }
        public int StudentdId { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }

        public string CourseName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
       
    }
}