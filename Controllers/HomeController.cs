using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cSharpBelt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;



namespace cSharpBelt.Controllers
{
    public class HomeController : Controller
    {

        private MyContext _context;
        public HomeController(MyContext context)
        {


            _context = context;
            _context.SaveChanges();
        }




        [HttpGet("")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }


        [HttpGet("Registerd")]
        public IActionResult SignIn()
        {
            Console.WriteLine("Got inside registerd");


            return View("SignIn");
        }


        [HttpPost("Home/create")]

        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                user.Confirm_Password = Hasher.HashPassword(user, user.Confirm_Password);

                user.Created_At = DateTime.Now;
                user.Updated_At = DateTime.Now;

                _context.Add(user);
                _context.SaveChanges();
                ViewBag.user = user;

                HttpContext.Session.SetString("Email", user.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Index");
            }


        }




        [HttpPost("Home/login")]

        public IActionResult LogingMethod(string Email, string Password)
        {

            // List<Posts> Posts = _context.posts.Include(wd => wd.Likes).ToList();

            User logUser = _context.users.SingleOrDefault(usr => usr.Email == Email);




            PasswordHasher<User> Hasher = new PasswordHasher<User>();

            if (logUser != null && Password != null)
            {

                if (0 != Hasher.VerifyHashedPassword(logUser, logUser.Password, Password))
                {

                    HttpContext.Session.SetString("Email", logUser.Email);

                    return RedirectToAction("Dashboard");

                }
                else
                {

                    ViewBag.err = "Password or Username is not valid";
                    return View("SignIn");

                }


            }
            else
            {

                ViewBag.err = "Email or Password can't be empty";
                return View("SignIn");
            }


        }




        public IActionResult Dashboard() ////////////////// loading the dash board
        {

            string Email = HttpContext.Session.GetString("Email");
            User logUser = _context.users.SingleOrDefault(usr => usr.Email == Email);

            List<Activities> allActs = _context.activities.Include(a => a.Intrsts).OrderByDescending(a => a.Date).ToList();

            List<Activities> noPast = new List<Activities>();


            foreach (var i in allActs)
            {

                if (i.Date.AddTicks(i.Time.TimeOfDay.Ticks) >= DateTime.Now)
                {
                    noPast.Add(i);
                }
            }
            ViewBag.allActs = noPast.OrderByDescending(a => a.Date.AddTicks(a.Time.TimeOfDay.Ticks));
            ViewBag.user = logUser;



            return View("Dashboard");
        }

        [HttpPost("Home/createNewAct")]////////////////////////////////// creating new avtivity
        public IActionResult ActDetails(Activities acti)
        {

            string Email = HttpContext.Session.GetString("Email");
            User logUser = _context.users.SingleOrDefault(usr => usr.Email == Email);

            acti.UserId = logUser.UserId;
            acti.Creator = logUser.First_Name;
            logUser.Activities.Add(acti);



            _context.SaveChanges();

            Activities act = _context.activities.Include(a => a.Intrsts).FirstOrDefault(a => a.ActivitiesId == acti.ActivitiesId);
            ViewBag.user = logUser;

            ViewBag.act = act;

            return View("ActDetails");
            // return RedirectToAction("ViewActDetails",new {act.ActivitiesId});

        }

        [HttpGet("Home/leave/{id}")] ///////////////////// leave an event
        public IActionResult leave(int id)
        {
            string Email = HttpContext.Session.GetString("Email");
            User logUser = _context.users.SingleOrDefault(usr => usr.Email == Email);

            Intrsts ints = _context.intrsts.FirstOrDefault(i => i.UserId == logUser.UserId);

            Activities act = _context.activities.FirstOrDefault(a => a.ActivitiesId == id);

            act.NumOfPrts--;
            _context.Remove(ints);
            _context.SaveChanges();


            if (act.NumOfPrts < 0)
            {
                act.NumOfPrts = 0;
            }
            return RedirectToAction("Dashboard");

        }



