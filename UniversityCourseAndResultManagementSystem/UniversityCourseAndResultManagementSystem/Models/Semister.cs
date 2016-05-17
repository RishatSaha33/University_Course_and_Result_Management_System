using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Semister
    {
        public int Id { get; set; }
        [Display(Name = "Semister Name")]
        public string SemisterName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}