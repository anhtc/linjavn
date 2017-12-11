using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinJas.Common;
using LinJas.Manager;

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
        public ActionResult Index(string UserName, string PasswordHash)
        {
            var _password = Utilities.GetMd5Hash(PasswordHash);
            var loginModel = AcountManager.Instance.Login(UserName, _password);
            if (loginModel.Count > 0)
            {
                return RedirectToAction("Index","Homes",new { area = "AdminLinja" });
            }
            else
            {
                ModelState.AddModelError("PROGRAM_ID", "Tài khoản  hoặc Mật khẩu chưa đúng?");
                return View();
            }
               
        }
    }
}