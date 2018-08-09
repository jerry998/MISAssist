using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MISAssist.Models
{
    [MetadataType(typeof(cart_transactionMD))]
    public partial class cart_transaction
    {
        public class cart_transactionMD
        {
            [DisplayName("印表機ID")]
            public Nullable<int> printer_id { get; set; }

            [DisplayName("碳粉匣")]
            public string cartridge { get; set; }

            [DisplayName("單價")]
            public Nullable<int> price { get; set; }

            [DisplayName("數量")]
            public Nullable<int> quantity { get; set; }

            [DisplayName("廠商")]
            public string vender { get; set; }

            [DisplayName("入出")]
            public string in_out { get; set; }

            [DisplayName("日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
            public Nullable<System.DateTime> date { get; set; }
        }
    }
}