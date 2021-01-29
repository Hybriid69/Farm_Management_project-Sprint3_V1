using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint_3_V1.ViewModels
{
    [NotMapped]
    public class CustomerViewModel
    {
        [Key]
        [Display(Name = "CustomerID")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Customer Username must be atleast 8 character which contain 1 Uppercase character and 1 number")]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Type")]
        public string CustType { get; set; }

        [Display(Name = "Multi")]
        public int Multi { get; set; }

        [Required]
        [Display(Name = "Cell Number")]
        [RegularExpression(@"^(\+?27|0)[1-8][0-9]{8}$", ErrorMessage = "Invalid Phone number")]
        [StringLength(12, ErrorMessage = "Number cannot be more than 12 digits")]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }


        [StringLength(30)]
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Postal Address")]
        [StringLength(30)]
        public string Address { get; set; }

        //[Display(Name = "Disabled")]
        //public bool Disabled { get; set; }

        //[Display(Name = "Account ID")]
        //public int AccountID { get; set; }

        //[ForeignKey("AccountID")]
        //public virtual Account Account { get; set; }

        //public virtual ICollection<Cart> Cart { get; set; }

        //public virtual ICollection<Order> Order { get; set; }

        //public virtual ICollection<Sale> Sale { get; set; }
    }
}