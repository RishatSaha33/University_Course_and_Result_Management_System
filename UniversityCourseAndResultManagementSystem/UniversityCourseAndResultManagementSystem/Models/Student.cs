using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }


        [Display(Name = " Name")]
        [Required (ErrorMessage = "Please enter Name")]
        public string StudentName { get; set; }
        
        [Display(Name ="Email")]
        [EmailAddress(ErrorMessage = "Enter valid email"),StringLength(50)]
         [Required(ErrorMessage = "Please enter Email")]
         [Remote("StudentEmailExits", "Students", ErrorMessage = "Email already exists.Try with another email.")]
        public string StudentEmail{ get; set; }


        [Display(Name = "Contact No.")]
         [Required(ErrorMessage = "Please enter Contact No")]
        public string StudentContactNo { get; set; }
        [Display(Name = "Date")]
         [Required(ErrorMessage = "Please enter Registration date")]
        public DateTime RegistrationDate  { get; set; }


        [Display(Name = "Address")]
         [Required(ErrorMessage = "Please enter Address")]
        [DataType(DataType.MultilineText)]

        public string StudentAddress { get; set; }
       [Display(Name = "Registration Number")]
       //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string RegistrationNo { get; set; }

       public int? CourseId { get; set; }
       [ForeignKey("CourseId")]
       public virtual List<Course> Course { get; set; }
         
      
         [Display(Name = "Department")]
         [Required(ErrorMessage = "Please enter Department")]
        public int DepartmentId { get; set; }

       
        public virtual StudentGrade StudentGrade{ get; set; }
        [ForeignKey("DepartmentId")]
         public virtual Departmrnt Departmrnt { get; set; }
    }
}