using System;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;
using System.Web;
using System.Text.RegularExpressions;

namespace LinJas.Common
{
    public static class Utilities
    {
        public static string GetDay(int i)
        {
            int day = (int)DateTime.Now.Date.AddDays(i).DayOfWeek;
            string value = "";
            switch (day)
            {
                case 1:
                    value = "Thứ 2";
                    break;
                case 2:
                    value = "Thứ 3";
                    break;
                case 3:
                    value = "Thứ 4";
                    break;
                case 4:
                    value = "Thứ 5";
                    break;
                case 5:
                    value = "Thứ 6";
                    break;
                case 6:
                    value = "Thứ 7";
                    break;
                default:
                    value = "Chủ nhật";
                    break;
            }
            return value;
        }
        public static string GetValueByKey(string key)
        {
            try
            {
                var value = WebConfigurationManager.AppSettings[key];
                return value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static readonly MD5 Md5Hash = MD5.Create();

        /// <summary>
        /// Determines if a nullable Guid (Guid?) is null or Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return (!guid.HasValue || guid.Value == Guid.Empty);
        }
        public static string GetMd5Hash(string input)
        {
            byte[] data = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }
        public static string GetClientIP()
        {
            var ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string RemoveStyle(string html) {
            try
            {
                string cleaned;
                cleaned = new Regex("style=\"[^\"]*\"").Replace(html, "");
                cleaned = new Regex("(?<=class=\")([^\"]*)\\babc\\w*\\b([^\"]*)(?=\")").Replace(cleaned, "$1$2");
                return cleaned;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //loại bỏ  các thẻ html,style tự sinh ra khi nhập liệu
        public static string ReplaceHtml(string Txt)
        {
            Txt = Regex.Replace(Txt, "<(.|\\n)*?>", string.Empty);
            return Txt;
        }
    }
}
