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
    
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            this.BookTours = new HashSet<BookTour>();
            this.Reviews = new HashSet<Review>();
        }
    
        public int Id { get; set; }
        public string TourName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DaysOfTour { get; set; }
        public string TourSchedule { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int StartingPlaceId { get; set; }
        public int DestinationId { get; set; }
        public int SubCategoryId { get; set; }
        public bool Voucher { get; set; }
        public Nullable<bool> Hot { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookTour> BookTours { get; set; }
        public virtual Destination Destination { get; set; }
        public virtual Destination Destination1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}