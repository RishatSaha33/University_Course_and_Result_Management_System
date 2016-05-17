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
    public class StudentsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: /Students/
        public ActionResult ViewStudent()
        {
            var students = db.Students.Include(s => s.Departmrnt);
            return View(students.ToList());
        }

        // GET: /Students/Details/5
       

        // GET: /Students/Create
        public ActionResult RegisterStudent()
        {
            ViewBag.Departments = db.Departments.ToList();
            //ViewBag.Departments = new SelectList(db.Departments, "Id", "DeptName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStudent([Bind(Include="Id,StudentName,StudentEmail,StudentContactNo,RegistrationDate,StudentAddress,RegistrationNo,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.RegistrationNo = GetStudentRegNo(student);
                db.Students.Add(student);
                db.SaveChanges();
                ViewBag.Message = "Student registration saved successfully";
                //return RedirectToAction("ViewStudent");
            }
            ViewBag.Departments = db.Departments.ToList();
            //ViewBag.Departments = new SelectList(db.Departments, "Id", "DeptName");
            return View(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetStudentRegNo(Student aStudent)
        {

            Departmrnt aDepartment = db.Departments.FirstOrDefault(aDept => aDept.Id == aStudent.DepartmentId);
            int countDepartmentStudentd =db.Students.Count(aStd => (aStd.DepartmentId == aStudent.DepartmentId) && (aStd.RegistrationDate.Year == aStudent.RegistrationDate.Year)) + 1;
            int noOfZeroAdded = 3 - countDepartmentStudentd.ToString().Length;
            string noOfZero = "";
            for (int i = 0; i < noOfZeroAdded; i++)
            {
                noOfZero += "0";
            }

            return aDepartment.DeptCode + "-" + aStudent.RegistrationDate.Year + "-" + noOfZero + countDepartmentStudentd;

        }
        public JsonResult StudentEmailExits(string studentemail)
        {
            var aStudent = db.Students.FirstOrDefault(m => m.StudentEmail == studentemail);
            if (aStudent == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
