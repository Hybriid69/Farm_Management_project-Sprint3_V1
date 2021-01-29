using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public class EmployeeAccountsViewModel
    {
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }

        [DuplicateID(ErrorMessage = "User Name alredy exists!")]
        [Display(Name = "User Name")]
        [StringLength(20, ErrorMessage = "User Name must be atleast 8 characters long", MinimumLength = 8)]
        [Required]
        public string UserName { get; set; }


        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must be atleast 8 character which contain 1 Uppercase character and 1 number")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Type")]
        //public string Type { get; set; }

        //public bool Disabled { get; set; }


        /// <summary>
        /// /////////////////////////////////////////////////
        /// </summary>
        [Key]
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

        [Display(Name ="Group ID")]
        public int GroupID { get; set; }

    }

    public class DuplicateID : ValidationAttribute
    {

        public override bool IsValid(object result)
        {
            Sprint_3_V1.Models.Sprint_3_V1Context db = new Models.Sprint_3_V1Context();

            var checkID = db.Accounts.Where(x => x.UserName == result.ToString()).Select(a => a.UserName).Count();
            bool check;
            check = (checkID == 0) ? true : false;
            return check;

        }
    }
}