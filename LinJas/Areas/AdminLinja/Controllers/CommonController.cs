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
        public ActionResult GetDanhMucSanPham(int? tinhId)
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
            
            return Json(listDanhMucs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUpdateSanPhamById(Guid? _id)
        {
            var itemSanPham = _db.Database.SqlQuery<SanPham>(TVConstants.StoredProcedure.AdminSanPham.GetUpdateSanPhamById, _id).FirstOrDefault();

            return Json(itemSanPham, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAnhSanPhamId([DataSourceRequest] DataSourceRequest request, Guid? _id)
        {
            try
            {
                if (_id==null)
                {
                    _id = new Guid();
                }
                var itemSanPham = _db.Database.SqlQuery<AnhSanPhamModel>(TVConstants.StoredProcedure.AdminAnhSanPham.GetAnhSanPhamId, _id).ToList();
                return Json(itemSanPham.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetAnhSanPhamById(Guid? Id)
        {
            var itemAnhSanPham = _db.Database.SqlQuery<AnhSanPham>(TVConstants.StoredProcedure.AdminAnhSanPham.GetAnhSanPhamById, Id).FirstOrDefault();

            return Json(itemAnhSanPham, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ShowPhotoAnhSanPham(Guid? id)
        {
            var item = _db.Database.SqlQuery<AnhSanPham>(TVConstants.StoredProcedure.AdminAnhSanPham.GetAnhSanPhamById, id).FirstOrDefault();
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
        #region Blog
        public ActionResult LoadDataBlog([DataSourceRequest] DataSourceRequest request)
        {
            var listItems = _db.Database.SqlQuery<Blog>(TVConstants.StoredProcedure.AdminBlog.GetBlogByAll).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBlogById(Guid? id)
        {
            var itemBlog = _db.Database.SqlQuery<Blog>(TVConstants.StoredProcedure.AdminBlog.GetBlogById, id).FirstOrDefault();
            return Json(itemBlog, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ShowPhotoBlogById(Guid? id)
        {
            var item = _db.Database.SqlQuery<Blog>(TVConstants.StoredProcedure.AdminBlog.GetBlogById, id).FirstOrDefault();
            if (item != null && item.HinhAnh != null)
            {
                return File(item.HinhAnh, "image/png");
            }
            else
            {
                return File("~/Content/Images/NoImage.jpg", "image/png");
            }
        }
        public ActionResult LoadListTag([DataSourceRequest] DataSourceRequest request, string inputSearch, int? tinTucId)
        {
            var listItems = _db.Database.SqlQuery<TagModel>(TVConstants.StoredProcedure.AdminTag.BlogTags_SelectByBlogId, inputSearch, tinTucId).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CapNhatTagTinTuc(string tagIds, string unTagIds, int? tinTucId, string SortOrder)
        {
            var result = 0;
            result =
                _db.Database.ExecuteSqlCommand(
                TVConstants.StoredProcedure.AdminBlog.BlogTags_CapNhat, tinTucId, tagIds, unTagIds, SortOrder);
            var text = "Cập nhật thành công";
            if (result < 0) text = "Cập nhật thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
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
        #region Tag
        public ActionResult LoadDataTag([DataSourceRequest] DataSourceRequest request, string inputSearch, int? loai)
        {
            var listItems = _db.Database.SqlQuery<TagModel>(TVConstants.StoredProcedure.AdminTag.TagBlogSelectAll, inputSearch, loai).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDetailTag(int id)
        {
            var listItems = _db.Database.SqlQuery<TagModel>(TVConstants.StoredProcedure.AdminTag.TagBlogSelectById, id).FirstOrDefault();
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}