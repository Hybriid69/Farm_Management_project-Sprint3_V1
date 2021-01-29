using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class FarmEmployee
    {
        [ForeignKey("FarmID")]
        public Farm Farm { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        [Key]
        public int FEID { get; set; }

        [ Column(Order = 1)]
        public int FarmID { get; set; }

        [ Column(Order = 2)]
        public int EmployeeID { get; set; }

        public DateTime DateStarted { get; set; }

        public DateTime DateEnded { get; set; }
    }
}