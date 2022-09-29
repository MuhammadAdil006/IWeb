using Microsoft.Data.SqlClient;
using Sign_Up_Form.Models.Interfaces;

namespace Sign_Up_Form.Models.Repository
{
    public class PostRepository:IPostRepository
    {
        ShareYourRoutineContext context;
        NotificationRepository notificationRepository;
        public PostRepository()
        {
            context = new ShareYourRoutineContext();
            notificationRepository = new NotificationRepository();
        }
        public String GetEmailOfUser(int userid)
        {
            UserDatum a = context.UserData.Where(x => x.Id == userid).FirstOrDefault();
            if (a == null)
                return "";
            return a.Email;
        }
        public Message SetAuditColumnFirstTime(Message a,String email)
        {
            a.CreatedBy = email;
            a.CreatedDate = DateTime.Now;
            return a;
        }  public Like SetAuditColumnFirstTime(Like a,String email)
        {
            a.CreatedBy = email;
            a.CreatedDate = DateTime.Now;
            return a;
        } 
        public Post SetAuditColumnFirstTime(Post a,String email)
        {
            a.CreatedBy = email;
            a.CreatedDate = DateTime.Now;
            return a;
        } 
        public Post SetAuditColumnSecondTime(Post a,String email)
        {
            a.ModifiedBy = email;
            a.ModifiedBy = DateTime.Now.ToString();
            return a;
        } public Message SetAuditColumnSecondTime(Message a,String email)
        {
            a.ModifiedBy = email;
            a.ModifiedBy = DateTime.Now.ToString();
            return a;
        } 
        public Comment SetAuditColumnFirstTime(Comment a,String email)
        {
            a.CreatedBy = email;
            a.CreatedDate = DateTime.Now;
            return a;
        }
        public string MakeDate()
        {

            string date = DateTime.Now.Year.ToString();
            date = date + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();

            return date;
        }
        //this function will get all messages related to that post
        public List<CommentUser> getAllComments(int postid)
        {
            //first get all list of comments
            var Com = context.Comments.Where(x => x.PostId == postid).ToList();
            List<CommentUser> mes = new List<CommentUser>();
            foreach (Comment c in Com)
            {
                CommentUser user = new CommentUser();

                user.com = c;
                user.msg = context.Messages.Where(x => x.Id == c.MsgId && x.RecieverId == null).FirstOrDefault();
                mes.Add(user);

            }
            return mes;

        }
        //this funcition will get the post id and icremente in it
        public void incComment(int id,String email)
        {
            
            Post p = context.Posts.Where(x => x.PostId == id).FirstOrDefault();
            p = SetAuditColumnSecondTime(p,email);
            p.NoOfComments = p.NoOfComments + 1;
            context.SaveChanges();

        }
        //this funcition will get the post id and decrement in it
        public void decComment(int id)
        {
            Post p = context.Posts.Where(x => x.PostId == id).FirstOrDefault();
            p.NoOfComments = p.NoOfComments - 1;
            context.SaveChanges();

        }
        //this sfunction will add comment
        public (Message, Comment) AddComment(Message m, Comment c)
        {
            //also add inc to no of comment
            String email = GetEmailOfUser(m.SenderId??0);
            m = SetAuditColumnFirstTime(m, email);
            context.Messages.Add(m);
            context.SaveChanges();
            c.MsgId = m.Id;
            c = SetAuditColumnFirstTime(c, email);
            context.Comments.Add(c);
            incComment(c.PostId,email);
            context.SaveChanges();
            notificationRepository.AddCommentToNotification(c, m,email);
            return (m, c);
        }
        //this function will store the newly created post information into posttable
        public void CreatePost(Post p)
        {
            //p.PostDate = Convert.ToDateTime(this.MakeDate());
            String email = GetEmailOfUser(p.UserId);
            p = SetAuditColumnFirstTime(p, email);
            context.Posts.Add(p);
            context.SaveChanges();



        }

        
        //This function will return filtered posts
        public List<Post> SearchTwentyPosts(String phrase,bool General, bool Sports, bool Gaming, bool Hiking)
        {
            List<Post> posts=new List<Post>();
            List<String> categories = new List<string>();
            if (Sports)
                categories.Add("Sports");
            if (Hiking)
                categories.Add("Hiking");
          
            if (Gaming) categories.Add("Gaming");
            if (General) categories.Add("General");
            
                 posts = context.Posts.Where(x => x.PostMessage.Contains(phrase) && categories.Contains(x.PostCategory)).Take(20).ToList();

            return posts;
        }
        //this function will get first posts to be displayed when logged int
        public List<Post> getTenPosts()
        {
            var posts = context.Posts.Take(10).ToList();
            return posts;
        }
        //this funciton will only return those posts which have adventure category
        public List<Post> getAdventouroursPosts()
        {
            var posts = context.Posts.Where(X => X.PostCategory == "Hiking").Take(10).ToList();
            return posts;
        }
        //this funciton will only return those posts which have gaming category
        public List<Post> getGamingPosts()
        {
            var posts = context.Posts.Where(X => X.PostCategory == "Gaming").Take(10).ToList();
            return posts;
        }
        //this funciton will only return those posts which have sports category
        public List<Post> getSportsPosts()
        {
            var posts = context.Posts.Where(X => X.PostCategory == "Sports").Take(10).ToList();
            return posts;
        }
        public void AddLikeToDb(Like l)
        {
            String email = GetEmailOfUser(l.UserId??0);
            l = SetAuditColumnFirstTime(l,email);
            context.Likes.Add(l);
            //adding like notification to that table
            notificationRepository.AddLikeToNotification(l);
            context.SaveChanges();
            //change the no of likes in post also
        }
        public void RemoveLikeFromDb(Like l)
        {
            //first get then remove 
            Like k = context.Likes.Where(x => x.PostId == l.PostId
            && x.UserId == l.UserId).FirstOrDefault();
            context.Likes.Remove(k);
            context.SaveChanges();
            //change the no of likes in post also
        }

