namespace Sign_Up_Form.Models
{
    public class Alerts
    {
       public List<UserNotification> Today { get; set; }
       public List<UserNotification> Past { get; set; }

       public UserDatum user { get; set; }
        public Alerts()
        {
            Today = new List<UserNotification>();
            Past = new List<UserNotification>();
        }
    }
}
