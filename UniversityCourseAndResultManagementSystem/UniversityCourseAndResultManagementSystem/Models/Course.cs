using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Course
    {
        public int Id { set; get; }


        [Display(Name ="Code" )]
        [Required(ErrorMessage = "Enter Course code")]
        [MinLength(5,ErrorMessage = "Course code must be 5 Charecter long")]
        [Remote("CourseCodeExists","Courses",ErrorMessage = "Course Code already exist")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Enter Course name")]
        [Display(Name = "Name")]
        [Remote("CourseNameExists", "Courses", ErrorMessage = "Course name already exist")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Enter Course credit"),Range(0.5,5,ErrorMessage = "Credit must be betwwen 0.5 to 5")]
        [Display(Name = "Credit")]
        public double CourseCredit { get; set; }

        [Required(ErrorMessage = "Enter Course description")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string CourseDescription { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId{ get; set; }

        
        [Display(Name = "Semester")]
        public int SemisterId{ get; set; }

        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual  Teacher Teacher { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Departmrnt Departmrnt { get; set; }


        [ForeignKey("SemisterId")]
        public virtual Semister Semister { get; set; }

        public int StudentId { get; set; }
        public virtual List<Student>  Student{ get; set; }
       

    }
}