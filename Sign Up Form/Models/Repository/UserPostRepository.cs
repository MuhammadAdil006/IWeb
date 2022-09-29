using Sign_Up_Form.Models.Interfaces;

namespace Sign_Up_Form.Models.Repository
{
    public class UserPostRepository:IUserPostRepository
    {
        public UserPost emptyPost(UserPost a)
        {

            a.post.PostCategory = "";
            a.post.PostId = -1;
            a.post.NoOfComments = -1;
            a.post.NoOfLikes = -1;
            a.post.PostMessage = "";
            ; return a;
        }
        public UserPost setAttributes(UserPost a, User u)
        {
            a.user.FirstName = u.firstname;
            a.user.LastName = u.lastname;
            a.user.Email = u.email;
            a.user.Gender = u.gender; return a;
            a.user.JoiningDate = u.joinedDate;
            a.user.DateOfBirth = u.DateOfBirth;
            a.user.About = u.About;
            a.user.Password = u.New_password;
            
        }
        public UserPost setAttributes(UserPost a, UserDatum u)
        {
            a.user.FirstName = u.FirstName;
            a.user.LastName = u.LastName;
            a.user.Email = u.Email;
            a.user.Gender = u.Gender;
            a.user.JoiningDate = (DateTime)u.JoiningDate;
            a.user.DateOfBirth = u.DateOfBirth;
            a.user.About = u.About;
            a.user.ImageUrl = u.ImageUrl;
            a.user.Id = u.Id;
            a.user.IsActive=u.IsActive;
            
            return a;
        }

        public User GetAttributes(User a, UserPost u)
        {
            a.firstname = u.user.FirstName;
            a.lastname = u.user.LastName;
            a.email = u.user.Email;
            a.gender = u.user.Gender;

            a.joinedDate = (DateTime)u.user.JoiningDate;
            a.DateOfBirth = u.user.DateOfBirth;
            a.About = u.user.About;
            a.New_password = u.user.Password;
            return a;
        }

        public Post GetAttributes(Post a, UserPost p)
        {
            a.UserId = p.user.Id;
            a.PostCategory = p.post.PostCategory;
            a.PostId = p.post.PostId;
            a.NoOfComments = p.post.NoOfComments;
            a.NoOfLikes = p.post.NoOfLikes;
            a.PostMessage = p.post.PostMessage;
            a.ImageUrl = p.post.ImageUrl;
            a.PostDate = p.post.PostDate; return a;
        }

        public UserPost setAttributes(UserPost a, Post p)
        {
            a.user.Id = p.UserId;
            a.post.PostCategory = p.PostCategory;
            a.post.PostId = p.PostId;
            a.post.NoOfComments = p.NoOfComments;
            a.post.NoOfLikes = p.NoOfLikes;
            a.post.PostMessage = p.PostMessage;
            a.post.PostDate = p.PostDate; return a;
        }
        public UserPost setAttributes(UserDatum a, Post P)
        {
            UserPost UP = new UserPost();
            UP.post.PostId = P.PostId;
            UP.post.UserId = a.Id;
            UP.post.PostCategory = P.PostCategory;
            UP.user.JoiningDate = (DateTime)a.JoiningDate;
            UP.post.PostDate = P.PostDate;
            UP.post.PostMessage = P.PostMessage;
            UP.post.ImageUrl = P.ImageUrl;
            UP.post.NoOfLikes = P.NoOfLikes;
            UP.post.NoOfComments = P.NoOfComments;
            UP.user.FirstName = a.FirstName;
            UP.user.ImageUrl = a.ImageUrl;
            UP.user.IsActive=a.IsActive;
            UP.user.LastName = a.LastName; return UP;

        }

    }
}
