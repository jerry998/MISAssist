using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MISAssist.Models
{
    [MetadataType(typeof(printerMD))]
    public partial class printer
    {
        public class printerMD
        {
            [DisplayName("使用單位")]
            [Required]
            public string department { get; set; }

            [DisplayName("廠牌")]
            [Required]
            public string brand { get; set; }

            [DisplayName("型號")]
            [Required]
            public string model { get; set; }

            [DisplayName("類別")]
            public string type { get; set; }

            [DisplayName("購買廠商")]
            public string vender { get; set; }

            [DisplayName("價格")]
            public Nullable<decimal> price { get; set; }

            [DisplayName("購買日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
            public Nullable<System.DateTime> date { get; set; }

            [DisplayName("IP位址")]
            public string ip { get; set; }

            [DisplayName("碳粉匣_黑")]
            public string cartridge_black { get; set; }

            [DisplayName("碳粉匣_青")]
            public string cartridge_blue { get; set; }

            [DisplayName("碳粉匣_紅")]
            public string cartridge_red { get; set; }

            [DisplayName("碳粉匣_黃")]
            public string cartridge_yellow { get; set; }

            [DisplayName("備註")]
            public string note { get; set; }
        }
    }
}