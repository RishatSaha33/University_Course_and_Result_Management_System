using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityCourseAndResultManagementSystem.DAL
{
    public class CourseGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["Projectdbcontext"].ConnectionString;
        private SqlConnection connection = null;
        public CourseGateway()
        {
            connection=new SqlConnection(connectionString);
        }
        public bool UnassignCourses()
        {
            string query = "UPDATE Courses SET TeacherId=NULL";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int noOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();
            bool isUnassigned = noOfRowsAffected> 0;
            return isUnassigned;
           
        }
    }
}