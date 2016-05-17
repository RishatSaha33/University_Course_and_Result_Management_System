using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Context;
using UniversityCourseAndResultManagementSystem.DAL;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.BLL
{
    public class RoomManager
    {  
        RoomGateway roomGteway=new RoomGateway();
        ProjectDbContext db=new ProjectDbContext();
        public  List<RoomSceduleInformation> GetScheduleInformation(int departmentId)
        {
       
            return roomGteway.GetScheduleInformation(departmentId);
        }

        public Tuple<bool,string> Overlaping(int roomId, int dayId, string startTime, string endTime)
        {
            List<RoomAllocation> classrooms = db.RoomAllocations.Where(c => c.RoomId == roomId && c.DayId == dayId).ToList();
            bool isConflict = false;
            string startTimeOfOverlap = "";
            string endTimeOfOverlap = "";
            string notificationMessage = "";
            string newStartMeridian = startTime.Substring(startTime.Length - 2);
            string newEndMeridian = endTime.Substring(endTime.Length - 2);
            string newStartTime = startTime.Substring(0, startTime.Length - 2);
            string newEndTime = endTime.Substring(0, endTime.Length - 2);

            if (newStartMeridian != newEndMeridian)
            {
                foreach (var classroom in classrooms)
                {
                    string oldStartMeridian = classroom.StartTime.Substring(classroom.StartTime.Length - 2);
                    string oldEndMeridian = classroom.EndTime.Substring(classroom.EndTime.Length - 2);


                    string oldStartTime = classroom.StartTime.Substring(0, classroom.StartTime.Length - 2);
                    string oldEndTime = classroom.EndTime.Substring(0, classroom.EndTime.Length - 2);


                    if (oldEndMeridian != newStartMeridian && newEndMeridian != oldStartMeridian)
                    {
                        if (oldStartMeridian == oldEndMeridian && newStartMeridian == newEndMeridian)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry! there is already a schedule For this romm from" +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day.Choose another schedule please.";
                            break;
                        }
                    }
                    else if (newEndMeridian != oldStartMeridian && oldEndMeridian == newStartMeridian)
                    {
                        if (oldEndTime.CompareTo(newStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day.Choose another schedule please.";
                            break;
                        }
                    }
                    else if (oldEndMeridian != newStartMeridian && newEndMeridian == oldStartMeridian)
                    {
                        if (newEndTime.CompareTo(oldStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. Choose another schedule please.";
                            break;
                        }
                    }
                    else
                    {
                        if (classroom.EndTime.CompareTo(startTime) <= 0 ||
                            endTime.CompareTo(classroom.StartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry! there is already a schedule for this room " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day.Choose another schedule please.";
                            break;
                        }
                    }
                }
            }
            else if (newStartTime.CompareTo(newEndTime) == 0)
            {
                isConflict = true;
                notificationMessage = "Check the schedule entry again," + startTime + " to " + endTime +
                                      " is not a valid schedule.";
            }
            else if (newStartTime.CompareTo(newEndTime) < 1)
            {
                foreach (var classroom in classrooms)
                {
                    string oldStartMeridian = classroom.StartTime.Substring(classroom.StartTime.Length - 2);
                    string oldEndMeridian = classroom.EndTime.Substring(classroom.EndTime.Length - 2);


                    string oldStartTime = classroom.StartTime.Substring(0, classroom.StartTime.Length - 2);
                    string oldEndTime = classroom.EndTime.Substring(0, classroom.EndTime.Length - 2);


                    if (oldEndMeridian != newStartMeridian && newEndMeridian != oldStartMeridian)
                    {
                        if (oldStartMeridian == oldEndMeridian && newStartMeridian == newEndMeridian)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry! there is already a schedule for this room from " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. Choose another schedule please.";
                            break;
                        }
                    }
                    else if (newEndMeridian != oldStartMeridian && oldEndMeridian == newStartMeridian)
                    {
                        if (oldEndTime.CompareTo(newStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry! there is already an schedule for this room from " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day.  Choose another schedule please.";
                            break;
                        }
                    }
                    else if (oldEndMeridian != newStartMeridian && newEndMeridian == oldStartMeridian)
                    {
                        if (newEndTime.CompareTo(oldStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry! there is already a schedule for this room from " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. Choose anotherschedule please.";
                            break;
                        }
                    }
                    else
                    {
                        if (classroom.EndTime.CompareTo(startTime) <= 0 ||
                            endTime.CompareTo(classroom.StartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.StartTime;
                            endTimeOfOverlap = classroom.EndTime;
                            notificationMessage = "Sorry! there is already a schedule for this room from " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. Choose another schedule please.";
                            break;
                        }
                    }
                }
            }
            else
            {
                isConflict = true;
                notificationMessage = "Check the schedule entry again," + startTime + " to " + endTime +
                                      " is not a valid schedule.";
            }

            return Tuple.Create<bool, string>(isConflict, notificationMessage);
        }

        public bool UnallocateRooms()
        {
            return roomGteway.UnallocateRooms();
        }
    }
}