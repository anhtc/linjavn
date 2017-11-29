namespace LinJas.Common
{
    public class Constants
    {
        public class Schema
        {
            public const string Http = "http";
            public const string Https = "https";
        }     
        #region Session

        public class Session
        {
            public const string AccountRegister = "AccountRegister";
            public const string AccountForgotPassword = "AccountForgotPassword";
            public const string Setting = "Setting";
            public const string GameShow = "GameShow";
            public const string TagSave = "TagSave";
            public const string FilterParamModel = "FilterParamModel";
            public const string TabIndex = "TabIndex";
            public const string ViDo = "ViDo";
            public const string KinhDo = "KinhDo";
            public const string GiamGiaId = "GiamGiaId";
            public const string CateFilterId = "CateFilterId";
            public const string BlogTinhId = "BlogTinhId";
        }
        public class Cookies
        {
            public const string ProvinceId = "ProvinceId";
            public const string DistrictId = "DistrictId";
            public const string EventId = "EventId";
            public const string TypeSearch = "TypeSearch";
        }

        #endregion       

        #region Manager
    
        public class HomeManager
        {

            public class StoredProcedure
            {
                public const string GetDatChoNhieuNhat = "NoDistance_HomeManager_GetDatChoNhieuNhat"; //Lấy danh sách bài ĐẶT CHỖ NHIỀU
                public const string GetDiemDenMoiNhat = "NoDistance_HomeManager_GetDiemDenMoiNhat"; //Lấy danh sách bài MỚi NHẤT
                public const string GetGiamGiaNhieuNhat = "NoDistance_HomeManager_GetGiamGiaNhieuNhat"; //Lấy danh sách bài GIẢM GIÁ NHIỀU NHẤT
                public const string GetGiamGiaNhieuNhatVsTab = "NoDistance_HomeManager_GetGiamGiaNhieuNhatVsTab"; //Lấy danh sách bài GIẢM GIÁ NHIỀU NHẤT với Tab ở Mobile
                public const string GetDatNhieuNhatVsTab = "NoDistance_HomeManager_GetDatNhieuNhatVsTab"; //Lấy danh sách Đặt nhiều với Tab ở Mobile
                public const string GetDiemDenHot = "NoDistance_HomeManager_GetDiemDenHot"; //Lấy danh sách bài HOT/TIÊU BIẾU
                public const string GetDiemDenHotHome = "NoDistance_HomeManager_GetDiemDenHotHome"; //Lấy danh sách bài HOT/TIÊU BIẾU
                public const string GetDiemDenOfTag = "NoDistance_HomeManager_GetDiemDenOfTag"; //Lấy danh sách bài CỦA TAG.

                public const string GetQuanHuyenWithTinhId = "NoDistance_HomeManager_GetQuanHuyenWithTinhId"; //Lấy danh sách bài CỦA TAG.
                public const string GetQuanHuyenWithId = "NoDistance_HomeManager_GetQuanHuyenWithId"; //Lấy Chi tiết quận vs Id.
                public const string GetLocationWithTinh = "NoDistance_HomeManager_GetLocationWithTinh"; //Lấy vị trí với Tỉnh.
                // Bản mới 02/2017
                public const string GetCategoryHome = "NoDistance_HomeManager_GetCategoryHome";// lấy danh sách theo Tỉnh và Mã
                public const string GetNumberPartnerWithTag = "NoDistance_HomeManager_GetNumberPartnerWithTag";// lấy số lượng điểm đến theo TagId
                public const string CollectionGetTop = "NoDistance_HomeManager_CollectionGetTop";// lấy danh sách Bộ sưu tập
                public const string HomeCategoryGetPhoto = "NoDistance_HomeManager_HomeCategory_GetPhoto";
                public const string HomeCategoryGetPhotoAnhTo = "NoDistance_HomeManager_HomeCategory_GetPhotoAnhTo";//lấy  ảnh to cho slide  gợi ý
                public const string GetDiemDenByAnUong = "NoDistance_HomeManager_GetDiemDenByAnUong";// lấy danh sách điểm đến theo tag ăn uống
                public const string GetCategoryByTinhId = "NoDistance_HomeManager_CollectionGetCategoryByTinhId";// Lấy  danh sách các danh mục khám  phá  theo tỉnh
            }
        }
      #endregion
    }
}