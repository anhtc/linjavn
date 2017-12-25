using LinJas.Areas.AdminLinja.Common;
using LinJas.Areas.AdminLinja.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinJas.Areas.AdminLinja.Controllers
{
    public class BlogController : Controller
    {
        private readonly ManagerModel _db;
        public BlogController()
        {
            _db = new ManagerModel();
        }
        // clear bộ nhớ
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
        // GET: AdminLinja/Blog
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(
             int? danhMucId
            , string name
            , string noiDung
            , string tieuDe
            , string moTa
            , string tuKhoa
            , int sapXep
            , DateTime? ngayDang                      
            , HttpPostedFileBase mediaFile
            , bool active            
            )
        {
            try
            {
                var result = 0;
                var _media = new byte[] { 0x20 };
                if (mediaFile != null)
                {
                    var binaryReader = new BinaryReader(mediaFile.InputStream);
                    _media = binaryReader.ReadBytes(mediaFile.ContentLength);
                }
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminBlog.AddBlog
                    , danhMucId
                    , name
                    , noiDung
                    , tieuDe
                    , moTa
                    , tuKhoa
                    , sapXep
                    , ngayDang                   
                    , _media
                    , active
                    );
                var text = "Thêm mới thành công";
                if (result < 1) text = "Thêm mới Thất bại";
                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(
            Guid id
            , int? danhMucId
            , string name
            , string noiDung
            , string tieuDe
            , string moTa
            , string tuKhoa
            , int sapXep
            , DateTime? ngayDang
            , HttpPostedFileBase mediaFile
            , bool active
            , bool changeImage
            )
        {
            try
            {
                var result = 0;
                var _media = new byte[] { 0x20 };
                if (mediaFile != null)
                {
                    var binaryReader = new BinaryReader(mediaFile.InputStream);
                    _media = binaryReader.ReadBytes(mediaFile.ContentLength);
                }
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminBlog.UpdateBlog
                    , id
                    , danhMucId
                    , name
                    , noiDung
                    , tieuDe
                    , moTa
                    , tuKhoa
                    , sapXep
                    , ngayDang
                    , _media
                    , active
                    , changeImage
                    );
                var text = "Cập nhật thành công";
                if (result < 1) text = "Cập nhật Thất bại";
                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Delete(Guid? id)
        {
            var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminBlog.DeleteBlog, id);
            var text = "Đã xóa thành công";
            if (result < 1) text = "Xóa thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// update thẻ tag cho bài blog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="sortOrder"></param>
        /// <param name="loai"></param>
        /// <returns></returns>
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