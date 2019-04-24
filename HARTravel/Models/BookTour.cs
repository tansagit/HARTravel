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
    
    public partial class BookTour
    {
        public int Id { get; set; }
        public int Numbers { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public int StatusId { get; set; }
        public int TourId { get; set; }
        public int EmpId { get; set; }
        public int CusId { get; set; }
        public string Voucher { get; set; }
        public System.DateTime DayStart { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
