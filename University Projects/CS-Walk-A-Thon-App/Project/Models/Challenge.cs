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

    public partial class Challenge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Challenge()
        {
            this.UserChallenges = new HashSet<UserChallenge>();
        }
    
        public int challengeID { get; set; }
        [Required]
        public string challengeName { get; set; }
        [Required]
        public string challengeDescription { get; set; }
        public double challengeDistance { get; set; }
        public System.DateTime challengeTimeStart { get; set; }
        public System.DateTime challengeTimeEnd { get; set; }
        public string challengeGender { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive integer Number")]
        public int challengeAgeLowerBound { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive integer Number")]
        public int challengeAgeUpperBound { get; set; }
        public int achievementID { get; set; }
    
        public virtual Achievement Achievement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
    }
}