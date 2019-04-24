using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HARTravel.Models.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string EmpName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public System.DateTime Birthday { get; set; }
        public string IdentityCard { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}