using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Sign_Up_Form.Models
{
    public class UserPost
    {
        public UserDatum user;
        public Post post;
        public  UserPost()
        {
            user = new UserDatum();
            post = new Post();
        }

       
        public int AlertCount { get; set; }//it will display the alert count new  notifications
        public IFormFile? PostPicture { get; set; }
        public IFormFile? UserProfileImage { get; set; }
        public bool IsLikedByCurUser { get; set; } = false;
        public int? offset { get; set; }
       
    }
}
