using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HARTravel.Models
{
    public class RegisterVM
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password), MinLength(3), MaxLength(20)]
        public string Password { get; set; }

        [DataType(DataType.Password), MinLength(3), MaxLength(20)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}