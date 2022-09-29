using Microsoft.AspNetCore.Mvc;
using Sign_Up_Form.Models.Interfaces;

namespace Sign_Up_Form.Controllers
{
    public class AdminController : Controller
    {
        IUserRepository urr;
        public AdminController(IUserRepository urr)
        {
            this.urr = urr;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ActiveOrDeactiveUser(int UserId,int isActive)
        {

            bool curStatus = false;
            if (isActive == 1)
                curStatus = true;
            String admin = HttpContext.Session.GetString("adminEmail");
            urr.ActiveOrDeactiveUser(UserId, curStatus, admin);
            return Json("success");
        }
    }
}
