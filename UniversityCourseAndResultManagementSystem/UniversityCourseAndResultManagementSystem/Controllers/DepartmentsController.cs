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
    public class DepartmentsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        public ActionResult ViewDepartment()
        {
            return View(db.Departments.ToList());
        }

       
        public ActionResult SaveDepartment()
        {
            
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDepartment([Bind(Include = "Id,DeptCode,DeptName")] Departmrnt departmrnt)
        {

            if (ModelState.IsValid)
            {
                ViewBag.message = "Saved success";
                db.Departments.Add(departmrnt);
                db.SaveChanges();
                ModelState.Clear();
                return View();
                //return RedirectToAction("Index");

            }
           
            return View(departmrnt);
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult DepartmentCodeExits(string deptcode)
        {
            var aDepartment = db.Departments.FirstOrDefault(x => x.DeptCode == deptcode);
            if (aDepartment != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DepartmentNameExits(string deptname)
        {
            var aDepartment = db.Departments.FirstOrDefault(x => x.DeptName == deptname);
            if (aDepartment != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

       
    }
}
