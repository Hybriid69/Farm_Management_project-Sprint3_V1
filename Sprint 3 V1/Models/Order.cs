using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mail;
using System.Net.Mail;

namespace Sprint_3_V1.Models
{
    [Table("Order")]
    public class Order
    {
       

       [Key]
       [Display(Name = "Order ID")]
        public int OrderID { get; set; }
        
        [Required(ErrorMessage = "Please enter status"), MaxLength(30)]
        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Display(Name ="Order Total")]
        public double Total { get; set; }

        public DateTime DateOfOrder { get; set; }

        public virtual ICollection<Sale> Sale { get; set; }

        public virtual ICollection<StockOrder> StockOrder { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

    }
}