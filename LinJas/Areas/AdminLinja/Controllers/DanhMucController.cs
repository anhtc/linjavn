using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinJas.Areas.AdminLinja.Models.AuthModel;
using LinJas.Areas.AdminLinja.Models;
using LinJas.Areas.AdminLinja.Common;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


namespace LinJas.Areas.AdminLinja.Controllers
{
    public class DanhMucController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: AdminLinja/DanhMuc
        public ActionResult Index()
        {
            return View();
        }
        private readonly ManagerModel _db;
        public DanhMucController()
        {
            _db = new ManagerModel();
        }
        // clear bộ nhớ
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }      
        
        public ActionResult Insert(int parentId, string name, int sapXep, int tinhId)
        {
            try
            {
                string ma = Common.StringHelper.ToUnsignTrim(name);

                var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminDanhMuc.addddanhMuc, parentId, ma, name, sapXep, tinhId);

                var text = "Thêm mới thành công";
                if (result <= 0) text = "Thêm mới thất bại";

                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Num = 0, Message = "Thêm mới thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Update(int id, int parentId, string name, int sapXep, int tinhId)
        {
            try
            {
                string ma = Common.StringHelper.ToUnsignTrim(name);
                var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminDanhMuc.UpdatedanhMuc, id,parentId,ma,name,sapXep, tinhId);

                var text = "Cập nhật thành công";
                if (result <= 0) text = "Cập nhật thất bại";

                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Num = 0, Message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int danhmucId)
        {
            try
            {
                var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminDanhMuc.DeleteDanhMuc, danhmucId);

                var text = "Xóa thành công";
                if (result <= 0) text = "Xóa thất bại";

                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Num = 0, Message = "Xóa ảnh Thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }        
    }
}