        //this function will check if the user has liked it or not
        public bool IsLiked(UserPost a, int id)
        {
            if (context.Likes.Any(x => x.UserId == id && x.PostId == a.post.PostId))
                return true;
            return false;
        }
        public void addIncToLike(Like l)
        {
            Post p = context.Posts.Where(x => x.PostId == l.PostId).FirstOrDefault();
            String email = GetEmailOfUser(l.UserId??0);
            p = SetAuditColumnSecondTime(p, email);
            p.NoOfLikes = p.NoOfLikes + 1;
            context.SaveChanges();
        }
        public void addDecToLike(Like l)
        {
            Post p = context.Posts.Where(x => x.PostId == l.PostId).FirstOrDefault();
            if (p.NoOfLikes != 0)
            {
                String email = GetEmailOfUser(l.UserId ?? 0);
                p = SetAuditColumnSecondTime(p, email);
                p.NoOfLikes = p.NoOfLikes - 1;
            }

            context.SaveChanges();
        }

        public bool DeleteComment(int currentUserId, int CommentId, int MessageId)
        {
            try
            {
                Comment c = context.Comments.Where(x => x.Id == CommentId && x.MsgId == MessageId).FirstOrDefault();
                int Postid = c.PostId;
                context.Comments.Remove(c);
                Message m = context.Messages.Where(x => x.Id == MessageId).FirstOrDefault();
                context.Messages.Remove(m);
                //get userid and postid to delete the notification also and decrement the no of comments
                notificationRepository.deleteNotification(currentUserId, Postid, 2);
                //decrement the no of comments from post
                Post p = context.Posts.Where(x => x.PostId == Postid).FirstOrDefault();
                if (p.NoOfComments > 0)
                {
                    p.NoOfComments = p.NoOfComments - 1;
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }
        public bool ModifyComment(string message, int messageId)
        {
            try
            {
                Message m = context.Messages.Where(x => x.Id == messageId).FirstOrDefault();
                String email = GetEmailOfUser(m.SenderId??0);
                m = SetAuditColumnSecondTime(m, email);
                m.Msg = message;
                //when date is added change date also
                //incomplete
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeletePost(int PostId)
        {

            if (DeleteAllLikesForThatPost(PostId))
            {
                if (DeleteAllCommentsForThatPost(PostId))
                {
                    if (DeleteAllNotificationsForThatPost(PostId))
                    {
                        try
                        {
                            Post p = context.Posts.Where(x => x.PostId == PostId).FirstOrDefault();
                            context.Posts.Remove(p);
                            context.SaveChanges();
                            return true;
                        }
                        catch (Exception e)
                        {
                            return false;
                        }
                    }

                }
                return false;

            }
            return false;


        }
        public bool DeleteAllNotificationsForThatPost(int PostId)
        {
            try
            {
                List<Notification> n = context.Notifications.Where(x => x.PostId == PostId).ToList();
                foreach (Notification a in n)
                {
                    context.Notifications.Remove(a);

                }
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool DeleteAllLikesForThatPost(int PostId)
        {
            try
            {
                var k = context.Likes.Where(x => x.PostId == PostId).FirstOrDefault();
                List<Like> l = context.Likes.Where(x => x.PostId == PostId).ToList();
                foreach (Like a in l)
                {
                    context.Likes.Remove(a);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteAllCommentsForThatPost(int PostId)
        {
            try
            {
                List<Comment> c = context.Comments.Where(x => x.PostId == PostId).ToList();
                foreach (Comment a in c)
                {
                    //delete the message also
                    Message m = context.Messages.Where(x => x.Id == a.MsgId).FirstOrDefault();
                    context.Comments.Remove(a);
                    context.SaveChanges();
                    context.Messages.Remove(m);


                }
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
