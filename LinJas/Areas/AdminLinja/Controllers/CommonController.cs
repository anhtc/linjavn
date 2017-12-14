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
        #region Tỉnh thành
        /// <summary>
        /// Lấy tỉnh thành  phố
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetTinh([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<TinhModel>(TVConstants.StoredProcedure.AdminTinh.GetTinhByAll).ToList();
            TinhModel model = new TinhModel { Id = 0, Name = "Tất cả" };
            listItems.Insert(0,model);
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Danh mục

        public ActionResult LoadDataDanhMuc([DataSourceRequest] DataSourceRequest request, int? tinhId, int? danhmucId)
        {
            var listItems = _db.Database.SqlQuery<DanhMuc>(TVConstants.StoredProcedure.AdminDanhMuc.GetdanhMucByAll, tinhId, danhmucId).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <param name="danhmucId"></param>
        /// <returns></returns>
        public ActionResult GetDataDanhMuc(int? tinhId)
        {
            var listDanhMucs = new List<DanhMuc>();
            if (tinhId == null)
                tinhId = 0;

            var listParents = _db.Database.SqlQuery<DanhMuc>(TVConstants.StoredProcedure.AdminDanhMuc.GetdanhMucBySelect, tinhId, 0).ToList();
            foreach (var items in listParents)
            {
                items.Ten = "+ " + items.Ten;
                listDanhMucs.Add(items);

                var childenDanhMucs = _db.Database.SqlQuery<DanhMuc>(TVConstants.StoredProcedure.AdminDanhMuc.GetdanhMucBySelect, tinhId, items.Id).ToList();
                foreach (var childenitem in childenDanhMucs)
                {
                    childenitem.Ten = " -------- " + childenitem.Ten;
                    listDanhMucs.Add(childenitem);
                }
            }
            var model = new DanhMuc { Id = 0, Ten = "-- Chuyên mục cha --" };
            listDanhMucs.Insert(0, model);

            return Json(listDanhMucs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDanhMucById(int danhmucId)
        {
            var itemAnh = _db.Database.SqlQuery<DanhMuc>(TVConstants.StoredProcedure.AdminDanhMuc.GetdanhMucById, danhmucId).FirstOrDefault();

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
        #region Sản Phẩm
        public ActionResult LoadDataSanPham([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<SanPhamModel>(TVConstants.StoredProcedure.AdminSanPham.GetSanPhamByAll).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region lấy ảnh avatar
        /// <summary>
        /// Show ảnh avatar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowPhotoSanPhamById(Guid? id)
        {
            var item = _db.Database.SqlQuery<SanPhamModel>(TVConstants.StoredProcedure.AdminSanPham.GetUpdateSanPhamById, id).FirstOrDefault();
            if (item != null && item.HinhAnh != null)
            {
                return File(item.HinhAnh, "image/png");
            }
            else
            {
                return File("~/Content/Images/NoImage.jpg", "image/png");
            }
        }
        #endregion
    }
}