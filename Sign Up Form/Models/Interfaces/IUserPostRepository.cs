namespace Sign_Up_Form.Models.Interfaces
{
    public interface IUserPostRepository
    {
        public UserPost emptyPost(UserPost user);
        public UserPost setAttributes(UserPost user,User u);
        public UserPost setAttributes(UserPost a, UserDatum u);
        public User GetAttributes(User a, UserPost u);
        public Post GetAttributes(Post a, UserPost p);
        public UserPost setAttributes(UserPost a, Post p);
        public UserPost setAttributes(UserDatum a, Post P);
    }
   
}
