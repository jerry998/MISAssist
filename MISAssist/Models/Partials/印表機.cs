using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MISAssist.Models
{
    [MetadataType(typeof(印表機MD))]
    public partial class 印表機
    {
        public class 印表機MD
        {
            public string 使用單位 { get; set; }
            public string 廠牌 { get; set; }
            public string 型號 { get; set; }
            public string 類別 { get; set; }
            public string 購買廠商 { get; set; }
            public Nullable<decimal> 價格 { get; set; }

            [DisplayName("購買日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
            public Nullable<System.DateTime> 購買日期 { get; set; }

            public string IP位址 { get; set; }
            public string 碳粉匣_黑 { get; set; }
            public string 碳粉匣_青 { get; set; }
            public string 碳粉匣_紅 { get; set; }
            public string 碳粉匣_黃 { get; set; }
            public string 備註 { get; set; }
        }
    }
}