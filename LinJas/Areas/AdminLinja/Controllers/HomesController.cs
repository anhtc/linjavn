using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinJas.Models;

namespace LinJas.Areas.AdminLinja.Controllers
{
    [Auth]
    public class HomesController : Controller
    {
        // GET: AdminLinja/Homes
        public ActionResult Index()
        {
            return View();
        }
    }
}