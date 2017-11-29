using LinJas.Manager.Common;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace LinJas.Manager.Common
{
    public class PWUtility
    {
       
        public static byte[] ConvertToBytes(Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public static bool IsValidImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return true;
            }
            return file.ContentLength <= 1 * 320 * 320;
        }

        //public static DbGeography CreatePoint(double latitude, double longitude)
        //{
        //    var text = string.Format(CultureInfo.InvariantCulture.NumberFormat,
        //    "POINT({0} {1})", longitude, latitude);
        //    return DbGeography.PointFromText(text, 4326);
        //}

        public static string GetValueByKey(string key)
        {
            try
            {
                string value = WebConfigurationManager.AppSettings[key];
                return value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

      
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            var replace = Regex.Replace(source, "<.*?>", string.Empty);
            return replace;
        }
        /// <summary>
        /// Lấy mật khẩu
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            MD5 Md5Hash = MD5.Create();
            byte[] data = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }
       
    }
}