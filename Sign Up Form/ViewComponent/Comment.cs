using Microsoft.AspNetCore.Mvc;
namespace Sign_Up_Form.ViewComponents
{
    public class Comment : ViewComponent
    {
        //public String Invoke()
        //{
        //    return "this is data";
        //}
        public IViewComponentResult Invoke(int Id)
        {
            return View();
        }
    }
}
