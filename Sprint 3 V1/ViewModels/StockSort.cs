using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public class StockSort
    {
        [Key]
        public int SortID { get; set; }
        public string SortBy { get; set; }
    }
}