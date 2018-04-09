using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LinJas.Common;
using LinJas.Models;

namespace LinJas.Manager
{
    public class AcountManager:BaseBlogManager
    {
        private static AcountManager _instance;
        private AcountManager()
        {
        }
        public static AcountManager Instance
        {
            get { return _instance ?? (_instance = new AcountManager()); }
        }
        //public byte[] GetPhotoBlog(string tenIMG)
        //{
        //    try
        //    {
        //        tenIMG = tenIMG.Split('.')[0];
        //        var words = tenIMG.Split('-');
        //        int id;
        //        int.TryParse(words[words.Length - 1], out id);
        //        var photo = (byte[])Database.ExecuteScalar(Constants.AcountManager.StoredProcedure.Login, id);

        //        return photo;
        //    }
        //    catch (Exception ex)            {

        //        throw ex;
        //    }
        //}
        /// <summary>
        /// đăng nhập
        /// </summary>
        /// <returns></returns>
        public List<LoginModel> Login(string UserName, string PasswordHash)
        {
            try
            {
                var rowMapper = MapBuilder<LoginModel>.MapAllProperties().Build();
                var result = Database.ExecuteSprocAccessor(Constants.AcountManager.StoredProcedure.Login, rowMapper, UserName, PasswordHash).ToList();
              
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}