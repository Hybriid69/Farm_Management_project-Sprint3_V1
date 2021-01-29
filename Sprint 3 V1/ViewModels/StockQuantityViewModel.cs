using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public class StockQuantityViewModel
    {
        //[Key]
        //public int QuantityID  { get; set; }

        public int Quantity { get; set; }
    }
}