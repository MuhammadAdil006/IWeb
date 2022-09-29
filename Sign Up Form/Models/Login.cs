using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class Login:RecInfo
    {
        public Login()
        {
            MessageRecievers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            NotificationGeneratorUsers = new HashSet<Notification>();
            NotificationUsers = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public String UserType { get; set; } = null;
        public virtual UserDatum IdNavigation { get; set; } = null!;
        public virtual ICollection<Message> MessageRecievers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Notification> NotificationGeneratorUsers { get; set; }
        public virtual ICollection<Notification> NotificationUsers { get; set; }
    }
}
