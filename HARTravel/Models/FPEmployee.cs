//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HARTravel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FPEmployee
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
        public System.DateTime TimeEnd { get; set; }
        public int EmpId { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
