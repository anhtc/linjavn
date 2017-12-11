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
    
        public class AcountManager
        {
            public class StoredProcedure
            {
                public const string Login = "admin_Login"; //login
               
            }
        }
      #endregion
    }
}