using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sprint_3_V1.Models;

namespace Sprint_3_V1.Models
{
    public class Planted
    {
        [Key]
        public int PlantedID { get; set; }

        public string Status { get; set; }

        public int FieldID { get; set; }

        public string FieldName { get; set; }

        [Display(Name = "Crop ID")]
        public int CropID { get; set; }

        [Display(Name = "Crop Name")]
        public string CropName { get; set; }

        [Display(Name = "Quantity Planted")]
        public double QuantityPlanted { get; set; }

        [Display(Name = "Date Planted")]
        public DateTime PlantedDate { get; set; }

        [Display(Name = "Date of Expected Harvest")]
        public DateTime ExpectedHarvestDate { get; set; }

        [Display(Name = "Expected Yield (Kilograms)")]
        public double ExpectedYield { get; set; }

        [Display(Name = "Last Watered")]
        [DataType(DataType.Date)]
        public DateTime LastWatered { get; set; }

        [Display(Name = "Next Water")]
        public DateTime NextWater { get; set; }

        [ForeignKey("CropID")]
        public virtual CropInfo Cropsinfo { get; set; }

        [ForeignKey("FieldID")]
        public virtual Field Field { get; set; }
    }
}