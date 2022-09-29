using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class Post:RecInfo
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Notifications = new HashSet<Notification>();
        }

        public int PostId { get; set; }
        public int UserId { get; set; }
        public int NoOfComments { get; set; }
        public int NoOfLikes { get; set; }
        public string PostCategory { get; set; } = null!;
        public string PostMessage { get; set; } = null!;
        public DateTime PostDate { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
