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
        /// <summary>
        /// Hiển thị danh sách các  quyền từ hệ thống
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult LoadData([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<AspController>(TVConstants.StoredProcedure.AdminRole.GetAllRolesController).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Thêm mới danh sách quyền từ hệ thống
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Lấy danh sách nhóm quyền Controller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Lấy action theo controller
        /// </summary>
        /// <param name="request"></param>
        /// <param name="_userId"></param>
        /// <param name="_controller"></param>
        /// <returns></returns>
        public ActionResult GetAllActionByController([DataSourceRequest] DataSourceRequest request,Guid? _userId, string _controller)
        {
            try
            {
                var listItems = _db.Database.SqlQuery<CheckQuyenModel>(TVConstants.StoredProcedure.AdminRole.GetAllActionByController,_userId, _controller).ToList();

                return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Cập nhật quyền  cho người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="controller"></param>
        /// <param name="quyenIds"></param>
        /// <param name="mota"></param>
        /// <param name="removelQuyenIds"></param>
        /// <returns></returns>
        public ActionResult UpdateQuyen(Guid userId, string controller, string quyenIds,string mota, string removelQuyenIds)
        {
            try
            {
                var text = "";
                var result = 0;
                string[] arrQuyen = Regex.Split(quyenIds, ",");
                string[] removeArrQuyen = Regex.Split(removelQuyenIds, ",");

                if (removeArrQuyen[0].ToString() != "" && !string.IsNullOrEmpty(removeArrQuyen[0]))
                {
                    for (int i = 0; i < removeArrQuyen.Length.GetHashCode(); i++)
                    {
                        AddQuyenModel model = new AddQuyenModel { RoleId = userId, Controller = controller, Action = removeArrQuyen[i].ToString(), Description = mota};
                        result += _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.DeletePhanquyenUserById, model.RoleId, model.Controller, model.Action, model.Description);
                    }                   
                }
                if (arrQuyen[0].ToString() != "" && !string.IsNullOrEmpty(arrQuyen[0]))
                {
                    for (int i = 0; i < arrQuyen.Length.GetHashCode(); i++)
                    {
                        AddQuyenModel model = new AddQuyenModel { RoleId = userId, Controller = controller, Action = arrQuyen[i].ToString(), Description = mota};
                        result += _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.UpdatePhanquyenUserById, model.RoleId, model.Controller, model.Action, model.Description);
                    }
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
                return Json(new { Num = 0, Message = "Quyền chưa được cập nhật." }, JsonRequestBehavior.AllowGet);
            }
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
        /// <summary>
        /// hiển thị quyền 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetAllRole([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<AspNetRole>(TVConstants.StoredProcedure.AdminRole.GetAllRoles).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Hiển thị cho Update mo tả  quyền
        /// </summary>
        /// <param name="_controller"></param>
        /// <param name="_action"></param>
        /// <returns></returns>
        public ActionResult GetUpdateMotaQuyen(string _controller, string _action)
        {
            var model = _db.Database.SqlQuery<AspController>(TVConstants.StoredProcedure.AdminRole.GetMotaQuyenByAction, _controller,_action).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateMotaQuyen(string _controllers, string _actions, string _descriptions)
        {
            var result = 0;

            result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.UpdateMotaQuyen, _controllers, _actions, _descriptions, "AdminLinja");
            var text = "Chỉnh sửa thành công";
            if (result < 1) text = "Chỉnh sửa Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Hiển thị cho update roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetUpdateRole(Guid id)
        {
            var model = _db.Database.SqlQuery<AspNetRole>(TVConstants.StoredProcedure.AdminRole.GetRoleById, id).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Sửa quyền admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UpdateRole(Guid id, string roleName)
        {
            var result = 0;
            
            result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.UpdateRoles,id, roleName);
            var text = "Chỉnh sửa thành công";
            if (result < 1) text = "Chỉnh sửa Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Xóa quyền dành cho admin
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Lấy người dùng để sửa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetUpdateUserById(Guid id)
        {
            var model = _db.Database.SqlQuery<AspNetUser>(TVConstants.StoredProcedure.AdminRole.GetUpdateUserById, id).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DeleteUserById(Guid Id)
        {
            var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.DeleteUserById, Id);
            var text = "Đã xóa thành công";
            if (result < 1) text = "Xóa thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Sửa người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        /// <param name="_phone"></param>
        /// <param name="_userName"></param>
        /// <param name="_avatar"></param>
        /// <param name="_active"></param>
        /// <param name="_roleId"></param>
        /// <param name="_hoten"></param>
        /// <returns></returns>
        public ActionResult UpdateUser(Guid id, string _email, string _password, string _phone, string _userName
            , string _avatar
            , bool _active
            , Guid _roleId
            , string _hoten)
        {
            var result = 0;

            result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.UpdateUser
                , id
                , _email
                , _password
                , _phone
                , _userName
                , _avatar
                , _active
                , _roleId
                , _hoten);

            var text = "Chỉnh sửa thành công";
            if (result < 1) text = "Chỉnh sửa Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Thêm mới người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        /// <param name="_phone"></param>
        /// <param name="_userName"></param>
        /// <param name="_avatar"></param>
        /// <param name="_active"></param>
        /// <param name="_roleId"></param>
        /// <param name="_hoten"></param>
        /// <returns></returns>
        public ActionResult InsertUser(string _email, string _password, string _phone, string _userName
            , string _avatar
            , bool _active
            , Guid _roleId
            , string _hoten)
        {
            var result = 0;

            result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminRole.InsertUser
                , _email
                , _password
                , _phone
                , _userName
                , _avatar
                , _active
                , _roleId
                , _hoten);

            var text = "Chỉnh sửa thành công";
            if (result < 1) text = "Chỉnh sửa Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
    }
}