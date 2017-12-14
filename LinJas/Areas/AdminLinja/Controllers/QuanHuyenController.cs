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
    public class QuanHuyenController : Controller
    {
        private readonly ManagerModel _db;
        public QuanHuyenController()
        {
            _db = new ManagerModel();
        }
        // clear bộ nhớ
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
        // GET: AdminLinja/QuanHuyen
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Insert(string tenQuan, int sapXep, int tinhId)
        {
            try
            {
                var text = "Thêm mới thành công";
                var result = 0;
                if (tinhId == 0)
                {
                    text = "Bạn cần chọn tỉnh thành  phố trước khi thêm mới quận huyện.";
                    return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
                }
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminQuanHuyen.AddQuanHuyen, tinhId, tenQuan, sapXep);

                
                if (result <= 0) text = "Thêm mới thất bại";

                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Num = 0, Message = "Thêm mới thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Update(string tenQuan, int sapXep, int tinhId, int quanId)
        {
            try
            {
                var text = "Cập nhật thành công";
                var result = 0;
                if (tinhId==0)
                {
                    text = "Bạn cần chọn tỉnh thành  phố trước khi sửa.";
                    return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
                }
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminQuanHuyen.UpdateQuanHuyen, quanId, tinhId, tenQuan, sapXep);

                if (result <= 0) text = "Cập nhật thất bại";

                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Num = 0, Message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult Delete(int quanId)
        {
            try
            {
                var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminQuanHuyen.DeleteQuanHuyen, quanId);

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