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

    public partial class Achievement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Achievement()
        {
            this.Challenges = new HashSet<Challenge>();
            this.GroupChallenges = new HashSet<GroupChallenge>();
            this.Routes = new HashSet<Route>();
            this.Users = new HashSet<User>();
        }
    
        public int achievementID { get; set; }

        [Display(Name = "Achievement Name")]
        [Required(ErrorMessage = "Required.")]
        public string achievementName { get; set; }

        [Display(Name = "Achievement Description")]
        [Required(ErrorMessage = "Required.")]
        public string achievementDescription { get; set; }

        [Display(Name = "Achievement Difficulty")]
        [Required(ErrorMessage = "Required.")]
        public string achievementDifficulty { get; set; }

        [Display(Name = "Achievement Category")]
        [Required(ErrorMessage = "Required.")]
        public string achievementCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Challenge> Challenges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupChallenge> GroupChallenges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Route> Routes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
