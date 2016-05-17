using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using Rotativa;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Models;
using UniversityCourseAndResultManagementSystem.Context;
using UniversityCourseAndResultManagementSystem.Migrations;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class StudentResultController : Controller
    {
       
        private ProjectDbContext db = new ProjectDbContext();

        public ActionResult SaveResult()
        {
           
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNo");
            ViewBag.GradeId = new SelectList(db.StudentGrades, "Id", "Grade");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveResult(int? studentId, int? courseId, int? gradeId)
        {
            if (studentId != null && courseId != null && gradeId != null)
            {
                var results = db.Database.SqlQuery<int>(
                    "SELECT Student_Id FROM dbo.StudentCourses Where Student_Id =" + studentId + " AND Course_Id = " +
                    courseId).ToList();
                var counts = results.Count;
                if (counts == 1)
                {
                    if (ModelState.IsValid)
                    {

                        db.Database.ExecuteSqlCommand(
                            "UPDATE dbo.StudentCourses SET GradeId = '" + gradeId + "' WHERE Student_Id = '" + studentId +
                            "' AND Course_Id = '" + courseId + "'");

                        ViewBag.Message = "Result Saved Successfully";
                    }
                }
                else
                {
                    ViewBag.FailMessage = "Please enroll the course first";
                }
            }
            else
            {
                ViewBag.FailMessage = "Please fill up all the required fields ";
            }
           
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNo");
            ViewBag.GradeId = new SelectList(db.StudentGrades, "Id", "Grade");
            return View();
        }

        public JsonResult GetEnrolledCoursesByStudentId(int studentId)
        {
            var courseIds = db.Database.SqlQuery<int>(
                "SELECT Course_Id FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
            var courseList = new List<Course>();

            foreach (var courseId in courseIds)
            {
                var course = db.Courses.SingleOrDefault(c => c.Id == courseId);
                courseList.Add(course);
            }
            var courses = courseList.AsQueryable();
            return Json(new SelectList(courses, "Id", "CourseName"));

        }
        public ActionResult ViewStudentResult()
        {
           
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNo");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ViewStudentResult(int? studentId)
        {
            var courseIds = db.Database.SqlQuery<int>(
                "SELECT Course_Id FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
            var gradeIds = db.Database.SqlQuery<int?>(
                "SELECT GradeId FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
            List<StudentResult> cList = new List<StudentResult>();

            var count = courseIds.Count;

            for (int i = 0; i < count; i++)
            {
                var value = gradeIds[i];
                var values = courseIds[i];
                var course = db.Courses.Single(c => c.Id == values);
                var grade = db.StudentGrades.SingleOrDefault(g => g.Id == value);
                StudentResult cGrade = new StudentResult();
                cGrade.CourseId = values;
                if (grade == null)
                {
                    cGrade.GradeId = 0;
                    cGrade.GradeName = "Not Graded Yet";
                }
                else
                {
                    cGrade.GradeId = grade.Id;
                    cGrade.GradeName = grade.Grade;
                }

                cGrade.CourseCode = course.CourseCode;
                cGrade.CourseName = course.CourseName;

                cList.Add(cGrade);

            }
            
            var students = db.Students.ToList();
            var studentList = students.FirstOrDefault(a => a.Id == studentId);
            string viewResult = "Student Result ==>>\nStudent Name : "+studentList.StudentName+"\nReg No : "+studentList.RegistrationNo+"\n Email : "+studentList.StudentEmail+"\nDepartment : "+studentList.Departmrnt.DeptName+"\n";
            foreach (StudentResult result in cList)
            {
                viewResult+="\nCourse Code:"+result.CourseCode +"\nCourse Name: "+ result.CourseName +"\n Grade: "+ result.GradeName+"\n";
            }

            viewResult += DateTime.Now.Date.ToString();
            Document document = new Document();
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(viewResult);
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            string filename = "Report.pdf";
            pdfRenderer.PdfDocument.Save(HostingEnvironment.ApplicationPhysicalPath + "/pdf/" + filename);
            Response.Redirect("~/pdf/Report.pdf");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNo");
            //return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult GetEnrolledCoursesNameAndGradeByStudentId(int studentId)
        {
            var courseIds = db.Database.SqlQuery<int>(
                "SELECT Course_Id FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
            var gradeIds = db.Database.SqlQuery<int?>(
                "SELECT GradeId FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
            List<StudentResult> cList = new List<StudentResult>();

            var count = courseIds.Count;

            for (int i = 0; i < count; i++)
            {
                var value = gradeIds[i];
                var values = courseIds[i];
                var course = db.Courses.Single(c => c.Id == values);
                var grade = db.StudentGrades.SingleOrDefault(g => g.Id == value);
                StudentResult cGrade = new StudentResult();
                cGrade.CourseId = values;
                if (grade == null)
                {
                    cGrade.GradeId = 0;
                    cGrade.GradeName = "Not Graded Yet";
                }
                else
                {
                    cGrade.GradeId = grade.Id;
                    cGrade.GradeName = grade.Grade;
                }

                cGrade.CourseCode = course.CourseCode;
                cGrade.CourseName = course.CourseName;

                cList.Add(cGrade);

            }


            return Json(cList, JsonRequestBehavior.AllowGet);
        }

        
       
    }
}