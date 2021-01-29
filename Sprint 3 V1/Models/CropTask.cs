using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.Models
{
    public class CropTask
    {
        [ForeignKey("MTaskID")]
        public virtual TaskMaster TaskMaster { get; set; }

        [ForeignKey("CropID")]
        public virtual CropInfo CropInfo { get; set; }

        [Key]
        public int CTID { get; set; }

        [ Column(Order = 1)]
        public int MTaskID { get; set; }

        [ Column(Order = 2)]
        public int CropID { get; set; }

        [Display(Name ="Duration per Square Meter")]
        public int Duration { get; set; }
    }
}