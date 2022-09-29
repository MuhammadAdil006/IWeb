namespace Sign_Up_Form.Models.Interfaces
{
    public interface INotificationRepository
    {
        public void AddLikeToNotification(Like l);
        public void AddCommentToNotification(Comment C, Message M, String email);
        public List<UserNotification> GetTenTodaySNotifications(int UserId);
        public List<UserNotification> getTenPastNotifications(int UserId);
        public int AlertCounting(int Userid);
        public bool deleteNotification(int GeneratorId, int postId, int NotificationType);


    }
}
