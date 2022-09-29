using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class Comment:RecInfo
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int MsgId { get; set; }

        public virtual Message Msg { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