        [HttpGet("Home/join/{id}")] ////////////////// joining Event
        public IActionResult join(int id)
        {
            string Email = HttpContext.Session.GetString("Email");
            User logUser = _context.users.SingleOrDefault(usr => usr.Email == Email);
            Activities act = _context.activities.FirstOrDefault(a => a.ActivitiesId == id);

            List<Intrsts> userEvents = _context.intrsts.Include(i => i.User).Include(i => i.Activities).Where(usr => usr.UserId == logUser.UserId).ToList();

            // DateTime dt = DateTime.Parse(i.ToString());

            // Date.AddTicks(i.Time.TimeOfDay.Ticks) >=DateTime.Now
            DateTime startTimeEventWantToJoin = act.Date.AddTicks(act.Time.TimeOfDay.Ticks);

            int eventTime = 0;
            if (act.durType == "Minutes")
            {
                eventTime = act.Duration;
            }
            else if (act.durType == "Hours")
            {
                eventTime = act.Duration * 60;

            }
            else if (act.durType == "Days")
            {
                eventTime = (act.Duration * 60) * 24;

            }




            DateTime endTimeEventWantToJoin = startTimeEventWantToJoin.AddMinutes(eventTime);

            foreach (var evnt in userEvents)
            {
                DateTime yourEventStartTime = evnt.Activities.Date.AddTicks(evnt.Activities.Time.TimeOfDay.Ticks);
                int yourEventTime = 0;
                if (act.durType == "Minutes")
                {
                    yourEventTime = act.Duration;
                }
                else if (act.durType == "Hours")
                {
                    yourEventTime = act.Duration * 60;

                }
                else if (act.durType == "Days")
                {
                    yourEventTime = (act.Duration * 60) * 24;

                }


                DateTime yourEventEndTimeEvent = yourEventStartTime.AddMinutes(eventTime);


                if (startTimeEventWantToJoin > yourEventStartTime && startTimeEventWantToJoin < yourEventEndTimeEvent)
                {
                    ViewBag.err = "Can't join this Event check your schedule if booked or time is in the past";

                    List<Activities> allActs = _context.activities.Include(a => a.Intrsts).OrderByDescending(a => a.Date).ToList();

                    List<Activities> noPast = new List<Activities>();


                    foreach (var i in allActs)
                    {

                        if (i.Date.AddTicks(i.Time.TimeOfDay.Ticks) >= DateTime.Now)
                        {
                            noPast.Add(i);
                        }
                    }
                    ViewBag.allActs = noPast.OrderByDescending(a => a.Date.AddTicks(a.Time.TimeOfDay.Ticks));
                    ViewBag.user = logUser;


                    // return RedirectToAction("Dashboard");

                    return View("Dashboard");

                }

                else if (endTimeEventWantToJoin > yourEventStartTime && startTimeEventWantToJoin < yourEventEndTimeEvent)
                {
                    ViewBag.err = "Can't join this Event check your schedule if booked or time is in the past";

                    List<Activities> allActs = _context.activities.Include(a => a.Intrsts).OrderByDescending(a => a.Date).ToList();

                    List<Activities> noPast = new List<Activities>();


                    foreach (var i in allActs)
                    {

                        if (i.Date.AddTicks(i.Time.TimeOfDay.Ticks) >= DateTime.Now)
                        {
                            noPast.Add(i);
                        }
                    }
                    ViewBag.allActs = noPast.OrderByDescending(a => a.Date.AddTicks(a.Time.TimeOfDay.Ticks));
                    ViewBag.user = logUser;



                    // return RedirectToAction("Dashboard");

                    return View("Dashboard");
                }





            }


            Intrsts ints = new Intrsts()
            {
                User = logUser,
                Activities = act
            };

            act.NumOfPrts++;
            logUser.Intrsts.Add(ints);
            act.Intrsts.Add(ints);
            _context.Add(ints);
            _context.SaveChanges();


            return RedirectToAction("Dashboard");


        }




        [HttpGet("Home/Delete/{id}")] ////////////////////// deleting event
        public IActionResult Delete(int id)
        {
            Activities act = _context.activities.FirstOrDefault(a => a.ActivitiesId == id);


            _context.Remove(act);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [HttpGet("Home/viewAct/{id}")]

        public IActionResult ViewActDetails(int id)
        { ///////////////////load the details page

            string Email = HttpContext.Session.GetString("Email");
            User logUser = _context.users.SingleOrDefault(usr => usr.Email == Email);


            Activities act = _context.activities.Include(a => a.Intrsts).FirstOrDefault(a => a.ActivitiesId == id);


            List<Intrsts> usrsInts = _context.intrsts.Where(i => i.ActivitiesId == act.ActivitiesId).Include(i => i.User).ToList();

            ViewBag.user = logUser;

            ViewBag.act = act;


            return View("ActDetails");
        }

        [HttpGet("Home/navToDashboard")] ///////////////////////////// return to Dashboard
        public IActionResult NavToDash()
        {

            return RedirectToAction("Dashboard");
        }





        [HttpGet("Home/addNewAct")] /////////////////////////////// load add new page
        public IActionResult NewAct()
        {


            return View("NewAct");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
