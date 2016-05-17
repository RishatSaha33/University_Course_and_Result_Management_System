using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.BLL;
using UniversityCourseAndResultManagementSystem.Models;
using UniversityCourseAndResultManagementSystem.Context;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        CourseManager courseManager=new CourseManager();

        // GET: /Courses/
        //public ActionResult ViewCourse()
        //{
        //    var courses = db.Courses.Include(c => c.Departmrnt).Include(c => c.Semister);
        //    return View(courses.ToList());
        //}

       
     
        public ActionResult SaveCourse()
        {
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.SemisterId = db.Semisters.ToList();
            
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourse([Bind(Include="Id,CourseCode,CourseName,CourseCredit,CourseDescription,DepartmentId,SemisterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Course Entry Saved Successfully";
                db.Courses.Add(course);
                db.SaveChanges();

            }

            ViewBag.Departments = db.Departments.ToList();
            ViewBag.SemisterId = db.Semisters.ToList();
            
            return View(course);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult CourseCodeExists(string courseCode)
        {
            var aCourse = db.Courses.FirstOrDefault(x => x.CourseCode == courseCode);
            if (aCourse == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult CourseNameExists(string courseName)
        {
            var aCourse = db.Courses.FirstOrDefault(m => m.CourseName == courseName);
            if (aCourse == null)
            {
                return Json(true,JsonRequestBehavior.AllowGet );
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AssignCourse()
        {
            ViewBag.DepartmentId=new SelectList(db.Departments,"Id","DeptName");
            return View();
        }
        [HttpPost]
        public ActionResult AssignCourse(int? departmentId,int? teacherId,int? courseId)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptName");
            if (departmentId != null && teacherId != null && courseId != null)
            {
                Course getCoursesById = db.Courses.SingleOrDefault(m => m.Id == courseId);
                Teacher getTeachersById = db.Teachers.SingleOrDefault(m => m.Id == teacherId);
                var credit = getCoursesById.CourseCredit;
                
                if (getCoursesById.TeacherId == null)
                {
                    getCoursesById.TeacherId = teacherId;
                    getTeachersById.RemainingCredit = getTeachersById.RemainingCredit - credit;
                    if (ModelState.IsValid)
                    {
                        db.Entry(getCoursesById).State=EntityState.Modified;
                        db.Entry(getTeachersById).State=EntityState.Modified;
                        try
                        {
                            db.SaveChanges(); 
                            
                        }
                        catch (DbEntityValidationException ex) { }
                    }
                    ViewBag.SuccessMessage = "Course assigned successfully";
                }
                else
                {
                    ViewBag.FailMessage = "Course already assigned! ";
                }

            }
           
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            return View();
        }
        public JsonResult CheckIfCourseIsAlreadyAssigned(int courseId)
        {
            string message = "";
            Course getCourseById = db.Courses.Single(x => x.Id == courseId);
            if (getCourseById.TeacherId != null)
            {
                message = "This course has already been assigned to a teacher!";
            }
            else
            {
                message = "";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {
            var teachers = db.Teachers.ToList();
            var teacherList = teachers.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(new SelectList(teacherList, "Id", "Name"));
        }
        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            var courses = db.Courses.ToList();
            var courseList = courses.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(new SelectList(courseList, "Id", "CourseCode"));
        }

        public JsonResult GetCreditsByTeacherId(int teacherId)
        {
            var teachers = db.Teachers.ToList();
            var teacherList = teachers.Where(a => a.Id == teacherId).ToList();
            return Json(new SelectList(teacherList, "TeacherCredit", "RemainingCredit"));
        }
        public JsonResult GetNameAndCreditByCourseId(int courseId)
        {
            var courses = db.Courses.ToList();
            var courseList = courses.Where(a => a.Id == courseId).ToList();
            return Json(new SelectList(courseList, "CourseName", "CourseCredit"));
        }

        public ActionResult CoursesInformation()
        {

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            return View();
        }

        [HttpPost]
        public ActionResult CoursesInformation(int departmentId)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            return View();
        }

        public JsonResult GetCoursesInformationByDepartmentId(int departmentId)
        {
            var courses = db.Courses.ToList();
            var courseList = courses.Where(a => a.DepartmentId == departmentId).ToList();

            return Json(courseList.Select(x => new
            {
                code = x.CourseCode,
                name = x.CourseName,
                semester = x.Semister.SemisterName,
                teacher = x.TeacherId == null ? "Not assigned Yet" : x.Teacher.Name


            }));
        }
        
        public ActionResult UnAssignCourse()
        {
            return View();
        }
        [HttpPost]
        [ActionName("UnassignCourse")]
        public ActionResult UnAssignCourses()
        {
            if (courseManager.UnassignCourses())
            {
                ViewBag.Message = "Course unassigned successfully";
            }
            else
            {
                ViewBag.Message = "Course already unassigned ";
            }
            return View();
        }
    }
}
