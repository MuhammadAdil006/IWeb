using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sign_Up_Form.Models.ViewModel
{
    public class SignUpUser:IdentityUser
    {
        [Required(ErrorMessage = "Enter first name")]
        [StringLength(50)]
        public String Firstname { get; set; }
        [Required(ErrorMessage = "Enter Last name")]
        [StringLength(50)]
        public String Lastname { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [StringLength(50)]
        public String Gender { get; set; }
        [Required(ErrorMessage = "Select Date")]

        public DateTime DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? About { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
