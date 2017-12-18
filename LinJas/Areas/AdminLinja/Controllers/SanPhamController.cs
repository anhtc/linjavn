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
            , string tuNgay
            , string denNgay
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
    }
}