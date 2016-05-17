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
    public class TeachersController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: /Teachers/
        public ActionResult ViewTeacher()
        {
            var teachers = db.Teachers.Include(t => t.Departmrnt).Include(t => t.Designation);
            return View(teachers.ToList());
        }
        public ActionResult SaveTeacher()
        {
            ViewBag.DepartmentId = db.Departments.ToList();
            ViewBag.DesignationId = db.Designations.ToList();
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            //ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTeacher([Bind(Include = "Id,Name,Address,TeacherEmail,TeacherContactInfo,TeacherCredit,DesignationId,DepartmentId")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RemainingCredit = teacher.TeacherCredit;
                ViewBag.Message = "Teacher Informtion saved successfully";
                db.Teachers.Add(teacher);
                db.SaveChanges();
                //return RedirectToAction("ViewTeacher");
            }
            ViewBag.DepartmentId = db.Departments.ToList();
            ViewBag.DesignationId = db.Designations.ToList();
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", teacher.DepartmentId);
            //ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult TeacherEmaiexists(string teacherEmail)
        {
            var aTeacher = db.Teachers.FirstOrDefault(x => x.TeacherEmail == teacherEmail);
            if (aTeacher == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
