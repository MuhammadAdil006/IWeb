using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;

namespace Sign_Up_Form
{
    public partial class Like:RecInfo
    {
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public int Id { get; set; }

        public virtual Post? Post { get; set; }
        public virtual UserDatum? User { get; set; }
    }
}
