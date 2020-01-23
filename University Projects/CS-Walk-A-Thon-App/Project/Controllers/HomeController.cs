using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using System.IO;
using System.Net;
using System.Data;
using System.Data.Entity;
using System.Web.Security;
using System.Security.Principal;
using System.Data.Entity.Validation;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            User user = GetUser();

            //Using a Cookie here
            HttpCookie cookie = new HttpCookie("UserID");
            cookie["ID"] = user.userID.ToString();
            cookie["Gender"] = user.gender;

            DateTime currentYear = DateTime.Now;
            DateTime birthYear = user.dateOfBirth;

            cookie["Distance"] = user.totDistanceTravelled.ToString();
            cookie["Age"] = (currentYear.Year - birthYear.Year).ToString();
            Response.Cookies.Add(cookie);
            //////////////////////////////////

            //Stephen Added
            ViewBag.Distance = int.Parse(user.totDistanceTravelled.ToString());

            DashboardModel data = new DashboardModel()
            {
                achievements = user.Achievements.Reverse().Take(10).ToList(),
                leaderboard = db.RouteLeaderboards.OrderByDescending(item => item.participantDistance).Take(5).ToList(),
                currentRoute = db.Routes.OrderByDescending(item => item.routeID).FirstOrDefault()
            };
            List<DistanceLog> distances = db.DistanceLogs.ToList();
            double total = 0;
            foreach (DistanceLog distance in distances)
            {
                total += distance.logDistance;
            }
            data.distance = total;
            if (data.currentRoute != null)
            {
                List<RouteSection> sections = data.currentRoute.RouteSections.ToList();
                int pos = 0;
                double currentDist = 0;
                for (int i = 0; i < sections.Count; i++)
                {
                    RouteSection routeSection = sections[i];
                    currentDist += routeSection.sectionDistance;
                    if (total < currentDist)
                    {
                        pos = i - 1;
                        break;
                    }
                }
                if (pos >= 0)
                {
                    PointOfInterest poi = sections[pos].PointOfInterests.FirstOrDefault();
                    data.currentPOI = poi;
                    Image image = poi.Image;
                    ViewBag.Image = "data:" + image.imageContentType + ";base64," + Convert.ToBase64String(image.imageData);
                }
                else
                {
                    data.currentPOI = null;
                }
            }
            else
            {
                data.currentPOI = null;
            }
            return View(data);
        }

        public User GetUser()
        {
            //return db.Users.FirstOrDefault(x => x.userName == "testUser");
            string userName = User.Identity.Name;
            User user = db.Users.Where(x => x.userName == userName).FirstOrDefault();
            return user;
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(x => x.userID == id);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "userName,password")]User user)
        {
            //RoleUser roleUser = db.ValidateUser(user.Username, user.Password).FirstOrDefault();
            //Validate_User_Result roleUser = db.Validate_User(user.Username, user.Password).FirstOrDefault();
            //string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "SHA1");
            //Validate_User_Result roleUser = db.Validate_User(user.Username, hashPassword).FirstOrDefault();

            User userFound = db.Users.Where(x => x.userName == user.userName)
                .Where(x => x.password == user.password)
                .FirstOrDefault();

            string message = string.Empty;

            if(userFound == null)
            {
                message = "Username and/or password is incorrect.";
            }
            else
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.userName, DateTime.Now, DateTime.Now.AddMinutes(2880), false, userFound.typeOfUser, FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                {
                    return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            //switch (roleUser.UserId.Value)
            //{
            //    case -1:
            //        message = "Username and/or password is incorrect.";
            //        break;
            //    default:
            //        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(2880), user.RememberMe, roleUser.Roles, FormsAuthentication.FormsCookiePath);
            //        string hash = FormsAuthentication.Encrypt(ticket);
            //        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

            //        if (ticket.IsPersistent)
            //        {
            //            cookie.Expires = ticket.Expiration;
            //        }
            //        Response.Cookies.Add(cookie);
            //        if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
            //        {
            //            return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
            //        }
            //        else
            //        {
            //            return RedirectToAction("Index");
            //        }
            //}

            ViewBag.Message = message;
            return View(user);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // GET: Home/LogDistance
        [Authorize]
        public ActionResult LogDistance()
        {
            return View(GetDistanceLogger());
        }

        public DistanceLogger GetDistanceLogger()
        {
            User user = GetUser();
            DistanceLogger data = new DistanceLogger()
            {
                distanceLogs = user.DistanceLogs.ToList(),
                currentRoute = db.Routes.FirstOrDefault()
            };
            DistanceLog lastLog = data.distanceLogs.LastOrDefault();
            if (lastLog != null)
            {
                String lastLogDate = lastLog.dateTimeCaptured.ToString("dd/MM/yyyy");
                String today = DateTime.Today.ToString("dd/MM/yyyy");
                if (lastLogDate.Equals(today))
                {
                    data.alreadyLogged = true;
                }
                else
                {
                    data.alreadyLogged = false;
                }
            }
            else
            {
                data.alreadyLogged = false;
            }
            return data;
        }

        // POST: Home/LogDistance
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogDistance(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                if (db.Routes.FirstOrDefault() != null)
                {
                    DistanceLog log = new DistanceLog()
                    {
                        logDistance = double.Parse(collection["logDistance"]),
                        dateTimeCaptured = DateTime.Today,
                        userID = GetUser().userID
                    };
                    db.DistanceLogs.Add(log);
                    db.SaveChanges();
                    CheckProgress(log.logDistance);
                    return RedirectToAction("Index");
                }
                else
                {
                    AddDistance(double.Parse(collection["logDistance"]));
                    return RedirectToAction("Index");
                }
            }

            return View(GetDistanceLogger());
        }

        public void AddDistance(double distance)
        {
            User user = GetUser();
            user.totDistanceTravelled = user.totDistanceTravelled + distance;
            user.confirmPassword = user.password;
            db.SaveChanges();
        }

        public void CheckProgress(double distance)
        {
            AddDistance(distance);
            List<DistanceLog> distanceLogs = db.DistanceLogs.ToList();
            List<int> userIDList = new List<int>();
            double total = 0;
            foreach (DistanceLog current in distanceLogs)
            {
                total += current.logDistance;
                if (userIDList.IndexOf(current.userID) < 0)
                {
                    userIDList.Add(current.userID);
                }
            }
            Route currentRoute = db.Routes.FirstOrDefault();
            if (total > currentRoute.distanceCovered)
            {
                foreach (int userID in userIDList)
                {
                    User user = GetUser(userID);
                    user.Achievements.Add(db.Achievements.FirstOrDefault(x => x.achievementID == currentRoute.achievementID));
                }
                db.SaveChanges();
            }
            UpdateLeaderboard(distance);
        }

        public void UpdateLeaderboard(double distance)
        {
            User user = GetUser();
            RouteLeaderboard userPos = db.RouteLeaderboards.FirstOrDefault(x => x.participantID == user.userID);
            if (userPos == null)
            {
                userPos = new RouteLeaderboard()
                {
                    participantID = user.userID,
                    participantDistance = distance,
                    participantFirstName = user.firstName,
                    participantSurname = user.surname
                };
                db.RouteLeaderboards.Add(userPos);
            }
            else
            {
                double newDistance = userPos.participantDistance + distance;
                userPos.participantDistance = newDistance;
            }
            db.SaveChanges();
        }

        // GET: Home/ViewLeaderboard
        public ActionResult ViewLeaderboard()
        {
            LeaderboardModel data = new LeaderboardModel()
            {
                users = db.Users.OrderByDescending(item => item.totDistanceTravelled).Take(10).ToList(),
                leaderboard = db.RouteLeaderboards.OrderByDescending(item => item.participantDistance).Take(10).ToList()
            };
            return View(data);
        }

        // GET: Home/ViewProfile
        [Authorize]
        public ActionResult ViewProfile()
        {
            User user = GetUser();
            //User user = db.Users.FirstOrDefault(x => x.userName == "testUser");
            //Image image = db.Images.FirstOrDefault(x => x.imageID == user.profilePictureID);
            Image image = user.Image;
            ViewBag.Image = "data:" + image.imageContentType + ";base64," + Convert.ToBase64String(image.imageData);
            return View(user);
        }

        // GET: Home/EditProfile/5
        [Authorize]
        public ActionResult EditProfile(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = db.Users.Find(id);

            ViewBag.GenderList = getGenders();
            ViewBag.FacultyList = getFaculties();
            ViewBag.YearList = getYears();
            ViewBag.UserTypeList = getUserTypes();

            User user = GetUser();
            //User user = db.Users.FirstOrDefault(x => x.userName == "testUser");
            Image image = user.Image;
            ViewBag.Image = "data:" + image.imageContentType + ";base64," + Convert.ToBase64String(image.imageData);
            user.confirmPassword = user.password;
            if (user == null)
            {
                return HttpNotFound();
            }
            //User user = db.Users.FirstOrDefault(x => x.userID == id);
            return View(user);
        }

        // POST: Home/EditProfile/5
        [HttpPost]
        [Authorize]
        public ActionResult EditProfile([Bind(Include = "userID,firstName,surname,dateOfBirth,gender,yearOfStudy,degreeFaculty,emailAddress,profilePictureID,totDistanceTravelled,userName,password,confirmPassword,typeOfUser")] User user, HttpPostedFileBase upload)
        {
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(upload.InputStream))
                    {
                        bytes = br.ReadBytes(upload.ContentLength);
                    }
                    Image image = new Image
                    {
                        imageName = Path.GetFileName(upload.FileName),
                        imageContentType = upload.ContentType,
                        imageData = bytes
                    };
                    db.Images.Add(image);
                    //var imageID = dataModel.Images.Last().imageID;
                    //int imageID = (from d in db.Images
                    //               orderby d.imageID
                    //               select d.imageID).LastOrDefault();
                    user.profilePictureID = image.imageID;
                }
                else
                {
                    // 1 is default
                    //user.profilePictureID = 1;
                }
                //user.totDistanceTravelled = 0;
                //db.Users.Add(user);
                //db.SaveChanges();

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewProfile");
            }
            return View(user);
        }

        // GET: Home/ViewAchievements/5
        [Authorize]
        public ActionResult ViewAchievements(int? id)
        {
            // might need to remove the id being passed as a route value and rather
            // query for logged in user adn extract the user from there
            //User user = db.Users.FirstOrDefault(x => x.userID == id);
            User user = GetUser();
            //User user = db.Users.FirstOrDefault(x => x.userName == "testUser");
            //return View(user.Achievements.ToList());
            return View(user);
        }

        // GET: Home/CreateProfile
        public ActionResult CreateProfile()
        {
            ViewBag.GenderList = getGenders();
            ViewBag.FacultyList = getFaculties();
            ViewBag.YearList = getYears();
            ViewBag.UserTypeList = getUserTypes();
            return View();
        }

        public IEnumerable<SelectListItem> getYears()
        {
            List<SelectListItem> years = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "First Year"},
                new SelectListItem { Value = "2", Text = "Second Year"},
                new SelectListItem { Value = "3", Text = "Third Year"},
                new SelectListItem { Value = "4", Text = "Honours"},
                new SelectListItem { Value = "5", Text = "Masters"},
                new SelectListItem { Value = "6", Text = "Doctoral"},
                new SelectListItem { Value = "7", Text = "Lecturer"},
            };
            return years;
        }

        public IEnumerable<SelectListItem> getUserTypes()
        {
            List<SelectListItem> usertypes = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Participant", Text = "Participant"},
                new SelectListItem { Value = "Administrator", Text = "Administrator"}
            };
            return usertypes;
        }

        public IEnumerable<SelectListItem> getGenders()
        {
            List<SelectListItem> regions = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Male", Text = "Male"},
                new SelectListItem { Value = "Female", Text = "Female"}
            };
            return regions;
        }

        public IEnumerable<SelectListItem> getFaculties()
        {
            List<SelectListItem> faculties = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Science", Text = "Faculty of Science"},
                new SelectListItem { Value = "Business", Text = "Faculty of Business and Economic Sciences"}
            };
            return faculties;
        }

        // POST: Home/CreateProfile
        //[HttpPost]
        //public ActionResult CreateProfile(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile([Bind(Include = "userID,firstName,surname,dateOfBirth,gender,yearOfStudy,degreeFaculty,emailAddress,userName,password,confirmPassword,typeOfUser")] User user, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(upload.InputStream))
                    {
                        bytes = br.ReadBytes(upload.ContentLength);
                    }
                    Image image = new Image
                    {
                        imageName = Path.GetFileName(upload.FileName),
                        imageContentType = upload.ContentType,
                        imageData = bytes
                    };
                    db.Images.Add(image);
                    //var imageID = dataModel.Images.Last().imageID;
                    //int imageID = (from d in db.Images
                    //               orderby d.imageID
                    //               select d.imageID).LastOrDefault();
                    user.profilePictureID = image.imageID;
                }
                else
                {
                    // 1 is default
                    user.profilePictureID = 1;
                }
                user.totDistanceTravelled = 0;
                db.Users.Add(user);
                db.SaveChanges();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.userName, DateTime.Now, DateTime.Now.AddMinutes(2880), false, user.typeOfUser, FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //[HttpPost]
        //public ActionResult UploadPicture(HttpPostedFileBase upload)
        //{

        //    byte[] bytes;
        //    using (BinaryReader br = new BinaryReader(upload.InputStream))
        //    {
        //        bytes = br.ReadBytes(upload.ContentLength);
        //    }
        //    dataModel.Images.Add(new Image
        //    {
        //        imageName = Path.GetFileName(upload.FileName),
        //        imageContentType = upload.ContentType,
        //        imageData = bytes
        //    });
        //    dataModel.SaveChanges();
        //    return RedirectToAction("CreateProfile");
        //}
    }
}
