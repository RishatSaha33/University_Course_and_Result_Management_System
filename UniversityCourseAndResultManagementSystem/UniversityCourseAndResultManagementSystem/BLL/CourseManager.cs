using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.DAL;

namespace UniversityCourseAndResultManagementSystem.BLL
{
    public class CourseManager
    {
        CourseGateway courseGateway=new CourseGateway();
        public bool UnassignCourses()
        {
            return courseGateway.UnassignCourses();
        }
    }
}