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
    public class DichVuController : Controller
    {
        // GET: AdminLinja/DichVu
        private readonly ManagerModel _db;
        public DichVuController()
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
             string tieuDe
            , string moTa
            , string noiDung
            , bool active
            , int sapxep
            , string metaTieuDe
            , string metaMoTa
            , string metaTuKhoa
            , HttpPostedFileBase mediaFile
            , int danhGiaSao
            , int soNguoiDanhGia
            , string ghiChu
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
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminDichVu.AddDichVu
                    , tieuDe
                    , moTa
                    , noiDung
                    , active
                    , sapxep
                    , metaTieuDe
                    , metaMoTa
                    , metaTuKhoa
                    , _media
                    , danhGiaSao
                    , soNguoiDanhGia
                    , ghiChu
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
            int id
            , string tieuDe
            , string moTa
            , string noiDung
            , bool active
            , int sapxep
            , string metaTieuDe
            , string metaMoTa
            , string metaTuKhoa
            , HttpPostedFileBase mediaFile
            , int danhGiaSao
            , int soNguoiDanhGia
            , string ghiChu
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
                result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminDichVu.UpdateDichVu
                     , id
                     , tieuDe
                     , moTa
                     , noiDung
                     , active
                     , sapxep
                     , metaTieuDe
                     , metaMoTa
                     , metaTuKhoa
                     , _media
                     , danhGiaSao
                     , soNguoiDanhGia
                     , ghiChu
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
            var result = _db.Database.ExecuteSqlCommand(TVConstants.StoredProcedure.AdminDichVu.DeleteDichVu, id);
            var text = "Đã xóa thành công";
            if (result < 1) text = "Xóa thất bại";
            return Json(new { Num = result, Message = text }, JsonRequestBehavior.AllowGet);
        }

    }
}