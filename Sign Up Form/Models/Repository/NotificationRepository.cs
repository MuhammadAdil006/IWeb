using Sign_Up_Form.Models.Interfaces;
using System;

namespace Sign_Up_Form.Models.Repository
{
    public class NotificationRepository:INotificationRepository
    {
        ShareYourRoutineContext context;
        public NotificationRepository()
        {
            context = new ShareYourRoutineContext();
        }
        public String GetEmailOfUser(int userid)
        {
            UserDatum a = context.UserData.Where(x => x.Id == userid).FirstOrDefault();
            if (a == null)
                return "";
            return a.Email;
        }
        public Notification setAuditFirstTime(Notification n,String email)
        {
            n.CreatedBy = email;
            n.CreatedDate = DateTime.Now;
            return n;
        }
        public Like setAuditFirstTime(Like n,String email)
        {
            n.CreatedBy = email;
            //n.CreatedDate = DateTime.Now;
            return n;
        }
        public void AddLikeToNotification(Like l)
        {
            Notification notification = new Notification();
            notification.PostId = (int)l.PostId;
            notification.GeneratorUserId = (int)l.UserId;
            notification.Ack = false;//because newly generated so 0 means unread
            notification.NotificationType = 1;//1 means like notificaton type
            notification.Opened = false;
            String email = GetEmailOfUser(notification.GeneratorUserId);
            notification = setAuditFirstTime(notification, email);
            Post p = context.Posts.Where(x => x.PostId == l.PostId).FirstOrDefault();
            notification.UserId = p.UserId;
            notification.GeneratorTime = DateTime.Now;
            context.Notifications.Add(notification);
            context.SaveChanges();

        }

        public void AddCommentToNotification(Comment C, Message M,String email)
        {
            Notification notification = new Notification();
            notification.PostId = C.PostId;
            notification.GeneratorUserId = (int)M.SenderId;
            notification.GeneratorTime = DateTime.Now;
            notification.Ack = false;
            notification.Opened = false;
            notification = setAuditFirstTime(notification,email);
            notification.NotificationType = 2;//2 means comment type notification
            Post p = context.Posts.Where(x => x.PostId == C.PostId).FirstOrDefault();
            notification.UserId = p.UserId;
            context.Notifications.Add(notification);
            context.SaveChanges();

        }
        //this funciton will get all unread notifications and change ack to 1 means now read
        public List<UserNotification> GetTenTodaySNotifications(int UserId)
        {
            List<UserNotification> l = new List<UserNotification>();
            List<Notification> list = context.Notifications.Where(x => x.UserId == UserId && x.GeneratorTime >= DateTime.Now.AddDays(-1)).OrderBy(x => x.GeneratorTime).Take(10).ToList();//getting all 10 notificaitons todays
            foreach (Notification notification in list)
            {
                notification.Ack = true;
                //changing it to 1 means these notification are displayed
                UserNotification n = new UserNotification();
                n.notification = notification;
                l.Add(n);

            }
            context.SaveChanges();
            return l;
        }
        //this function will get 10 read notifications considering data and time 
        public List<UserNotification> getTenPastNotifications(int UserId)
        {
            List<Notification> list = context.Notifications.Where(x => x.UserId == UserId && x.GeneratorTime < DateTime.Now.AddDays(-1)).OrderBy(x => x.GeneratorTime).Take(10).ToList();
            List<UserNotification> l = new List<UserNotification>();
            foreach (Notification notification in list)
            {
                notification.Ack = true;
                UserNotification n = new UserNotification();
                n.notification = notification;
                l.Add(n);
                //changing it to 1 means these notification are displayed

            }
            context.SaveChanges();
            return l;
        }

        //this function will count the rows affected to count new notificaitons
        public int AlertCounting(int Userid)
        {
            List<Notification> list = context.Notifications.Where(x => x.UserId == Userid && x.Ack == false).ToList();
            return list.Count();
        }

        //this funcition will delete the notification from table
        public bool deleteNotification(int GeneratorId, int postId, int NotificationType)
        {
            try
            {
                Notification notification = context.Notifications.FirstOrDefault(x => x.GeneratorUserId == GeneratorId && x.PostId == postId && x.NotificationType == 2);
                context.Notifications.Remove(notification);
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
