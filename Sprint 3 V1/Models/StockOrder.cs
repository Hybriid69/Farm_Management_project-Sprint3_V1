using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class StockOrder
    {
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("StockID")]
        public virtual Stock Stock { get; set; }

        [Key]
        public int SOID { get; set; }

        [Column(Order = 1)]
        public int OrderID { get; set; }

        [Column(Order = 2)]
        public int StockID { get; set; }

        public double Quantity { get; set; }

        [Display(Name ="Subtotal")]
        public double SubPrice { get; set; }
    }
}