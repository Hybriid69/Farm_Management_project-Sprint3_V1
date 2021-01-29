using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public class CheckoutItemsViewModel
    {

        [Display(Name = "Stock Name")]
        public string StockName { get; set; }

        public double Quantity { get; set; }
    }
}