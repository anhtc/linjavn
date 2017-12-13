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
        public ActionResult LoadData([DataSourceRequest] DataSourceRequest request, int? tinhId)
        {
            var listItems = _db.Database.SqlQuery<QuanHuyenModel>(TVConstants.StoredProcedure.AdminQuanHuyen.GetQuanHuyenByAll,tinhId).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy tỉnh thành  phố
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetTinh([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<TinhModel>(TVConstants.StoredProcedure.AdminTinh.GetTinhByAll).ToList();
            return Json(listItems, JsonRequestBehavior.AllowGet);        
        }
        public ActionResult Insert(string tenQuan, int sapXep, int tinhId)
        {
            try
            {
                var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminQuanHuyen.AddQuanHuyen, tinhId, tenQuan, sapXep);

                var text = "Thêm mới thành công";
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
                var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminQuanHuyen.UpdateQuanHuyen, quanId, tinhId, tenQuan, sapXep);

                var text = "Cập nhật thành công";
                if (result <= 0) text = "Cập nhật thất bại";

                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Num = 0, Message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult Delete(string quanId)
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
        public ActionResult GetQuanById(int quanId)
        {
            var itemAnh = _db.Database.SqlQuery<QuanHuyen>(TVConstants.StoredProcedure.AdminQuanHuyen.GetQuanHuyenById, quanId).FirstOrDefault();

            return Json(itemAnh, JsonRequestBehavior.AllowGet);
        }
    }
}