using Sign_Up_Form.Models.Interfaces;

namespace Sign_Up_Form.Models.Repository
{
    public class UserNotificationRepository:IUserNotificationRepository
    {
        ShareYourRoutineContext context;
        public UserNotificationRepository()
        {
            context = new ShareYourRoutineContext();
        }

        public List<UserNotification> settingRespectingUsers(List<UserNotification> un)
        {
            foreach (UserNotification uno in un)
            {
                UserDatum d = new UserDatum();
                d = context.UserData.Where(x => x.Id == uno.notification.GeneratorUserId).FirstOrDefault();
                uno.user = d;
            }
            return un;
        }
    }
}
