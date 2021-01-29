using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{
    [Table("EmpPos")]
    public class EmpPos
    {

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

        [Key]
        public int EmpPosID { get; set; }

        [ Column(Order = 1)]
        public int EmployeeID { get; set; }

        [ Column(Order = 2)]
        public int PositionID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Started { get; set; }

        [DataType(DataType.DateTime)]
        public System.Nullable<DateTime> Ended { get; set; }



    }
}