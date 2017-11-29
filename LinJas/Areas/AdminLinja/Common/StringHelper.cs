using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LinJas.Manager.Common
{
    public static class StringHelper
    {
        public static string ToSeoUrl(this string url)
        {
            var encodedUrl = (url ?? string.Empty).ToLower();
            encodedUrl = encodedUrl.ToUnSign();
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");
            encodedUrl = encodedUrl.Replace("'", "");
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        public static string ToUnSign(this string url)
        {
            url = (url ?? string.Empty).ToLower();
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = url.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ToAddressShortName(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var lstString = value.Split(',');

            return lstString.Length > 1 ? lstString[1] : lstString[0];
        }

        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(value);
        }
        
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) 
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
       
    }
}