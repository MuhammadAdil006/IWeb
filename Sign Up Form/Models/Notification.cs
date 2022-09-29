using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class Notification:RecInfo
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int NotificationType { get; set; }
        public bool Ack { get; set; }
        public int GeneratorUserId { get; set; }
        public DateTime GeneratorTime { get; set; }
        public bool Opened { get; set; }

        public virtual Login GeneratorUser { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual Login User { get; set; } = null!;
    }
}
