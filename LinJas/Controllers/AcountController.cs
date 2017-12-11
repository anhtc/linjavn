using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinJas.Controllers
{
    public class AcountController : Controller
    {
        // GET: Acount
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string PasswordHash)
        {


            return View();
        }
    }
}