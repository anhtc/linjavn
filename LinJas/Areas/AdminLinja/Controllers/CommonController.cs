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
    public class CommonController : Controller
    {
        private readonly ManagerModel _db;
        public CommonController()
        {
            _db = new ManagerModel();
        }
        // clear bộ nhớ
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
        // GET: AdminLinja/Common
        public ActionResult Index()
        {
            return View();
        }
        #region
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
        #endregion
        #region Danh mục

        public ActionResult LoadDataDanhMuc([DataSourceRequest] DataSourceRequest request, int? tinhId, int? danhmucId)
        {
            var listItems = _db.Database.SqlQuery<QuanHuyenModel>(TVConstants.StoredProcedure.AdminDanhMuc.GetdanhMucByAll, tinhId,danhmucId).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDanhMucById(int danhmucId)
        {
            var itemAnh = _db.Database.SqlQuery<QuanHuyen>(TVConstants.StoredProcedure.AdminDanhMuc.GetdanhMucById, danhmucId).FirstOrDefault();

            return Json(itemAnh, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Quận Huyện

        public ActionResult LoadDataQuanHuyen([DataSourceRequest] DataSourceRequest request, int? tinhId)
        {
            var listItems = _db.Database.SqlQuery<QuanHuyenModel>(TVConstants.StoredProcedure.AdminQuanHuyen.GetQuanHuyenByAll, tinhId).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQuanById(int quanId)
        {
            var itemAnh = _db.Database.SqlQuery<QuanHuyen>(TVConstants.StoredProcedure.AdminQuanHuyen.GetQuanHuyenById, quanId).FirstOrDefault();

            return Json(itemAnh, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}