using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls.WebParts;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.DAL
{
    public class AssignTeacherGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["ProjectDbContext"].ConnectionString;
         private SqlConnection connection;

        public AssignTeacherGateway()
        {
            connection= new SqlConnection(connectionString);
        }
        public List<Departmrnt> GetAllDepartment()
        {
            string query = "SELECT *FROM Departmrnts";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Departmrnt> departmentList = new List<Departmrnt>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Departmrnt department = new Departmrnt();
                    department.Id = int.Parse(reader["Id"].ToString());
                    department.DeptCode = reader["DeptCode"].ToString();
                    department.DeptName = reader["DeptName"].ToString();
                    departmentList.Add(department);

                }
                reader.Close();
            }
            connection.Close();
            return departmentList;
        }

        public List<Teacher> GetAllTeacherByDepartment(int departmentId)
        {
            string query = "SELECT *FROM Teachers WHERE DepartmentId='" + departmentId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Teacher> teachers = new List<Teacher>();
            Teacher teacher = null;
            if (reader.HasRows)
            {
                reader.Read();
                teacher = new Teacher();
                teacher.Id = int.Parse(reader["Id"].ToString());
                teacher.Name = reader["Name"].ToString();
                teacher.TeacherCredit = int.Parse(reader["TeacherCredit"].ToString());
                teacher.Address = reader["Address"].ToString();
                teacher.TeacherContactInfo = reader["TeacherContactInfo"].ToString();
                reader.Close();
                teachers.Add(teacher);
            }
            connection.Close();
            return teachers;


        }


      
    }
}