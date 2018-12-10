using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MISAssist.Models
{
    [MetadataType(typeof(異動記錄MD))]
    public partial class 異動記錄
    {
        public class 異動記錄MD
        {
            public string 碳粉匣 { get; set; }
            public Nullable<int> 單價 { get; set; }
            public Nullable<int> 數量 { get; set; }
            public string 廠商 { get; set; }
            public string 入出 { get; set; }

            [DisplayName("異動日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
            public Nullable<System.DateTime> 日期 { get; set; }
        }
    }
}