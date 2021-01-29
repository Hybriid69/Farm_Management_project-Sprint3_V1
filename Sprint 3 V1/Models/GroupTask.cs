using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class GroupTask
    {
        [ForeignKey("EmployeeID")]
        public virtual Group Group { get; set; }

        [ForeignKey("PTaskID")]
        public virtual PlantedTask PlantedTask { get; set; }

        [Key]
        public int GTID { get; set; }

        [ Column(Order = 1)]
        public int PTaskID { get; set; }

        [ Column(Order = 2)]
        public int EmployeeID { get; set; }
    }
}