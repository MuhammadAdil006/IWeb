using System.ComponentModel.DataAnnotations.Schema;

namespace Sign_Up_Form.Models
{
    public class RecInfo
    {
        [Column(TypeName ="varchar(max)")]
        public String? CreatedBy { get; set; }
        [Column(TypeName = "varchar(max)")]
        public String? ModifiedBy { get; set; }
        [Column(TypeName = "varchar(max)")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(max)")]
        public DateTime? ModifiedDate { get; set; }
    }
}
