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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.DistanceLogs = new HashSet<DistanceLog>();
            this.UserChallenges = new HashSet<UserChallenge>();
            this.UserGroupChallenges = new HashSet<UserGroupChallenge>();
            this.Achievements = new HashSet<Achievement>();
        }

        public int userID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required.")]
        public string firstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Required.")]
        public string surname { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime dateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Required.")]
        public string gender { get; set; }

        [Display(Name = "Year Of Study")]
        [Required(ErrorMessage = "Required.")]
        public int yearOfStudy { get; set; }

        [Display(Name = "Faculty Of Degree")]
        [Required(ErrorMessage = "Required.")]
        public string degreeFaculty { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid.")]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.
        //                    \w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string emailAddress { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Required.")]
        public int profilePictureID { get; set; }

        [Display(Name = "Total Distance Travelled")]
        //[Required(ErrorMessage = "Required.")]
        public double totDistanceTravelled { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Required.")]
        public string userName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Required.")]
        [Compare("password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        [Display(Name = "Type Of User")]
        public string typeOfUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DistanceLog> DistanceLogs { get; set; }
        public virtual Image Image { get; set; }
        public virtual RouteLeaderboard RouteLeaderboard { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserChallenge> UserChallenges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGroupChallenge> UserGroupChallenges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
