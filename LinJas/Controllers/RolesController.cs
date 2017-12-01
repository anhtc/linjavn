using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using LinJas.Models;

namespace LinJas.Controllers
{
    public class RolesController : Controller
    {
        AsbNetModel db = new AsbNetModel();
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetController()
        {
            string namespaces = "LinJas.Areas.AdminLinja.Controllers";
            // đọc các controller và action
            Assembly asm =  Assembly.GetExecutingAssembly();
            var controllerActionList = asm.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new ControllerModel { Controller = x.DeclaringType.Name.Replace("Controller", ""), Action = x.Name, Attributes = string.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            //Thêm mới vào db
            foreach (var item in controllerActionList)
            {
                try
                {
                    AsbController itemAdd = new AsbController { Controller = item.Controller, Action = item.Action };
                    db.AsbControllers.Add(itemAdd);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View(db.AsbControllers.ToList());
        }
        public ActionResult AddController(Guid? id)
        {
            if (id==null)
            {
                id = new Guid("00000000-0000-0000-0000-000000000000");
            }
            var role = db.AsbNetRoles.SingleOrDefault(s=>s.Id==id);
            if (role!=null)
            {
                var controllerSelected = db.AsbRoleControllers.Where(s=>s.RoleId==id).ToList();
                var controller = db.AsbControllers.ToList();
                ControllerRoleModel model = new ControllerRoleModel { Roles = role, ControllerSelecteds= controllerSelected, Controllers= controller };
                return View(model);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult AddController(Guid userId, params string[] selectedController)
        {
            var ctx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            ctx.ExecuteStoreCommand("DELETE FROM [AsbRoleController] WHERE RoleId={0}", userId);

            foreach (var item in selectedController)
            {
                string controller = item.Split('-')[0];
                string action = item.Split('-')[1];
                
                db.AsbRoleControllers.Add(new AsbRoleController { RoleId = userId, Controller = controller, Action = action });
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}