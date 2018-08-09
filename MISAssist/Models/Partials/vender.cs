using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MISAssist.Models
{
    [MetadataType(typeof(venderMD))]
    public partial class vender
    {
        public class venderMD
        {
            [DisplayName("公司名稱")]
            [Required]
            public string company { get; set; }

            [DisplayName("聯絡人")]
            [Required]
            public string contact { get; set; }

            [DisplayName("職稱")]
            public string title { get; set; }

            [DisplayName("E-Mail")]
            public string email { get; set; }

            [DisplayName("電話")]
            public string tel_office { get; set; }

            [DisplayName("行動電話")]
            public string tel_mobile { get; set; }

            [DisplayName("傳真")]
            public string fax { get; set; }

            [DisplayName("速撥")]
            public string quick_no { get; set; }

            [DisplayName("備註")]
            public string note { get; set; }
        }
    }
}