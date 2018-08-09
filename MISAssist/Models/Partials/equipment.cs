using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MISAssist.Models
{
    [MetadataType(typeof(equipmentMD))]
    public partial class equipment
    {
        public class equipmentMD
        {
            [DisplayName("單位")]
            [Required]
            public string department { get; set; }

            [DisplayName("類別")]
            [Required]
            public string ftype { get; set; }

            [DisplayName("名稱")]
            public string name { get; set; }

            [DisplayName("用途")]
            public string feature { get; set; }

            [DisplayName("IP位址")]
            public string ip { get; set; }

            [DisplayName("作業系統")]
            public string op { get; set; }

            [DisplayName("位元")]
            public string op_bit { get; set; }

            [DisplayName("系統版權")]
            public string op_copyright { get; set; }

            [DisplayName("資料庫")]
            public string db { get; set; }

            [DisplayName("Office")]
            public string office { get; set; }

            [DisplayName("Office版權")]
            public string offcie_copyright { get; set; }

            [DisplayName("防毒軟體")]
            public string antivirus { get; set; }

            [DisplayName("說明")]
            public string note { get; set; }
        }
    }
}