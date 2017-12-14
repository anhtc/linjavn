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
        
        public ActionResult Insert(int parentId, string ma, string name, int sapXep, int tinhId)
        {
            try
            {
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

        public ActionResult Update(int id, int parentId, string ma, string name, int sapXep, int tinhId)
        {
            try
            {
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
            listItems.Add(model);
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Danh mục

        public ActionResult LoadDataDanhMuc([DataSourceRequest] DataSourceRequest request, int? tinhId, int? danhmucId)
        {
            var listItems = _db.Database.SqlQuery<QuanHuyenModel>(TVConstants.StoredProcedure.AdminDanhMuc.GetdanhMucByAll, tinhId, danhmucId).ToList();
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