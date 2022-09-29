namespace Sign_Up_Form.Models
{
    public class CommentUser
    {
        public CommentUser()
        {
            msg = new Message();
            com = new Comment();
        }
        public Message msg { get; set; }
        public Comment com { get; set; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String imageUrl { get; set; }
    }
}
