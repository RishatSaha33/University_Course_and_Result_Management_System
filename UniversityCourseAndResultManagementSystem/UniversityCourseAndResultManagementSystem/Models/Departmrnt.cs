using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Departmrnt
    {
        public int Id { set; get; }
        [Display(Name="Code")]
        [Required(ErrorMessage = "Please enter Department code"), MinLength(2, ErrorMessage = "Use minimum two character"), MaxLength(7, ErrorMessage = "Use maximum seven character")]
        [Remote("DepartmentCodeExits", "Departments", ErrorMessage = "Department code has been already exists.")]
        public string DeptCode { set; get; }
        [Display(Name = "Name")]
         [Required(ErrorMessage = "Please enter Department Name")]
         [Remote("DepartmentNameExits", "Departments", ErrorMessage = "Department name has been already exsits.")]
        public string DeptName { set; get; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; } 

    }
}