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
using System.Text.RegularExpressions;

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
        public ActionResult AddController([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var controller = _db.Database.SqlQuery<NhomQuyenModel>(TVConstants.StoredProcedure.AdminRole.GetAllControllerByRole).ToList();

                return Json(controller.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetAllActionByController([DataSourceRequest] DataSourceRequest request, string _controller)
        {
            try
            {
                var listItems = _db.Database.SqlQuery<CheckQuyenModel>(TVConstants.StoredProcedure.AdminRole.GetAllActionByController, _controller).ToList();

                return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult UpdateQuyen(Guid userId, string controller, string quyenIds, string removelQuyenIds)
        {
            try
            {
                var text = "";
                var result = 0;
               
                for (int i = 0; i < quyenIds.Length; i++)
                {
                    string[] arrQuyen = Regex.Split(quyenIds, ",");
                    string[] removeArrQuyen = Regex.Split(removelQuyenIds, ",");

                    result += _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.UpdatePhanquyenUserById, userId, controller, arrQuyen[i], removeArrQuyen[i]);
                }              

                if (result > 0)
                {
                    text = "Cập nhật quyền thành công.";
                }
                else
                {

                    text = "Cập nhật quyền thất bại.";
                }

                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Num = 0, Message = "Cập nhật quyền thất bại." }, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpPost]
        //public ActionResult AddController(Guid userId, params string[] selectedController)
        //{
        //    var ctx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_db).ObjectContext;
        //    ctx.ExecuteStoreCommand("DELETE FROM [AsbRoleController] WHERE RoleId={0}", userId);

        //    foreach (var item in selectedController)
        //    {
        //        string controller = item.Split('-')[0];
        //        string action = item.Split('-')[1];

        //        _db.AspRoleControllers.Add(new AspRoleController { RoleId = userId, Controller = controller, Action = action });
        //    }
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
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
        /// <summary>
        /// LẤy người dùng để phân quyền
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult LoadDataNguoiDung([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<NguoiDungModel>(TVConstants.StoredProcedure.AdminRole.GetAllUser).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}