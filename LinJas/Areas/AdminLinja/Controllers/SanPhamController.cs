using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinJas.Areas.AdminLinja.Common;
using System.IO;
using LinJas.Areas.AdminLinja.Models.AuthModel;

namespace LinJas.Areas.AdminLinja.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ManagerModel _db;
        public SanPhamController()
        {
            _db = new ManagerModel();
        }
        // clear bộ nhớ
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
        // GET: AdminLinja/SanPham
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(
             string tenSanPham
            , string title
			, string description
            , string keyword
            , int? giaCu
			, int? giaMoi
			, int? chietKhau
			, int? khuyenMaiId
            , DateTime? tuNgay
            , DateTime? denNgay
            , int? danhMuc
            , int? sapXep
            , int? trangThai
            , string noiDung
            , bool active
            , HttpPostedFileBase mediaFile
            , string tuKhoa
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
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminSanPham.AddSanPham
                    , tenSanPham
                    , title
                    , description
                    , keyword                    
                    , giaCu
                    , giaMoi
                    , chietKhau
                    , khuyenMaiId
                    , tuNgay
                    , denNgay
                    , danhMuc
                    , sapXep
                    , trangThai
                    , noiDung
                    , active
                    , _media
                    , tuKhoa
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
            Guid Id
            , string tenSanPham
            , string title
            , string description
            , string keyword
            , int? giaCu
            , int? giaMoi
            , int? chietKhau
            , int? khuyenMaiId
            , DateTime? tuNgay
            , DateTime? denNgay
            , int? danhMuc
            , int? sapXep
            , int? trangThai
            , string noiDung
            , bool active
            , HttpPostedFileBase mediaFile
            , bool changeImage
            , string tuKhoa
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
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminSanPham.UpdateSanPham,Id
                    , tenSanPham
                    , title
                    , description                    
                    , keyword
                    , giaCu
                    , giaMoi
                    , chietKhau
                    , khuyenMaiId
                    , tuNgay
                    , denNgay
                    , danhMuc
                    , sapXep
                    , trangThai
                    , noiDung
                    , active
                    , _media
                    , changeImage
                    , tuKhoa
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
            var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminSanPham.DeleteSanPham, id);
            var text = "Đã xóa thành công";
            if (result < 1) text = "Xóa thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateAnh(
             string tenAnhSanPham
            , Guid? sanphamId
            , HttpPostedFileBase mediaFile
            , int sapXep
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
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminAnhSanPham.AddAnhSanPham
                    , tenAnhSanPham
                    , sanphamId
                    , _media
                    , sapXep
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
        public ActionResult UpdateAnh(
            Guid? id
            , string tenAnh
            , Guid? sanphamId
            , HttpPostedFileBase mediaFile
            , int sapxep
            , bool changeAnh
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
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminAnhSanPham.UpdateAnhSanPham
                    ,id
                    , tenAnh
                    , sanphamId
                    , _media
                    , sapxep
                    , changeAnh
                    );
                var text = "Sửa thành công";
                if (result < 1) text = "Sửa Thất bại";
                return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DeleteAnh(Guid? id)
        {
            var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminAnhSanPham.DeleteAnhSanPham, id);
            var text = "Đã xóa thành công";
            if (result < 1) text = "Xóa thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }
    }
}