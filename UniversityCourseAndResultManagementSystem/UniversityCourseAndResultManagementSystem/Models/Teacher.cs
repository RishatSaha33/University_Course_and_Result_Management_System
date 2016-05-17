using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Teacher name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address ")]
          [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter email ")]
        [EmailAddress(ErrorMessage = "Email address not valid")]
        [Display(Name = "Email")]
        [Remote("TeacherEmaiexists","Teachers",ErrorMessage = "Email already exists")]
        public string TeacherEmail { get; set; }

        [Required(ErrorMessage ="Enter contact no" )]
          [Display(Name = "Contact No.")]
        public string TeacherContactInfo { get; set; }

        [Required(ErrorMessage = "Enter cridt to be taken")]
        [Range(0.5,double.MaxValue,ErrorMessage = "Credit can not be negative")]
          [Display(Name = "Credit to be taken")]
        public double TeacherCredit { get; set; }

        public double RemainingCredit { get; set; }

        [Required(ErrorMessage = "Select Designation")]
          [Display(Name = "Designation")]
        public int DesignationId { get; set; }

         [Required(ErrorMessage = "Select Department")]
          [Display(Name = "Department")]
        public int DepartmentId { get; set; }


        [ForeignKey("DepartmentId")]
          public virtual Departmrnt Departmrnt { get; set; }


        [ForeignKey("DesignationId")]
        public virtual Designation Designation { get; set; }
    }
}