using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MISAssist.Models

{
    [MetadataType(typeof(contactMD))]
    public partial class contact
    {
        public class contactMD
        {
            [DisplayName("單位")]
            [Required]
            public string department { get; set; }

            [DisplayName("員工編號")]
            public string pno { get; set; }

            [DisplayName("姓名")]
            [Required]
            public string name { get; set; }

            [DisplayName("職稱")]
            public string title { get; set; }

            [DisplayName("公司電話")]
            public string tel_office { get; set; }

            [DisplayName("分機")]
            public string tel_ext { get; set; }

            [DisplayName("行動電話")]
            public string tel_mobile { get; set; }

            [DisplayName("備註")]
            public string note { get; set; }
        }
    }
}