using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Models;
using UniversityCourseAndResultManagementSystem.Context;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class EnrollStudentController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

       
        public ActionResult StudentEnroll()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNo");
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentEnroll(int? studentId,int? courseId,DateTime? registrationDate)
        {
            if (studentId!= null && courseId != null && registrationDate!= null)
            {
                var students = db.Database.SqlQuery<int>(
                "SELECT Student_Id FROM dbo.StudentCourses Where Student_Id =" + studentId + " AND Course_Id = " + courseId ).ToList();
            var counts = students.Count;
            if (counts==0)
              {
               if (ModelState.IsValid)
                 {
                     db.Database.ExecuteSqlCommand(
                             "INSERT INTO dbo.StudentCourses VALUES('" + studentId + "','" + courseId + "','" + registrationDate + "','"+null+"')");
                //db.Students.Add(student);
                //db.SaveChanges();
                ViewBag.Message = "Saved Successfully";
                //return RedirectToAction("Index");
                 }  
              }
            else
              {
                ViewBag.ErrorMessage = "Student already enrolled";  
              } 
            }
            else
            {
                ViewBag.ErrorMessage = "Please fill up all the required fields ";     
            }
           
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNo");
            return View();
        }
  
        public JsonResult GetNameEmailDeptByRegNo(int studentId)
        {
            var students = db.Students.ToList();
            var studentList = students.Where(a => a.Id ==studentId).ToList();
            return Json(studentList.Select(x => new
            {
            name=x.StudentName,
            email=x.StudentEmail,
            //x.Departmrnt.DeptName
            department=db.Departments.Where(c=>c.Id==x.DepartmentId).Select(c=>c.DeptName).Single()

            }),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCoursesByStudentId(int studentId)
        {
            var student = db.Students.Find(studentId);
            var courses = db.Courses.Where(c => c.DepartmentId == student.DepartmentId);
            return Json(new SelectList(courses, "Id", "CourseName"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}
