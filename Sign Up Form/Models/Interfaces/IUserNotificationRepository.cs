namespace Sign_Up_Form.Models.Interfaces
{
    public interface IUserNotificationRepository
    {
        public List<UserNotification> settingRespectingUsers(List<UserNotification> un);

    }
}
