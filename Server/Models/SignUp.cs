using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Server.Models
{
    public class SignUp
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
