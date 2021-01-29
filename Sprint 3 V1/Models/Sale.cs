using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{
    [Table("Sale")]
    public class Sale
    {
        [Key]
        [Display(Name = "Sale ID")]
        public int SaleID { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Total Sale Value")]
        public double Total { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

    }
}