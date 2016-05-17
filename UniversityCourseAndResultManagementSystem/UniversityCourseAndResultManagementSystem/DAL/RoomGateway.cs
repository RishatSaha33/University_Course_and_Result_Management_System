using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.DAL
{
    public class RoomGateway
    { 
        private string connectionString = WebConfigurationManager.ConnectionStrings["ProjectdbContext"].ConnectionString;

        private SqlConnection connection;

        public RoomGateway()
        {
            connection = new SqlConnection(connectionString);
        }
        public List<RoomSceduleInformation> GetScheduleInformation(int departmentId)
        {
         List<RoomSceduleInformation> roomAllocationInfoList=new List<RoomSceduleInformation>();
         string query = "SELECT * FROM RoomScheduleView WHERE DepartmentId='" + departmentId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                RoomSceduleInformation roomAllocationInfo = new RoomSceduleInformation()
                {
                    CourseCode = reader["CourseCode"].ToString(),
                    CourseName = reader["CourseName"].ToString(),
                    Day = reader["DayId"].ToString() != ""? reader["DayId"].ToString().Substring(0, 3): reader["DayId"].ToString(),
                    RoomName = reader["RoomName"].ToString(),
                    StartTime = reader["StartTime"].ToString(),
                    EndTime = reader["EndTime"].ToString()
                };
                roomAllocationInfoList.Add(roomAllocationInfo);
            }
            connection.Close();
            return roomAllocationInfoList;
        
        }

        public bool UnallocateRooms()
        {
           // string query = "DELETE FROM RoomAllocations";
            string query = "Update RoomAllocations SET RoomId = null,DayId=null,StartTime=null,EndTime=null";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            bool isUnallocated = rowsAffected > 0;
            return isUnallocated;
        }
    }

}