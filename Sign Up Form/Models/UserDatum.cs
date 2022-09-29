using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class UserDatum:RecInfo
    {
        public UserDatum()
        {
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? About { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }
        public virtual Login Login { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }
    }
}
