//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PointOfInterest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PointOfInterest()
        {
            this.RouteSections = new HashSet<RouteSection>();
        }
    
        public int POI_ID { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string POI_Name { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string POI_Information { get; set; }

        public int POI_PictureID { get; set; }
    
        public virtual Image Image { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteSection> RouteSections { get; set; }
    }
}