using Microsoft.AspNetCore.Identity;
using Sign_Up_Form.Models.ViewModel;

namespace Sign_Up_Form.Models.Interfaces
{
    public interface IUserRepository
    {
        public List<CommentUser> setProfile(List<CommentUser> a);
        public UserPost updateCredentials(UserPost u, string passwo);
        public string Getpassowrd(int id);
        public int CheckCredentials(User u);
<<<<<<< HEAD
        public int CheckCredentials(RegisterUser u);
        public RegisterUser ConvertoLowercase(RegisterUser a);
=======
>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e
        public string MakeDate(/*User a*/RegisterUser a);
        public UserDatum GetUser(int id);
        public List<UserDatum> GetpostUserData(List<Post> p);
        public Dictionary<string, string> GetallEmailsAndUserName();
        public int getIdFromDb(User a);
        public void addLoginDetails(UserDatum a);
        public User ConvertoLowercase(User a);
        public bool compareEmail(User a);
        //public bool addUser(User a);
        public  Task<IdentityResult> addUser(/*User a*/RegisterUser u);
<<<<<<< HEAD
        public UserDatum Equalling(RegisterUser a);
=======
        //public UserDatum Equalling(User a);
>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e
        public bool CheckAdmin(int id);
        public List<UserDatum> GetAllUsers(int AdminId, int x);
        public void ActiveOrDeactiveUser(int Userid,bool isActive,String admin);
        public  Task<SignInResult> SigningIn(RegisterUser u);
    }
}
