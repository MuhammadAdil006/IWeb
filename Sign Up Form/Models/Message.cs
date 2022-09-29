using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class Message:RecInfo
    {
        public Message()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string? Msg { get; set; }
        public int? SenderId { get; set; }
        public int? RecieverId { get; set; }

        public virtual Login? Reciever { get; set; }
        public virtual Login? Sender { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
