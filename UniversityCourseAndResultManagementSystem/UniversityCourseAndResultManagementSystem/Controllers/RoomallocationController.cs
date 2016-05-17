using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.BLL;
using UniversityCourseAndResultManagementSystem.Models;
using UniversityCourseAndResultManagementSystem.Context;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class RoomallocationController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        RoomManager roomManager=new RoomManager();
        // GET: /Roomallocation/
        public ActionResult Index()
        {
            var roomallocations = db.RoomAllocations.Include(r => r.Course).Include(r => r.Day).Include(r => r.Departmrnt).Include(r => r.Room);
            return View(roomallocations.ToList());
        }

        // GET: /Roomallocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            return View(roomallocation);
        }

        // GET: /Roomallocation/Create
        public ActionResult AllocateRoom()
        {
            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode");
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNo");
            return View();
        }

        // POST: /Roomallocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllocateRoom([Bind(Include = "Id,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime")] RoomAllocation roomallocation)
        {
            if (!roomManager.Overlaping(roomallocation.RoomId, roomallocation.DayId, roomallocation.StartTime,
                roomallocation.EndTime).Item1)
            {
                 if (ModelState.IsValid)
               {
                db.RoomAllocations.Add(roomallocation);
                db.SaveChanges();
                 ViewBag.SuccessMessage = "New Schedule Saved Successfully!";
                //return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.FailMessage = roomManager.Overlaping(roomallocation.RoomId, roomallocation.DayId,roomallocation.StartTime,
                 roomallocation.EndTime).Item2;  
            }

            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNo", roomallocation.RoomId);
            return View();
        }

        // GET: /Roomallocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", roomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNo", roomallocation.RoomId);
            return View(roomallocation);
        }

        // POST: /Roomallocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime")] RoomAllocation roomallocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomallocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", roomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNo", roomallocation.RoomId);
            return View(roomallocation);
        }

        // GET: /Roomallocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            return View(roomallocation);
        }

        // POST: /Roomallocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            db.RoomAllocations.Remove(roomallocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetRoomInformation()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        public JsonResult GetCoursesByDepartmentId(int? departmentId)
        {
            var courseList = db.Courses.Where(m => m.DepartmentId == departmentId).ToList();
            return Json(new SelectList(courseList,"Id","CourseName"));

        }

        public JsonResult GetroomallocationInfoList(int departmentId)
        {
            var scheduleInfoList = roomManager.GetScheduleInformation(departmentId);
            return Json(scheduleInfoList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Unallocate()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Unallocate")]
        public ActionResult UnallocateRooms()
        {
            if (roomManager.UnallocateRooms())
            {
                ViewBag.SuccessMessage = "All rooms unallocated Successfully!";
            }
            else
            {
                ViewBag.SuccessMessage = "All rooms are already unallocated!";
            }
            return View();
        }

    }
}
