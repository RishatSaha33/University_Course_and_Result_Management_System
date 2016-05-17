using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class HomePageController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ContactUs()
        {
            return View();
        }
	}
}