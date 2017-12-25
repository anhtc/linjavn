using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinJas.Areas.AdminLinja.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int SortOrder { get; set; }       
        public int Type { get; set; }
        public string TypeName
        {
            get
            {
                if (Type == 1)
                    return "Sản phẩm";

                if (Type == 2)
                    return "Blog";

                if (Type == 3)
                    return "Tin tức";

                return "Sản phẩm";
            }
        }
        public int IsCheck { get; set; }

        public string IsCheckFormat
        {
            get
            {
                if (IsCheck == 1)
                {
                    return "checked=true";
                }
                return "";
            }
        }
    }
}