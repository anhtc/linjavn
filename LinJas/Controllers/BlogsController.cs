using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinJas.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
    }
}