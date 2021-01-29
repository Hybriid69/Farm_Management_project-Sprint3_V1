using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{
    [Table("Farm")]
    public class Farm
    {
        [Key]
        [Display(Name = "Farm ID")]
        public int FarmID { get; set; }

        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Number of Workers")]
        public int NumWorkers { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name ="Number Of Parcels Of Land")]
        public int NumLand { get; set; }

        [Display(Name ="Total Land Area")]
        public double LandArea { get; set; }

        [Required]
        public bool Disabled { get; set; }



        public virtual ICollection<Land> Land { get; set; }

        public virtual ICollection<FarmEmployee> FarmEmployee { get; set; }
    }
}