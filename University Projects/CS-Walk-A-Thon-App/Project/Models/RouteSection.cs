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

    public partial class RouteSection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteSection()
        {
            this.PointOfInterests = new HashSet<PointOfInterest>();
        }
    
        public int sectionID { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string sectionStart { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string sectionEnd { get; set; }

        [Required(ErrorMessage = "Required.")]
        public double sectionDistance { get; set; }

        public int routeID { get; set; }

        public int POI_ID { get; set; }

        public virtual Route Route { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointOfInterest> PointOfInterests { get; set; }
    }
}
