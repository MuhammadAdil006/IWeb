using System;
using System.Collections;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Sign_Up_Form.Models.Interfaces;
using Sign_Up_Form.Models.ViewModel;

namespace Sign_Up_Form.Models.Repository
{
    public class UserRepository:IUserRepository
    {
        ShareYourRoutineContext context;
        private readonly UserManager<SignUpUser> _identityuserManager;
<<<<<<< HEAD
        private readonly SignInManager<SignUpUser> _signInManager;
        public UserRepository(UserManager<SignUpUser> identityuserManager, SignInManager<SignUpUser> signInManager)
        {
            context = new ShareYourRoutineContext();
            _identityuserManager = identityuserManager;
            _signInManager = signInManager;
=======
        public UserRepository(UserManager<SignUpUser> identityuserManager)
        {
            context = new ShareYourRoutineContext();
            _identityuserManager = identityuserManager;

>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e
        }
        public String getPassword(int id)
        {
            UserDatum d = context.UserData.Where(x => x.Id == id).FirstOrDefault();
            return d.Password;
        }
        public void ActiveOrDeactiveUser(int Userid, bool isActive, String admin)
        {
            UserDatum userDatum = context.UserData.Where(x => x.Id == Userid).FirstOrDefault();
            userDatum.IsActive = !isActive;
            userDatum.ModifiedBy = admin;
            userDatum.ModifiedDate=DateTime.Now;
            context.SaveChanges();
        }
        public UserDatum SetAuditColumnsFirstTime(UserDatum a)
        {
            a.CreatedDate = DateTime.Now;
            a.CreatedBy = a.Email;
            return a;
        }
        public UserDatum SetAuditColumnsSecondTime(UserDatum a)
        {
            a.ModifiedDate = DateTime.Now;
            a.ModifiedBy = a.Email;
            return a;
        } public Login SetAuditColumnsSecondTime(Login a,String email)
        {
            a.ModifiedDate = DateTime.Now;
            a.ModifiedBy = email;
            return a;
        }
        public List<CommentUser> setProfile(List<CommentUser> a)
        {
            string s;
            foreach (CommentUser user in a)
            {
                UserDatum b = context.UserData.Where(x => x.Id == user.msg.SenderId).FirstOrDefault();
                user.firstname = b.FirstName;
                user.lastname = b.LastName;
                user.imageUrl = b.ImageUrl;

            }
            return a;
        }
        //updating credentials or other user information
        public UserPost updateCredentials(UserPost u, string passwo)
        {
            Login l = new Login();
            l = SetAuditColumnsSecondTime(l, u.user.Email);
            l = context.Logins.Where(s => s.Id == u.user.Id).FirstOrDefault();
            l.Password = passwo;
            context.SaveChanges();
            UserDatum userr = new UserDatum();
            userr = context.UserData.Where(s => s.Id == u.user.Id).FirstOrDefault();
            userr.FirstName = u.user.FirstName;
            userr.LastName = u.user.LastName;
            userr.About = u.user.About;
            userr.Password = passwo;
            userr.ImageUrl = u.user.ImageUrl;
            userr = SetAuditColumnsSecondTime(userr);
            u.user = userr;
            context.SaveChanges();
            u.user = userr;
            return u;
        }
        //getting password from login
        public string Getpassowrd(int id)
        {
            Login a = new Login();
            a = context.Logins.Where(s => s.Id == id).FirstOrDefault();
            return a.Password;
        }
        //passing data from user model to userdatum
<<<<<<< HEAD
        public UserDatum Equalling(RegisterUser a)
        {
            UserDatum c = new UserDatum();
            //c.Id = a.id;
            c.FirstName = a.firstname;
            c.LastName = a.lastname;
            c.Password = a.Password;
            c.Email = a.Email;
            c.Gender = a.gender;
            c.DateOfBirth = Convert.ToDateTime(MakeDate(a));
            c.JoiningDate = a.joinedDate;
=======
        //public UserDatum Equalling(User a)
        //{
        //    UserDatum c = new UserDatum();
        //    c.Id = a.id;
        //    c.FirstName = a.firstname;
        //    c.LastName = a.lastname;
        //    c.Password = a.New_password;
        //    c.Email = a.email;
        //    c.Gender = a.gender;
        //    c.DateOfBirth = Convert.ToDateTime(MakeDate(a));
        //    c.JoiningDate = a.joinedDate;
>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e

        //    return c;

        //}
        //checking login credentials
        public int CheckCredentials(User u)
        {

            Login b = new Login();
            b = context.Logins.Where(p => p.Email == u.email).Where(p => p.Password == u.New_password).Select(c => c).FirstOrDefault();

            if (b == null)
                return -1;
            return b.Id;
        }  public int CheckCredentials(RegisterUser u)
        {

            Login b = new Login();
            b = context.Logins.Where(p => p.Email == u.Email).Where(p => p.Password == u.Password).Select(c => c).FirstOrDefault();

            if (b == null)
                return -1;
            return b.Id;
        }
        //it will convert the date from string to date type
        public string MakeDate(/*User a*/RegisterUser a)
        {
            string date = Convert.ToString(a.year);
            date = date + "-" + Convert.ToString(a.month) + "-" + Convert.ToString(a.day);

            return date;
        }
        public UserDatum GetUser(int id)
        {
            User user = new User();
            UserDatum d = context.UserData.Where(e => e.Id == id).FirstOrDefault();

            return d;
        }
        public List<UserDatum> GetpostUserData(List<Post> p)
        {
            List<UserDatum> us = new List<UserDatum>();
            foreach (Post pos in p)
            {
                UserDatum? n = context.UserData.Where(x => x.Id == pos.UserId).FirstOrDefault();
                //empty the password 
                n.Password = "";
                us.Add(n);
            }
            return us;
        }

        //get all user emails and user name and map them return as a dictionary
        public Dictionary<string, string> GetallEmailsAndUserName()
        {
            Dictionary<string, string> email = new Dictionary<string, string>();

            var d = context.UserData.Select(e => new { e.Email, e.FirstName, e.LastName }).ToList();
            foreach (var e in d)
            {
                email.Add(e.Email, e.FirstName + " " + e.LastName);
            }
            return email;



        }
        //getting id formed by db
        public int getIdFromDb(User a)
        {
            UserDatum d = new UserDatum();
            d = context.UserData.Where(e => e.Id == a.id).FirstOrDefault();

            return d.Id;


        }

        //adding credentionals to login table
        public void addLoginDetails(UserDatum a)
        {
            Login l = new Login();
            l.Email = a.Email;
            l.Password = a.Password;
            l.Id = a.Id;
            l.CreatedDate = DateTime.Now;
            l.CreatedBy = l.Email;
            l.UserType = "User";
            context.Logins.Add(l);

        }
        //this function will check if the id is admin's id or user id
        public bool CheckAdmin(int id)
        {
            Login a = context.Logins.Where(x => x.Id == id).FirstOrDefault();
            if (a.UserType == "Admin") return true;
            else
                return false;
        }
        //this function will get all the users andplace admin at last

        public List<UserDatum> GetAllUsers(int AdminId,int x)
        {
            List<UserDatum> users = context.UserData.Where(x=>x.Id!=AdminId).Take(30).Skip(x).ToList();
            users.Add(GetUser(AdminId));//admin at the last

            return users;
        }
        //it will convert all credentials to lowercase except password so comparing doesnt matter
        public RegisterUser ConvertoLowercase(RegisterUser a)
        {
            a.Email = a.Email.ToLower();
            a.gender = a.gender.ToLower();
            return a;
        }
        public bool compareEmail(User a)
        {
            UserDatum d = new UserDatum();
            d = context.UserData.Where(e => e.Email == a.email).FirstOrDefault();

            if (d != null)
            {

                return true;
            }


            return false;
        }
        public async Task<IdentityResult> addUser(/*User a*/RegisterUser u)
        {
            //check if email exists if exits then return false esle return true
            //add  to db
            //MAKING DATE
            //a.joinedDate = DateTime.Now;
            ////converting to lowercase
<<<<<<< HEAD
            u = ConvertoLowercase(u);
            
                //String date = MakeDate(a);
                UserDatum b = Equalling(u);
                if (b.Gender == "male")
                {
                    b.ImageUrl = "Images/maleImage.png";
                }
                else if (b.Gender == "female")
                {
                    b.ImageUrl = "Images/FemalImage.png";
                }

                b.Id = 0;
                b = SetAuditColumnsFirstTime(b);
                b.IsActive = true;
                context.UserData.Add(b);
                context.SaveChanges();
                UserDatum c = context.UserData.Where(e => e.Email == b.Email).Where(e => e.Password == b.Password).Select(e => e).FirstOrDefault();

                addLoginDetails(c);
                context.SaveChanges();
                var user = new SignUpUser()
                {
                    Email = u.Email,
                    UserName = u.Email
               ,
                    Firstname = u.firstname,
                    Lastname = u.lastname,
                    DateOfBirth = u.DateOfBirth,
                    JoiningDate = u.joinedDate,
                    Gender = u.gender
               ,
                    IsActive = true
                };

                var results = await _identityuserManager.CreateAsync(user, u.Password);
                return results;
            
           

           
        }

        public async Task<SignInResult> SigningIn(RegisterUser u)
        {
           var results=await _signInManager.PasswordSignInAsync(u.Email, u.Password, true, false);
            return results;
        }

        public User ConvertoLowercase(User a)
        {
            throw new NotImplementedException();
=======
            //a = ConvertoLowercase(a);
            //if (!compareEmail(a))
            //{
            //    //String date = MakeDate(a);
            //    UserDatum b = Equalling(a);
            //    if(b.Gender=="male")
            //    {
            //        b.ImageUrl = "Images/maleImage.png";
            //    }
            //    else if(b.Gender=="female")
            //    {
            //        b.ImageUrl = "Images/FemalImage.png";
            //    }

            //    b.Id = 0;
            //    b = SetAuditColumnsFirstTime(b);
            //    b.IsActive = true;
            //    context.UserData.Add(b);
            //    context.SaveChanges();
            //    UserDatum c = context.UserData.Where(e => e.Email == a.email).Where(e => e.Password == a.New_password).Select(e => e).FirstOrDefault();

            //    addLoginDetails(c);
            //    context.SaveChanges();
            //    return true;
            //}
            //return false;

            var user = new SignUpUser() { Email=u.Email,UserName=u.Email
               ,Firstname=u.firstname,Lastname=u.lastname,DateOfBirth=u.DateOfBirth,JoiningDate=u.joinedDate,Gender=u.gender
               ,IsActive=true
            };

            var results = await _identityuserManager.CreateAsync(user, u.Password);
            return results;
>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e
        }
    }
}
