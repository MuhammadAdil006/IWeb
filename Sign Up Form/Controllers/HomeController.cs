using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sign_Up_Form.Models;
using Sign_Up_Form.Models.Interfaces;
using Sign_Up_Form.Models.Repository;
using Sign_Up_Form.Models.ViewModel;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Claims;

namespace Sign_Up_Form.Controllers
{
    public class HomeController : Controller
    {
        INotificationRepository notificationRepository;
        IUserRepository usrmng;
        IPostRepository pstmng;
        IUserPostRepository usrpstmng;
        IMapper mapper;
        private readonly ILogger<HomeController> _logger;
       
        //reverse scenario db to model to view to controller
      
        public HomeController(ILogger<HomeController> logger,INotificationRepository Nr,IUserRepository Ur,IPostRepository Pr,IUserPostRepository UPr,IMapper ma)
        {
            notificationRepository = Nr;
            _logger = logger;
            usrmng = Ur;
            pstmng =Pr;
            usrpstmng = UPr;
            
            mapper =ma;
            AllEmails.emailMappers = usrmng.GetallEmailsAndUserName();
        }
        //Redirect after creating posts
        public ViewResult GetHome(int UserId)
        {
            UserPost u = new UserPost();

            UserDatum d = new UserDatum();
            d= usrmng.GetUser(UserId);

            u = usrpstmng.setAttributes(u, d);
            //getting the alert counts
            u.AlertCount = notificationRepository.AlertCounting(UserId);
            List<UserPost> usp = new List<UserPost>();
            //also add 10 blogs posts
            List<Post> blogsPosts = pstmng.getTenPosts();//it will get first 10 posts
                                                         //get the first name and lastname and also profile image
            List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
            List<UserPost> uspo = new List<UserPost>();
            for (int i = 0; i < Userdata.Count; i++)
            {
                UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);
                uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, u.user.Id);

