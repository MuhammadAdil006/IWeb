namespace Sign_Up_Form.Models.ViewModel
{
    public class UserWithLessDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
