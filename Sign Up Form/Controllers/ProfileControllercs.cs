using Microsoft.AspNetCore.Mvc;
using Sign_Up_Form.Models;
using Sign_Up_Form.Models.Interfaces;
using Sign_Up_Form.Models.Repository;

namespace Sign_Up_Form.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _env;//for path
        IUserPostRepository usrpstmng;
        IUserRepository usrmng;
        public ProfileController(IWebHostEnvironment env,IUserPostRepository UPr,IUserRepository Ur)
        {
            usrpstmng = UPr;
            usrmng = Ur;
            _env = env;

        }
        public IActionResult EditProfile(int UserId)
        {
            UserPost u = new UserPost();
            UserDatum a = new UserDatum();
               a = usrmng.GetUser(UserId);
          
            u = usrpstmng.setAttributes(u, a);
            ViewData["Title"] = "EditProfile";
            ViewBag.passwordmatched = 0;
            return View(u);
        }
        public ViewResult update(IFormFile UserProfileImage,String FirstName,int userId,String LastName,String About,String userPassword,String gender, String new_password,String imageUrl)
        {
            UserPost u = new UserPost();
            u.UserProfileImage = UserProfileImage;
            u.user.FirstName=FirstName;
            u.user.LastName=LastName;
            u.user.Id=userId;
            u.user.About = About;
           
            u.user.Password = userPassword;
            u.user.Gender = gender;
            u.user.ImageUrl = imageUrl;

            String password = usrmng.Getpassowrd(u.user.Id);
            if(password == u.user.Password)
            {
                String filename = "";
                if (u.UserProfileImage != null )
                {
                    filename = "Images/" + Guid.NewGuid().ToString() + u.UserProfileImage.FileName;
                    String Serverfilename = Path.Combine(_env.WebRootPath, filename);
                    u.UserProfileImage.CopyTo(new FileStream(Serverfilename, FileMode.Create));
                    u.user.ImageUrl = filename;
                }
               
                //can update
                 u= usrmng.updateCredentials(u,new_password);
                u.user.Password = "";
                ViewBag.passwordmatched = 1;
                return (View("EditProfile", u));
            }
            else
            {
                //can't update
                ViewBag.passwordmatched = 2;
            }
            u.user.Password = "";
            UserDatum temp = usrmng.GetUser(u.user.Id);
            u = usrpstmng.setAttributes(u, temp);

            return View("EditProfile",u);
        }
    }
}
