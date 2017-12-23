using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinJas.Areas.AdminLinja.Common;
using System.IO;
using LinJas.Areas.AdminLinja.Models;
using LinJas.Areas.AdminLinja.Models.AuthModel;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace LinJas.Areas.AdminLinja.Controllers
{
    public class TagController : Controller
    {
        private readonly ManagerModel _db;
        public TagController()
        {
            _db = new ManagerModel();
        }
        // clear bộ nhớ
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
        // GET: AdminLinja/Tag
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadData([DataSourceRequest] DataSourceRequest request, string inputSearch, int? loai)
        {
            var listItems = _db.Database.SqlQuery<TagModel>(TVConstants.StoredProcedure.AdminTag.TagBlogSelectAll, inputSearch, loai).ToList();
            return Json(listItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDetail(int id)
        {
            var listItems = _db.Database.SqlQuery<TagModel>(TVConstants.StoredProcedure.AdminTag.TagBlogSelectById, id).FirstOrDefault();
            return Json(listItems, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InsertTag(string name, int sortOrder, int loai)
        {
            var mediaData = new byte[] { 0x20 };
            var moTa = "";
            var result = 0;
            result =
                _db.Database.ExecuteSqlCommand(
                TVConstants.StoredProcedure.AdminTag.TagBlogInsert, name, sortOrder, loai, moTa, mediaData);

            var text = "Thêm thành công";
            if (result == 0) text = "Thêm Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UpdateTag(int id, string name, int sortOrder, int loai)
        {
            var mediaData = new byte[] { 0x20 };
            var moTa = "";
            var result = 0;
            result =
                _db.Database.ExecuteSqlCommand(
                TVConstants.StoredProcedure.AdminTag.TagBlogUpdate, id, name, sortOrder, loai, moTa, mediaData);
            var text = "Sửa thành công";
            if (result == 0) text = "Sửa Thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTag(int id)
        {
            var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminTag.TagBlogDelete, id);
            var text = "Đã xóa thành công";
            if (result == -1)
                text = "Không được xóa đối tác này";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
    }
}