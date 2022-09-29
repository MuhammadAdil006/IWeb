using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server;
using Newtonsoft.Json;
using Sign_Up_Form.Models;
using Sign_Up_Form.Models.Interfaces;
using Sign_Up_Form.Models.Repository;

namespace Sign_Up_Form.Controllers
{
    public class PostController : Controller
    {
        IUserRepository usrmng;
        IPostRepository pstmng;
        IUserPostRepository usrpstmng;
        private INotificationRepository notificationRepository;
        private readonly IWebHostEnvironment _env;//for path

        public PostController(IWebHostEnvironment env,IUserRepository Ur, IPostRepository Pr, IUserPostRepository UPr, INotificationRepository notificationRepository)
        {
            usrmng = Ur;
            pstmng = Pr;
            usrpstmng = UPr;
            _env = env;
            this.notificationRepository = notificationRepository;
        }
        [HttpPost]
        public ActionResult CreatePost(Post u,IFormFile image)
        {
            
            //u = usrpstmng.setAttributes(u, temp);
           
                u.NoOfComments = 0;
                u.NoOfLikes = 0;
                u.PostDate = System.DateTime.Now;
                Console.WriteLine("Gotcha");
            String filename = "";
            if (image != null)
            {
                filename = "Images/" + Guid.NewGuid().ToString() + image.FileName;
                String Serverfilename = Path.Combine(_env.WebRootPath, filename);
                image.CopyTo(new FileStream(Serverfilename, FileMode.Create));
            }
            u.ImageUrl = filename;

            pstmng.CreatePost(u);
            //also get this id
            return Json(new { message = "success" });
        }
        [HttpPost]
        public IActionResult AddLike(Like l)
        {
            pstmng.AddLikeToDb(l);
            //add +1 to no of likes to that post
            pstmng.addIncToLike(l);
            return Json(new { message = "success" });
        }

        [HttpPost]
        public IActionResult removeLike(Like l)
        {
            pstmng.RemoveLikeFromDb(l);
            pstmng.addDecToLike(l);
            return Json(new { message = "success" });
        }

        [HttpPost]
        public IActionResult DeletePost(int PostId)
        {
            if (pstmng.DeletePost(PostId))
            {
                return Json(true);
            }
            return Json(false);
        }
        //working on this func
        [HttpPost]
        public IActionResult FilterPosts(String phrase, bool General,bool Sports,bool Gaming,bool Hiking,int UserId)
        {
            UserPost u = new UserPost();

            UserDatum d = new UserDatum();
            d = usrmng.GetUser(UserId);
            u = usrpstmng.setAttributes(u, d);
            //getting the alert counts
            u.AlertCount = notificationRepository.AlertCounting(UserId);
            List<UserPost> usp = new List<UserPost>();
            //also add 20 blogs posts as in the search
            List<Post> blogsPosts = pstmng.SearchTwentyPosts(phrase, General,Sports,Gaming,Hiking);//it will get first 10 posts
                                                         //get the first name and lastname and also profile image
            List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
            List<UserPost> uspo = new List<UserPost>();
            for (int i = 0; i < Userdata.Count; i++)
            {
                UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);
                uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, u.user.Id);

                uspo.Add(uspp);
            }
            //no need to add current user

            if(uspo.Count > 0)
            {
                String jsonResult = JsonConvert.SerializeObject(uspo, Formatting.Indented,
                   new JsonSerializerSettings()
                   {
                       ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                   }
               );
                return Json(jsonResult);
            }
            else
            {
                String jsonResult = JsonConvert.SerializeObject("not found", Formatting.Indented,
                   new JsonSerializerSettings()
                   {
                       ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                   }
               );
                return Json(jsonResult);
            }
            
            
        }

    }
}
