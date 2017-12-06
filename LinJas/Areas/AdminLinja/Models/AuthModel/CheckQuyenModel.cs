using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models.AuthModel
{
    public class CheckQuyenModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Mota
        {
            get
            {
                string text = "";
                if (string.IsNullOrEmpty(Description))
                {
                    text += "Null";                   
                }
                else
                {
                    text += Description;
                }
                return text;
            }
        }
        public int isBranch { get; set; }
        public string isBranchFormat
        {
            get
            {
                if (isBranch > 0)
                {
                    return "checked=true";
                }
                return "";
            }
        }
    }
}