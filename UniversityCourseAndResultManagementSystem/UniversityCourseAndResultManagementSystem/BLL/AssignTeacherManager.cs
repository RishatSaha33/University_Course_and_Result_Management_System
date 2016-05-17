using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.DAL;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.BLL
{
    public class AssignTeacherManager
    {
        AssignTeacherGateway assignTeacherGateway=new AssignTeacherGateway();
        public List<Departmrnt> GetAllDepartment()
        {
            return assignTeacherGateway.GetAllDepartment();
        }

        public List<Teacher> GetAllTeacherByDepartment(int departmentId)
        {
            return assignTeacherGateway.GetAllTeacherByDepartment(departmentId);
        }
    }
}