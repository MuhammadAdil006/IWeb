using Microsoft.AspNetCore.Mvc;
using Sign_Up_Form.Models;
using Sign_Up_Form.Models.Interfaces;
using Sign_Up_Form.Models.Repository;

namespace Sign_Up_Form.Controllers
{
    public class NotificationController : Controller
    {
        IUserRepository usrmng;
        INotificationRepository notificationRepository;
        IUserNotificationRepository usrNotRepository;

        public NotificationController(IUserRepository Ur,INotificationRepository Nr,IUserNotificationRepository UNr)
        {
            usrmng = Ur;
            usrNotRepository = UNr;
            notificationRepository = Nr;
        }
        public IActionResult GetNotification(int UserId)
        {
            //here get all the notification list and add it in to your page
            List<UserNotification> unreadNotificationList = notificationRepository.GetTenTodaySNotifications(UserId);
            
          
            UserDatum d = new UserDatum();
            d = usrmng.GetUser(UserId);
            Alerts alerts = new Alerts();
            //here get 10 read notificationslits
            alerts.Past=notificationRepository.getTenPastNotifications(UserId);

            alerts.user = d;
            alerts.Today = unreadNotificationList;
            alerts.Today=usrNotRepository.settingRespectingUsers(alerts.Today);
            alerts.Past=usrNotRepository.settingRespectingUsers(alerts.Past);
            return View("Index",alerts);
        }
    }
}
