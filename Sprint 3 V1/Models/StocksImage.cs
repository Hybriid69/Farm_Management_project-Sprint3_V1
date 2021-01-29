using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class StocksImage
    {
        [Key]
        public int StockImageID { get; set; }

        [Display(Name = "Stock Image")]
        public byte[] StockImage { get; set; }

        public string ImageName { get; set; }
        public System.Nullable<int> StockID { get; set; }
        [ForeignKey("StockID")]
        public  Stock Stock{ get; set; }
    }
}