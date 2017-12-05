using Kendo.Mvc.UI;
using LinJas.Areas.AdminLinja.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using LinJas.Areas.AdminLinja.Common;
using Kendo.Mvc.Extensions;

namespace LinJas.Areas.AdminLinja.Controllers
{
    public class RolesController : Controller
    {
        // khởi tạo db
        private readonly AspNetRoleModel _db;
        public RolesController() {
            _db = new AspNetRoleModel();
        }
        // clear bộ nhớ
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
        // GET: AdminLinja/Roles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadData([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<AspController>(TVConstants.StoredProcedure.AdminRole.GetAllRolesController).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetController([DataSourceRequest] DataSourceRequest request)
        {
            string namespaces = "LinJas.Areas.AdminLinja.Controllers";
            // đọc các controller và action
            Assembly asm = Assembly.GetExecutingAssembly();
            var controllerActionList = asm.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new ControllerViewModel { Controller = x.DeclaringType.Name.Replace("Controller", ""), Action = x.Name, Attributes = string.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            //Thêm mới vào db
            var result = 0;
            var text = "Thêm thành công";

            foreach (var item in controllerActionList)
            {
                AspController flag = _db.AspControllers.Find(item.Controller, item.Action);
                if (flag == null)
                {
                    result =
                        _db.Database.ExecuteSqlCommand(
                        TVConstants.StoredProcedure.AdminRole.AddController, item.Controller, item.Action, "", "", 0);
                    if (result == 0) text = "Thêm Thất bại";
                }
            }
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddController(Guid? id)
        {
            if (id == null)
            {
                id = new Guid("00000000-0000-0000-0000-000000000000");
            }
            var role = _db.AspNetRoles.SingleOrDefault(s => s.Id == id);
            if (role != null)
            {
                var controllerSelected = _db.AspRoleControllers.Where(s => s.RoleId == id).ToList();
                var controller = _db.AspControllers.ToList();
                ControllerRoleViewModel model = new ControllerRoleViewModel { Roles = role, ControllerSelecteds = controllerSelected, Controllers = controller };
                return View(model);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult AddController(Guid userId, params string[] selectedController)
        {
            var ctx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_db).ObjectContext;
            ctx.ExecuteStoreCommand("DELETE FROM [AsbRoleController] WHERE RoleId={0}", userId);

            foreach (var item in selectedController)
            {
                string controller = item.Split('-')[0];
                string action = item.Split('-')[1];

                _db.AspRoleControllers.Add(new AspRoleController { RoleId = userId, Controller = controller, Action = action });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Thêm quyền
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>        
        [ValidateInput(false)]
        public ActionResult CreateNewRoleId(string roleName)
        {
            var result = 0;           
            result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.CreateRoleId, roleName);
            var text = "Thêm mới thành công";
            if (result < 1) text = "Thêm mới Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllRole([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<AspNetRole>(TVConstants.StoredProcedure.AdminRole.GetAllRoles).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUpdateRole(Guid id)
        {
            var model = _db.Database.SqlQuery<AspNetRole>(TVConstants.StoredProcedure.AdminRole.GetRoleById, id).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public ActionResult UpdateRole(Guid id, string roleName)
        {
            var result = 0;
            
            result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.UpdateRoles,id, roleName);
            var text = "Chỉnh sửa thành công";
            if (result < 1) text = "Chỉnh sửa Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteRole(Guid Id)
        {
            var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.DeleteRoleById, Id);
            var text = "Đã xóa thành công";
            if (result < 1) text = "Xóa thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
    }
}