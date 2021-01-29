using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{
    [Table("Position")]
    public class Position
    {
        
        [Key]
        [Display(Name = "Position ID")]
        public int PositionID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Position Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Job Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Basic Salary")]
        public double BaseSalary { get; set; }

        [Display(Name = "Disabled")]
        public bool Disabled { get; set; }
        public virtual ICollection<EmpPos> EmpPos { get; set; }
    }
}