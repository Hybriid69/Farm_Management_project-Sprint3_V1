using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{
    [Table("Land")]
    public class Land
    {

        [Key]
        
        public int LandID { get; set; }

        
        public int FarmID { get; set; }


        [Display(Name = "Number of Fields")]
        public int NumFields { get; set; }


        [Display(Name = "Size(m2)")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Only Numbers Allowed")]
        [Range(1, Double.MaxValue, ErrorMessage = "Size should be bigger")]
        public double Size { get; set; }

        [Required]
        public bool Disabled { get; set; }

        public virtual ICollection<Field> Fields { get; set; }

        [ForeignKey("FarmID")]
        public virtual Farm Farm { get; set; }
    }
}