using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Net.Mail;

namespace Sprint_3_V1.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

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

        [NotMapped]
        public string Role { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }

        public static void BuildEmailTemplate(string sendto, string status)
        {
            string from, to, bcc, cc, subject, body;
            from = "Lorenzomunsami2@gmail.com";
            to = sendto.Trim();
            bcc = "";
            cc = "";
            subject = "Welcome Customer";
            StringBuilder sb = new StringBuilder();
            sb.Append("You Have Been Registered As A Customer For farm fresh produce." + status);
            body = sb.ToString();
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 25;
            client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(" Lorenzomunsami2@gmail.com", "Lorry@123");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

