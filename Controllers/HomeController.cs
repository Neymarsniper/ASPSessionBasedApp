using Microsoft.AspNetCore.Mvc;
using SeesionASPCore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;     //you gain access to a wide range of HTTP-related classes and types, such as HttpContext, HttpRequest, HttpResponse, HttpCookie, and more.
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace SeesionASPCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbFirstApprochContext _context;

        public HomeController(ILogger<HomeController> logger, DbFirstApprochContext context)
        {
            _logger = logger;
            this._context = context;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("MyKey", "FirstSession"); // SetString() to create new session.
            //TempData["sessionID"] = HttpContext.Session.Id;
            return View();
        }
        public IActionResult About() //this is the way to display the String from HomeController's Action method.
        {
            if (HttpContext.Session.GetString("MyKey").ToString() != null)  //this is used to check if the value associated with the key="MyKey" does exist or not.
            {
                ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString(); // GetString() to Get/View the existing session.
            }
            TempData["sessionID"] = HttpContext.Session.Id;  // since every session have a unique ID, this tempdata is used to show it on a webpage.
            return View();
        }
        // HttpContext is the class's object and Session is the property and GetString(key,value) is the method of the session object.
        //ViewBag is the dynamic property, that allows you to pass from controller to the specific views.

        //NOTE : Since these properties like TempData, ViewData and ViewBag are used to pass data from Controller to the Views.

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        //GET
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserCred user)
        {
            var myuser = _context.UserCreds.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            //It checks if a user with matching credentials exists in the database.
            if (myuser != null)
            {
                HttpContext.Session.SetString("UserSession",myuser.Email);  // In this case, the user's email is stored in a session variable named "UserSession."
                                   // This session variable is used to keep track of whether a user is logged in. Storing the email is a common practice to identify the user in future requests.
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.message = "Login Failed...";
            }
            return View();
        }

        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.mysession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult NewsDetails(int? Id)
        {
            List<News> news = new List<News>()
            {
            new News() {Id =1, Title = "Asian stocks slip as China data continues to disappoint - Reuters", Description="Asian stocks stumbled on Wednesday, as more disappointing Chinese economic data and the absence of meaningful stimulus from Beijing continued to weigh on investor sentiment.", urlToImage="https://www.reuters.com/resizer/ip8qer8P1SG6nQSdSAe_7e_D1iE=/1200x628/smart/filters:quality(80)/cloudfront-us-east-2.images.arcpublishing.com/reuters/JAQ6SVUAKRKMXCEYFJ5WFEQQXQ.jpg", url="https://www.reuters.com/markets/global-markets-wrapup-1-2023-08-16/"},
            new News() {Id =2, Title = "Maui's displaced grow anxious as wildfire recovery drags on - Reuters", Description="A week after wildfire ravaged the resort town of Lahaina, traumatized Maui residents have grown weary from living off relief supplies while many are kept from inspecting their homes and still left awaiting news about their missing loved one.", urlToImage="https://www.reuters.com/resizer/XKOq-0Q2XURddp6CBiAfbf-21tA=/1200x628/smart/filters:quality(80)/cloudfront-us-east-2.images.arcpublishing.com/reuters/S5A7BVXBSFNOXKKLLZOYH5JFYE.jpg", url="https://www.reuters.com/world/us/maui-officials-urge-patience-search-missing-inches-ahead-2023-08-15/"},
            new News() {Id =3, Title = "U.S. band the Killers apologises for bringing Russian fan on stage in Georgia - Reuters", Description = "A week after wildfire ravaged the resort town of Lahaina, traumatized Maui residents have grown weary from living off relief supplies while many are kept from inspecting their homes and still left awaiting news about their missing loved one.", urlToImage = "https://www.reuters.com/resizer/XKOq-0Q2XURddp6CBiAfbf-21tA=/1200x628/smart/filters:quality(80)/cloudfront-us-east-2.images.arcpublishing.com/reuters/S5A7BVXBSFNOXKKLLZOYH5JFYE.jpg", url="https://www.reuters.com/world/europe/us-band-killers-apologises-bringing-russian-fan-stage-georgia-2023-08-16/"},
            new News() {Id =4, Title = "Trump expected to be booked at Fulton County jail, sheriff says - CNN", Description="Former President Donald Trump is expected to surrender at the Fulton County jail, the local sheriff said Tuesday in a statement, along with the other 18 co-defendants charged on Monday in the Georgia 2020 election subversion case.", urlToImage = "https://media.cnn.com/api/v1/images/stellar/prod/230814093833-04-donald-trump-080423.jpg?c=16x9&q=w_800,c_fill", url= "https://www.cnn.com/2023/08/15/politics/fulton-county-jail-trump/index.html"}
            };

            News selectedNews = news.FirstOrDefault(n => n.Id == Id);

            if (selectedNews == null)
            {
                return NotFound();
            }
            return View(selectedNews);
        }

        public IActionResult News()
        {
            //List<News> news = new List<News>()
            //{
            //new News() {Id =1, Title = "Asian stocks slip as China data continues to disappoint - Reuters", Description="Asian stocks stumbled on Wednesday, as more disappointing Chinese economic data and the absence of meaningful stimulus from Beijing continued to weigh on investor sentiment.", urlToImage="https://www.reuters.com/resizer/ip8qer8P1SG6nQSdSAe_7e_D1iE=/1200x628/smart/filters:quality(80)/cloudfront-us-east-2.images.arcpublishing.com/reuters/JAQ6SVUAKRKMXCEYFJ5WFEQQXQ.jpg", url="https://www.reuters.com/markets/global-markets-wrapup-1-2023-08-16/"},
            //new News() {Id =2, Title = "Maui's displaced grow anxious as wildfire recovery drags on - Reuters", Description="A week after wildfire ravaged the resort town of Lahaina, traumatized Maui residents have grown weary from living off relief supplies while many are kept from inspecting their homes and still left awaiting news about their missing loved one.", urlToImage="https://www.reuters.com/resizer/XKOq-0Q2XURddp6CBiAfbf-21tA=/1200x628/smart/filters:quality(80)/cloudfront-us-east-2.images.arcpublishing.com/reuters/S5A7BVXBSFNOXKKLLZOYH5JFYE.jpg", url="https://www.reuters.com/world/us/maui-officials-urge-patience-search-missing-inches-ahead-2023-08-15/"},
            //new News() {Id =3, Title = "U.S. band the Killers apologises for bringing Russian fan on stage in Georgia - Reuters", Description = "A week after wildfire ravaged the resort town of Lahaina, traumatized Maui residents have grown weary from living off relief supplies while many are kept from inspecting their homes and still left awaiting news about their missing loved one.", urlToImage = "https://www.reuters.com/resizer/XKOq-0Q2XURddp6CBiAfbf-21tA=/1200x628/smart/filters:quality(80)/cloudfront-us-east-2.images.arcpublishing.com/reuters/S5A7BVXBSFNOXKKLLZOYH5JFYE.jpg", url="https://www.reuters.com/world/europe/us-band-killers-apologises-bringing-russian-fan-stage-georgia-2023-08-16/"},
            //new News() {Id =4, Title = "Trump expected to be booked at Fulton County jail, sheriff says - CNN", Description="Former President Donald Trump is expected to surrender at the Fulton County jail, the local sheriff said Tuesday in a statement, along with the other 18 co-defendants charged on Monday in the Georgia 2020 election subversion case.", urlToImage = "https://media.cnn.com/api/v1/images/stellar/prod/230814093833-04-donald-trump-080423.jpg?c=16x9&q=w_800,c_fill", url= "https://www.cnn.com/2023/08/15/politics/fulton-county-jail-trump/index.html"}
            //};

            //News selectedNews = news.FirstOrDefault(n => n.Id == Id);

            //if (selectedNews != null)
            //{
            //    return RedirectToAction("NewsDetails", "Home");//return NotFound();
            //}
            return View();

            //return View(news);
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession").ToString() != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        //Get
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCred user)
        {
            if(ModelState.IsValid)
            {
                await _context.UserCreds.AddAsync(user);
                await _context.SaveChangesAsync();
                TempData["success"] = "Registered Successfully";
                return RedirectToAction("Login");
            }
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}