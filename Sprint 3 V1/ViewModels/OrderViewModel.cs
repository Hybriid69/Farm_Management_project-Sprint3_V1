using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public class OrderViewModel
    {
        [Key]
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Please enter status"), MaxLength(30)]
        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Order Total")]
        public double Total { get; set; }

        public int Quantity { get; set; }


    }
}