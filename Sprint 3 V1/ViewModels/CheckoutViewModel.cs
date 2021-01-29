using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public class CheckoutViewModel
    {
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Payment Option")]
        public string PaymentOption { get; set; }

        [Display(Name = "Collection Option")]
        public string CollectionOption { get; set; }

        [Display(Name = "Order Total")]
        public double Total { get; set; }

    }
}