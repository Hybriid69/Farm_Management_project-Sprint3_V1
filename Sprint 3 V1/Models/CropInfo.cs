using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{ 
    [Table("CropInfo")]
    public class CropInfo
    {
        [Key]
        public int CropID { get; set; }

        [Required(ErrorMessage = "Please enter a valid name"), MaxLength(30)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter a description"), MaxLength(40)]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Please enter a season"), MaxLength(6)]
        public string Season { get; set; }

        [Required(ErrorMessage = "Please enter the amount of spacing")]
        [Range(0.0, 10000.00, ErrorMessage = "Please enter correct value")]
        public double Spacing { get; set; }

        [Required(ErrorMessage = "Please enter the average yield per Square Meter")]
        [Range(0.0, 10000.00, ErrorMessage = "Please enter correct value")]
        [Display(Name = "Average Yield(kilograms)")]
        public double AverageYield { get; set; }

        [Required(ErrorMessage = "Please enter a value for irregation interval")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Irregation Interval(days)")]
        public int IrrigationInterval { get; set; }

        [Required(ErrorMessage = "Please enter a value for Growth time")]
        [Range(0.0, 10000.00, ErrorMessage = "Please enter correct value")]
        [Display(Name = "Growth time(days)")]
        public double GrowthTime { get; set; }

        public int LifeExpect { get; set; }

        [Required]
        public bool Disabled { get; set; }

        public virtual ICollection<Field> fields { get; set; }

        public virtual ICollection<Stock> Stock { get; set; }

        public virtual ICollection<CropTask> CropTask { get; set; }

    }
}