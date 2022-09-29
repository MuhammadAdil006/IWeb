using System.ComponentModel.DataAnnotations;
namespace Sign_Up_Form.Models
{
    public class User
    {
        //Model validations
        [Required (ErrorMessage ="Enter first name")]
        [StringLength(50)]
        public String firstname { get; set; }
        [Required(ErrorMessage = "Enter Last name")]
        [StringLength(50)]
        public String lastname { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [StringLength(50)]
        public String email { get; set; }
        [Required(ErrorMessage = "Enter Password name")]
        [StringLength(50)]
        public String New_password { get; set; }
        [Required (ErrorMessage ="Select Gender")]

        public String gender { get; set; }
        [Required(ErrorMessage ="Select Date")]
        public int day { get; set; }
        [Required(ErrorMessage = "Select Month")]
        public int month { get; set; }
        [Required(ErrorMessage = "Select year")]
        public int year { get; set; }
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTime joinedDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? About { get;  set; }
      

        public User()
        {
            firstname = "";
            lastname = "";
            year = 1950;
            id = -1;
            day = 1;
            month = 1;
            gender = "";
            New_password = "";
            email = "";
        }


    }
}
