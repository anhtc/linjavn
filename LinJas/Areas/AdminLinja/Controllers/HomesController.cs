using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinJas.Areas.AdminLinja.Controllers
{
    public class HomesController : Controller
    {
        // GET: AdminLinja/Homes
        public ActionResult Index()
        {
            return View();
        }
    }
}