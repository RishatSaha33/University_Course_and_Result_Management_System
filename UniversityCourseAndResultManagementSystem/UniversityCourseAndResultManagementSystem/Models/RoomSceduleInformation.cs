using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class RoomSceduleInformation
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string RoomName { get; set; }
        public string Day { get; set; }
        public string StartTime{ get; set; }
        public string EndTime { get; set; }
    }
}