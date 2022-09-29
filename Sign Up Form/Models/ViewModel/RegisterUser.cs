using System.ComponentModel.DataAnnotations;

namespace Sign_Up_Form.Models.ViewModel
{
    public class RegisterUser
    {
        [Required(ErrorMessage="Please enter your email")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Please Enter valid Email address")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Please enter Strong Password")]
        [Compare("ConfirmPassword",ErrorMessage ="Password does not match")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm Password")]
       
        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        [StringLength(50)]
        public String firstname { get; set; }
        [Required(ErrorMessage = "Enter Last name")]
        [StringLength(50)]
        public String lastname { get; set; }
        public String gender { get; set; }
        [Required(ErrorMessage = "Select Date")]
        public int day { get; set; }
        [Required(ErrorMessage = "Select Month")]
        public int month { get; set; }
        [Required(ErrorMessage = "Select year")]
        public int year { get; set; }
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTime joinedDate { get; set; }
        public DateTime DateOfBirth { get; set; }
     

    }
}
