using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class RoomAllocation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select a Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please select a Room No.")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Please select a Day")]
        public int DayId { get; set; }
        [Required(ErrorMessage = "Please enter when class will start")]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Please enter when class will end")]
        public string EndTime { get; set; }
        [ForeignKey(("DepartmentId"))]
        public virtual Departmrnt Departmrnt { get; set; }
        public virtual Course Course { get; set; }
        public virtual Room Room { get; set; }
        public virtual Day Day { get; set; }
    }
}