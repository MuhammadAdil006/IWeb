using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class Image
    {
        public Image()
        {
            Posts = new HashSet<Post>();
            UserData = new HashSet<UserDatum>();
        }

        public int Id { get; set; }
        public string ImagePath { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserDatum> UserData { get; set; }
    }
}
