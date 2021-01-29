using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sprint_3_V1.Models;

namespace Sprint_3_V1.Models
{
    [Table("Field")]
    public class Field
    {
        [Key]
        [Display(Name = "Field ID")]
        public int FieldID { get; set; }


        [Display(Name = "Land ID")]
        public int LandID { get; set; }

        [Display(Name = "Field Name")]
        public string FieldName { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Size(m2)")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Only Numbers Allowed")]
        [Range(1, Double.MaxValue, ErrorMessage = "Size should be greater than or equal to 1")]
        public double Size { get; set; }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++    


        [ForeignKey("LandID")]
        public virtual Land Land { get; set; }

        public virtual ICollection<PlantedTask> Task { get; set; }

        [Required]
        public bool Disabled { get; set; }

    }
}