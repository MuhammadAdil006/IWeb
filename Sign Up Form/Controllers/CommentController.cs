using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sign_Up_Form.Models;
using Sign_Up_Form.Models.Interfaces;
using Sign_Up_Form.Models.Repository;
using System.Text.Json;

namespace Sign_Up_Form.Controllers
{
    public class CommentController : Controller

    {
        IPostRepository pst;
        IUserRepository usr;
        public CommentController(IUserRepository Ur,IPostRepository Pr)
        {
            pst = Pr;
            usr = Ur;
        }
        [HttpPost]
        public IActionResult AddComment(int SenderId,int PostId,String message)
        {
            Message m = new Message();
            m.SenderId = SenderId;
            m.Msg = message;
            Comment c = new Comment();
            c.PostId= PostId;
            CommentUser user = new CommentUser();
            (user.msg,user.com)= pst.AddComment(m, c);
            String jsonResult = JsonConvert.SerializeObject(user, Formatting.Indented,
                   new JsonSerializerSettings()
                   {
                       ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                   }
               );
            return Json(jsonResult);
        }
        [HttpPost]
        public IActionResult GetAllComments(int postid)
        {
           List<CommentUser> a= pst.getAllComments(postid);
            a = usr.setProfile(a);
            String jsonResult = JsonConvert.SerializeObject(a, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    }
                );
            return Json(jsonResult);
        }
        [HttpPost]
        public IActionResult DeleteComment(int MessageId, int CurrentUserId,int CommentId)
        {
            //convert poster id and currentuserid and commentid to check exist in db
            //posterid and current user id are same
            bool flag = pst.DeleteComment(CurrentUserId, CommentId,MessageId);
            return Json(flag);

        }
        [HttpPost]
        public IActionResult ModifyComment(String message,int messageId)
        {
            //just have to update the message and return true or false
            bool flag = pst.ModifyComment(message, messageId);
            return Json(flag);
        }
    }
}
