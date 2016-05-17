using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class CourseEnrollment
    {
        
        public int Id { get; set; }

        [Display(Name = "Student Reg. No")]
        public int StudentId { get; set; }

        public int CourseId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date formate")]
        public DateTime EnrollDate { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

    }
}