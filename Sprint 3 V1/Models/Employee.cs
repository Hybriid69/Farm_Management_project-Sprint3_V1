using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint_3_V1.Models
{
    [Table("Employee")]
    public class Employee
    {
        [NotMapped]
        public string Search { get; set; }
        
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee Username")]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }


        [StringLength(13)]
        [Required]
        [Display(Name = "ID Number")]
        [RegularExpression(@"^[0-9]{13,13}$", ErrorMessage = "Must be only 13 digits")]
        public string IDNumber { get; set; }

        [Required]
        [Display(Name = "Date Hired")]
        [DataType(DataType.Date)]
        public DateTime DateHired { get; set; }

        [Required]
        [Display(Name = "Cell Number")]
        [RegularExpression(@"^(\+?27|0)[1-8][0-9]{8}$", ErrorMessage = "Invalid Phone number")]
        [StringLength(12, ErrorMessage = "Number cannot be more than 12 digits")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNum { get; set; }

        [Required]
        [Display(Name = "Net of Kin Cell Number")]
        [RegularExpression(@"^(\+?27|0)[1-8][0-9]{8}$", ErrorMessage = "Invalid Phone number")]
        [StringLength(10, ErrorMessage = "Number cannot be more than 10 digits")]
        [DataType(DataType.PhoneNumber)]
        public string KinContactNum { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Farm ID")]
        public int FarmID { get; set; }

        [Display(Name = "Position ID")]
        public int PositionID { get; set; }

        [Display(Name = "Account ID")]
        public int AccountID { get; set; }

        [Display(Name ="Group ID")]
        public int GroupID { get; set; }

        [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }

        [ForeignKey("GroupID")]
        public virtual Group Group { get; set; }

        public virtual ICollection<EmpPos> EmpPos { get; set; }

        public virtual ICollection<FarmEmployee> FarmEmployee { get; set; }

    }
}