                uspo.Add(uspp);
            }
            uspo.Add(u);
            return View("NewsFeed", uspo);
        }
        //Sign in 
        [HttpPost]
        public async Task<IActionResult> SignIn(RegisterUser u)
        {
            int id = usrmng.CheckCredentials(u);

            var results = await usrmng.SigningIn(u);
            if(results.Succeeded)
            {
                HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                //also get the first 10 posts
                    //credentials matched
                    UserDatum temp = new UserDatum();
                temp = usrmng.GetUser(id);
                //check if active or not
                if (!temp.IsActive)
                {
                    //credentials not matched
                    RegisterUser temp1 = new RegisterUser();
                    temp1.id = -7;
                    return View("Index", temp1);
                }
                //check if temp is admin or not
                if (usrmng.CheckAdmin(temp.Id))
                {

                    //HttpContext.Session.

                    List<UserDatum> a = usrmng.GetAllUsers(temp.Id, 0);//get all users and place admin at last
                                                                       //use auto map for password

                    //manage the session skip value adding skip value to 0

                    String adminEmail = a.Where(x => x.Id == temp.Id).FirstOrDefault().Email;
                    HttpContext.Session.SetString("adminEmail", adminEmail);

                    List<UserWithLessDetail> b = new List<UserWithLessDetail>();
                    foreach (UserDatum c in a)
                    {
                        UserWithLessDetail up = new UserWithLessDetail();
                        up = mapper.Map<UserWithLessDetail>(c);
                        b.Add(up);
                    }

                    return View("AdminPannel", b);
                }
                else
                {

                    UserPost mymodel = new UserPost();
                    mymodel = usrpstmng.setAttributes(mymodel, temp);
                    //getting the alert counts
                    mymodel.AlertCount = notificationRepository.AlertCounting(id);
                    mymodel.offset = 0;//this will select from where get the posts
                    List<Post> blogsPosts = pstmng.getTenPosts();//it will get first 10 posts
                                                                 //get the first name and lastname and also profile image
                    List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
                    List<UserPost> uspo = new List<UserPost>();
                    for (int i = 0; i < Userdata.Count; i++)
                    {
                        UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);
                        //this will store the like eith respect to current user and post
                        uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, mymodel.user.Id);
                        uspo.Add(uspp);
                    }
                    Post p = new Post();
                    mymodel = usrpstmng.setAttributes(mymodel, p);
                    mymodel.user.Id = temp.Id;
                    ViewBag.success = 1;
                    //pending sending list of userpost and user itself
                    uspo.Add(mymodel);//LAST ELEMENT IS USER LOGGED INS

                    //adding cookie userid
                    if (!HttpContext.Request.Cookies.ContainsKey("UserEmail") && !HttpContext.Request.Cookies.ContainsKey("Usertoken"))
                    {
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddDays(1);

                        HttpContext.Response.Cookies.Append("UserEmail", Convert.ToString(temp.Email), options);
                        HttpContext.Response.Cookies.Append("Usertoken", Convert.ToString(usrmng.Getpassowrd(temp.Id)), options);

                    }

                    return View("NewsFeed", uspo);
                }

            }
            else
            {
                //credentials not matched
                    RegisterUser temp = new RegisterUser();
                temp.id = -6;
                return View("Index", temp);
            }

            //if(id==-1)
            //{
            //    //credentials not matched
            //    User temp = new User();
            //    temp.id = -6;
            //    return View("Index",temp);
            //}
            //else
            //{
            //    //also get the first 10 posts
            //    //credentials matched
            //    UserDatum temp=new UserDatum();
            //    temp= usrmng.GetUser(id);
            //    //check if active or not
            //    if(!temp.IsActive)
            //    {
            //        //credentials not matched
            //        User temp1 = new User();
            //        temp1.id = -7;
            //        return View("Index", temp1);
            //    }
            //    //check if temp is admin or not
            //    if ( usrmng.CheckAdmin(temp.Id))
            //    {

            //        //HttpContext.Session.

            //        List<UserDatum>a=usrmng.GetAllUsers(temp.Id,0);//get all users and place admin at last
            //        //use auto map for password

            //        //manage the session skip value adding skip value to 0

            //            String adminEmail = a.Where(x => x.Id == temp.Id).FirstOrDefault().Email;
            //            HttpContext.Session.SetString("adminEmail", adminEmail);

            //        List<UserWithLessDetail> b = new List<UserWithLessDetail>();
            //        foreach(UserDatum c in a)
            //        {
            //            UserWithLessDetail up = new UserWithLessDetail();
            //            up = mapper.Map<UserWithLessDetail>(c);
            //            b.Add(up);
            //        }

            //        return View("AdminPannel", b);
            //    }
            //    else
            //    {

            //        UserPost mymodel = new UserPost();
            //        mymodel = usrpstmng.setAttributes(mymodel, temp);
            //        //getting the alert counts
            //        mymodel.AlertCount = notificationRepository.AlertCounting(id);
            //        mymodel.offset = 0;//this will select from where get the posts
            //        List<Post> blogsPosts = pstmng.getTenPosts();//it will get first 10 posts
            //                                                     //get the first name and lastname and also profile image
            //        List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
            //        List<UserPost> uspo = new List<UserPost>();
            //        for (int i = 0; i < Userdata.Count; i++)
            //        {
            //            UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);
            //            //this will store the like eith respect to current user and post
            //            uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, mymodel.user.Id);
            //            uspo.Add(uspp);
            //        }
            //        Post p = new Post();
            //        mymodel = usrpstmng.setAttributes(mymodel, p);
            //        mymodel.user.Id = temp.Id;
            //        ViewBag.success = 1;
            //        //pending sending list of userpost and user itself
            //        uspo.Add(mymodel);//LAST ELEMENT IS USER LOGGED INS

            //        //adding cookie userid
            //        if (!HttpContext.Request.Cookies.ContainsKey("UserEmail")&& !HttpContext.Request.Cookies.ContainsKey("Usertoken"))
            //        {
            //            CookieOptions options = new CookieOptions();
            //            options.Expires = DateTime.Now.AddDays(1);

            //            HttpContext.Response.Cookies.Append("UserEmail", Convert.ToString(temp.Email), options);
            //            HttpContext.Response.Cookies.Append("Usertoken", Convert.ToString(usrmng.Getpassowrd(temp.Id)), options);

            //        }

            //        return View("NewsFeed", uspo);
            //    }

            //}
        }
        public ViewResult Adventurours(int UserId)
        {

            UserDatum d = new UserDatum();
            d = usrmng.GetUser(UserId);
            UserPost u = new UserPost();
            u = usrpstmng.setAttributes(u, d);
            //getting the alert counts
            u.AlertCount = notificationRepository.AlertCounting(UserId);
            //SEND LIST ALSO HERE FIRST DEAL WITH NEWSFEED
            List<Post> blogsPosts = pstmng.getAdventouroursPosts();//it will get first 10 posts
                                                         //get the first name and lastname and also profile image
            List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
            List<UserPost> uspo = new List<UserPost>();
            for (int i = 0; i < Userdata.Count; i++)
            {
                UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);

                uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, u.user.Id);
                uspo.Add(uspp);
            }
            uspo.Add(u);
            return View("Adventurours", uspo);
           
        }
        public ViewResult Sports(int UserId)
        {
            UserPost u = new UserPost();
            UserDatum d = new UserDatum();
            d = usrmng.GetUser(UserId);
            u = usrpstmng.setAttributes(u, d);
            //getting the alert counts
            u.AlertCount = notificationRepository.AlertCounting(UserId);
            //SEND LIST ALSO HERE FIRST DEAL WITH NEWSFEED
            //SEND LIST ALSO HERE FIRST DEAL WITH NEWSFEED
            List<Post> blogsPosts = pstmng.getSportsPosts();//it will get first 10 posts
                                                                   //get the first name and lastname and also profile image
            List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
            List<UserPost> uspo = new List<UserPost>();
            for (int i = 0; i < Userdata.Count; i++)
            {
                UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);
                uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, u.user.Id);
                uspo.Add(uspp);
            }
            uspo.Add(u);
            return View("Sports", uspo);
        }
        
        public ViewResult Gaming(int UserId )
        {
            UserPost u= new UserPost();
            UserDatum d = new UserDatum();
            d = usrmng.GetUser(UserId);
            u = usrpstmng.setAttributes(u, d);
            //getting the alert counts
            u.AlertCount = notificationRepository.AlertCounting(UserId);
            //SEND LIST ALSO HERE FIRST DEAL WITH NEWSFEED
            //SEND LIST ALSO HERE FIRST DEAL WITH NEWSFEED
            List<Post> blogsPosts = pstmng.getGamingPosts();//it will get first 10 posts
                                                                   //get the first name and lastname and also profile image
            List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
            List<UserPost> uspo = new List<UserPost>();
            for (int i = 0; i < Userdata.Count; i++)
            {
                UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);
                uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, u.user.Id);
                uspo.Add(uspp);
            }
            uspo.Add(u);
            
            return View("Gaming", uspo);
        }
        //sign up form 1st time getting
        [HttpGet]
        public IActionResult SignUp()
        {
            //after sign up again to index


            //User temp = new User();
            //temp.firstname = "";
            //temp.New_password = "";
            //temp.id = -1;
            //return View("Index",temp);
            RegisterUser r = new RegisterUser();
            return View("Index", r);
           
        }
        //sign up form getting user data

      
  
        [HttpPost]
        
        public async Task<IActionResult> SignUp(/*User u*/ RegisterUser u)
        {

<<<<<<< HEAD
            
=======
            //u.About = "";
>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e
            //model validations
            if (ModelState.IsValid)
            {
                //if adding true then added successfuly else email already exits
<<<<<<< HEAD
                String lowEmail = u.Email.ToLower();
                if (AllEmails.emailMappers.ContainsKey(lowEmail))//if email exists then return it already exits 
                {

                    u.id = -4;
=======
                //String lowEmail = u.email.ToLower();
                //if(AllEmails.emailMappers.ContainsKey(lowEmail))//if email exists then return it already exits 
                //{
>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e

                //    u.id = -4;

                //    return View("Index", u);
                //}
                //u.date = Convert.ToDateTime(usrmng.MakeDate(u));
                //usrmng.addUser(u);
                //User temp1 = new User();
                //temp1.firstname = "";
                //temp1.New_password = "";
                //temp1.id = -2;
                ////change id here and display message registered successfuly
                ////further display message
                //return View("Index", temp1);
                u.date = Convert.ToDateTime(usrmng.MakeDate(u));
                u.DateOfBirth = u.date;
                u.joinedDate = System.DateTime.Now;
                var results= await usrmng.addUser(u);
                if(!results.Succeeded)
                {
                    foreach(var errorMessage in results.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    u.id = -5;
                    u.firstname = "";
                    u.Password = "";
                    return View("Index", u);
                }
<<<<<<< HEAD
                //u.date = Convert.ToDateTime(usrmng.MakeDate(u));
                //usrmng.addUser(u);
                //User temp1 = new User();
                //temp1.firstname = "";
                //temp1.New_password = "";
                //temp1.id = -2;
                ////change id here and display message registered successfuly
                ////further display message
                //return View("Index", temp1);
                u.date = Convert.ToDateTime(usrmng.MakeDate(u));
                u.DateOfBirth = u.date;
                u.joinedDate = System.DateTime.Now;
                var results= await usrmng.addUser(u);
                if(!results.Succeeded)
                {
                    foreach(var errorMessage in results.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    u.id = -5;
                    u.firstname = "";
                    u.Password = "";
                    return View("Index", u);
                }
=======
>>>>>>> f04c4ae0478f187a2a0d1f2c976d31adeb5e778e
                ModelState.Clear();
                 RegisterUser temp1 = new RegisterUser();
                temp1.firstname = "";
                temp1.Password = "";
                temp1.id = -2;

                //adding data to table using identity manager

                return View("Index", temp1);



            }
            //After sign up reloading do not display wrong credentials which will be displayed on sign in
            RegisterUser temp2 = new RegisterUser();
            temp2.firstname = "";
            temp2.Password = "";
            temp2.id = -5;

            //return View("Index",temp);
            return View("Index", temp2);




            //further display message

        }
        //this function will store the newly created post in database


        public IActionResult Index()
        {

            //User temp = new User();
            //temp.firstname = "";
            //temp.New_password = "";
            //temp.id = -1;
            //if(HttpContext.Request.Cookies.ContainsKey("UserEmail"))
            //{
            //    if(HttpContext.Request.Cookies.ContainsKey("Usertoken"))
            //    {
            //        //match the password and email
            //        User u = new User(); u.email = HttpContext.Request.Cookies["UserEmail"];
            //        u.New_password=HttpContext.Request.Cookies["Usertoken"];

            //        int id = usrmng.CheckCredentials(u);
            //        if(id != -1)
            //        {
            //            //also get the first 10 posts
            //            //credentials matched
            //            UserDatum temp3 = new UserDatum();
            //            temp3 = usrmng.GetUser(id);
            //            //check if active or not
            //            if (!temp3.IsActive)
            //            {
            //                //removing cookie userid
            //                if (HttpContext.Request.Cookies.ContainsKey("UserEmail"))
            //                {


            //                    HttpContext.Response.Cookies.Delete("UserEmail");
            //                    HttpContext.Response.Cookies.Delete("Usertoken");

            //                }
            //                //credentials not matched
            //                User temp1 = new User();
            //                temp1.id = -7;
            //                return View("Index", temp1);
            //            }
            //            //check if temp is admin or not
            //            if (usrmng.CheckAdmin(temp3.Id))
            //            {

            //                //HttpContext.Session.

            //                List<UserDatum> a = usrmng.GetAllUsers(temp3.Id, 0);//get all users and place admin at last
            //                                                                   //use auto map for password

            //                //manage the session skip value adding skip value to 0

            //                String adminEmail = a.Where(x => x.Id == temp3.Id).FirstOrDefault().Email;
            //                HttpContext.Session.SetString("adminEmail", adminEmail);
            //                if (!HttpContext.Request.Cookies.ContainsKey("UserEmail"))
            //                {
            //                    CookieOptions options = new CookieOptions();
            //                    options.Expires = DateTime.Now.AddDays(1);

            //                    HttpContext.Response.Cookies.Append("UserEmail", Convert.ToString(temp3.Email), options);
            //                    HttpContext.Response.Cookies.Append("Usertoken", Convert.ToString(usrmng.Getpassowrd(temp3.Id)), options);

            //                }
            //                List<UserPost> b = new List<UserPost>();
            //                foreach (UserDatum c in a)
            //                {
            //                    UserPost up = new UserPost();
            //                    up = usrpstmng.setAttributes(up, c);
            //                    b.Add(up);
            //                }

            //                return View("AdminPannel", b);
            //            }
            //            else
            //            {

            //                UserPost mymodel = new UserPost();
            //                mymodel = usrpstmng.setAttributes(mymodel, temp3);
            //                //getting the alert counts
            //                mymodel.AlertCount = notificationRepository.AlertCounting(id);
            //                mymodel.offset = 0;//this will select from where get the posts
            //                List<Post> blogsPosts = pstmng.getTenPosts();//it will get first 10 posts
            //                                                             //get the first name and lastname and also profile image
            //                List<UserDatum> Userdata = usrmng.GetpostUserData(blogsPosts);
            //                List<UserPost> uspo = new List<UserPost>();
            //                for (int i = 0; i < Userdata.Count; i++)
            //                {
            //                    UserPost uspp = usrpstmng.setAttributes(Userdata[i], blogsPosts[i]);
            //                    //this will store the like eith respect to current user and post
            //                    uspp.IsLikedByCurUser = pstmng.IsLiked(uspp, mymodel.user.Id);
            //                    uspo.Add(uspp);
            //                }
            //                Post p = new Post();
            //                mymodel = usrpstmng.setAttributes(mymodel, p);
            //                mymodel.user.Id = temp3.Id;
            //                ViewBag.success = 1;
            //                //pending sending list of userpost and user itself
            //                uspo.Add(mymodel);//LAST ELEMENT IS USER LOGGED INS

            //                //adding cookie userid
            //                if (!HttpContext.Request.Cookies.ContainsKey("UserEmail"))
            //                {
            //                    CookieOptions options = new CookieOptions();
            //                    options.Expires = DateTime.Now.AddDays(1);

            //                    HttpContext.Response.Cookies.Append("UserEmail", Convert.ToString(temp3.Email), options);
            //                    HttpContext.Response.Cookies.Append("Usertoken", Convert.ToString(temp3.Password), options);

            //                }

            //                return View("NewsFeed", uspo);
            //            }
            //        }
            //    }
            //}
            //return View("Index", temp);


            RegisterUser u = new RegisterUser();
            return View("Index", u);
            
        }
        //logout
        [Route ("/Home/logOut",Name ="LogOut")]
        public IActionResult LogOut()
        {
            RegisterUser temp = new RegisterUser();
            temp.firstname = "";
            temp.Password = "";
            temp.id = -1;
            //adding cookie userid
            if (HttpContext.Request.Cookies.ContainsKey("UserEmail"))
            {
                

                HttpContext.Response.Cookies.Delete("UserEmail");
                HttpContext.Response.Cookies.Delete("Usertoken");

            }
            return View("Index", temp);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}