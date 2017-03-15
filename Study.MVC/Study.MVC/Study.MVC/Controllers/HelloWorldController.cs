using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study.MVC.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult Index()
        {
            return View();
        }
        public string Welcome( string name ,int  num)
        {
            return string.Format("{0}第{1}访问固定的Action",name,num);
        }
        public ActionResult List()
        {
            return View();
        }
    }
}