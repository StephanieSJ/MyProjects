using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
//using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class RouteController : Controller
    {
        private DataModel db = new DataModel();

        public ActionResult TheView()
        {
            //var route = db.Routes.FirstOrDefault();
            //IEnumerable<RouteSection> sectionss = db.RouteSections;
            //RouteAndSection newOne = new RouteAndSection();
            //var model = newOne;
            //foreach (var s in db.RouteSections)
            //{
            //    if (s.routeID == route.routeID)
            //    {
            //        newOne.Route = route;
            //        newOne.sections = sectionss;
            //        return View(model);
            //    }

            //}
            //return View();

            /* HOW I WOULD HAVE DONE IT */

            User user = GetUser();

            HttpCookie cookie = new HttpCookie("UserID");

            Route route = db.Routes.OrderByDescending(item => item.routeID).FirstOrDefault();
            List<DistanceLog> distances = db.DistanceLogs.ToList();
            double total = 0;
            foreach (DistanceLog distance in distances)
            {
                total += distance.logDistance;
            }
            ViewData["Distance"] = total.ToString();

            RouteAndSection newOne = new RouteAndSection();
            if (route != null)
            {
                IEnumerable<RouteSection> sections = route.RouteSections;
                newOne.Route = route;
                newOne.sections = sections;
            }
            return View(newOne);


        }

        public double getDistance()
        {
            User user = GetUser();
            double distance;
            if (user != null)
            {
                distance = user.totDistanceTravelled;
                return distance;
            }
            return 0;
        }

        public User GetUser()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(x => x.userName == userName).FirstOrDefault();
            return user;
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(x => x.userID == id);
        }

        public ActionResult SectionDetails()
        {
            RouteSection rs = db.RouteSections.First();
            return View(rs);
        }

        //public ActionResult EditSection()
        //{
        //    return View();
        //}

        // GET: Route/
        public ActionResult ViewRoute()
        {
            // Not necessary I think
            var routes = db.Routes.Include(r => r.Achievement);

            return View(routes.ToList());
        }

        public ActionResult CreateRouteSection(int? id)
        {
            //ViewBag.routeID = new SelectList(db.Routes, "routeID");
            //Route route = db.Routes.Find(id);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            //ViewBag.RouteID = id;
            RouteSection routeSection = new RouteSection();
            routeSection.routeID = route.routeID;
            ViewBag.RouteName = route.routeName;
            ViewBag.POI_ID = new SelectList(db.PointOfInterests, "POI_ID", "POI_Name");
            return View(routeSection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRouteSection([Bind(Include = "sectionID,sectionStart,sectionEnd,sectionDistance,routeID")] RouteSection routeSection)
        {
            if (ModelState.IsValid)
            {
                PointOfInterest pointOfInterest = db.PointOfInterests.Find(routeSection.POI_ID);
                routeSection.PointOfInterests.Add(pointOfInterest);
                db.RouteSections.Add(routeSection);
                db.SaveChanges();
                return RedirectToAction("CreateRouteSection", routeSection.routeID);
            }

            //ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", route.achievementID);
            ViewBag.POI_ID = new SelectList(db.PointOfInterests, "POI_ID", "POI_Name", routeSection.POI_ID);
            return View(routeSection);
        }

        // GET: Route/
        //public ActionResult ManageRoute()
        //{
        //    // Not necessary I think
        //    var routes = db.Routes.Include(r => r.Achievement);


        //    return View(routes.ToList());
        //}

        public ViewResult ManageRoute(string searchString)
        {

            var routes = from s in db.Routes.Include(t => t.Achievement)
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                routes = routes.Where(s => s.routeName.Contains(searchString));
            }

            return View(routes.ToList());
        }

        public ViewResult ManageSection(string searchString)
        {

            var routesections = from s in db.RouteSections
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                routesections = routesections.Where(s => s.routeID == int.Parse(searchString));
            }

            return View(routesections.ToList());
        }

        // GET: Route/Details/5
        public ActionResult RouteDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // GET: Route/Create
        public ActionResult Create()
        {
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName");
            return View();
        }

        // POST: Route/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "routeID,routeName,routeStart,routeEnd,distanceCovered,achievementID")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("CreateRouteSection", new { id = route.routeID });
            }

            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", route.achievementID);
            return View(route);
        }

        // GET: Route/CreatePOI
        public ActionResult CreatePOI()
        {
            return View();
        }

        // POST: Route/CreatePOI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePOI([Bind(Include = "POI_ID,POI_Name,POI_Information")] PointOfInterest pointOfInterest, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (IsPOIAvailable(pointOfInterest.POI_Name) is true)
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
                        pointOfInterest.Image = image;
                    }
                    else
                    {
                        // 6 is default
                        pointOfInterest.POI_PictureID = 6;
                    }
                    db.PointOfInterests.Add(pointOfInterest);
                    db.SaveChanges();
                    return RedirectToAction("ManageRoute");
                }
                else
                    //{
                    //    ViewBag
                    //}
                    ModelState.AddModelError("POI_Name", "POI name already exists.");
            }


            return View(pointOfInterest);
        }

        public bool IsPOIAvailable(string POIName)
        {
            // Assume these details coming from database 
            List<PointOfInterest> POIs = db.PointOfInterests.ToList();

            var PoIId = (from u in POIs
                         where u.POI_Name == POIName
                         select new { POIName }).FirstOrDefault();

            bool status;
            if (PoIId != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        // GET: POI
        public ActionResult EditPOI(int? i)
        {
            if (i == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointOfInterest poi = db.PointOfInterests.Find(i);
            if (poi == null)
            {
                return HttpNotFound();
            }
            return View(poi);
        }

        //
        // GET: /Movies/Edit/5

        public ActionResult EditRoute(int id = 0)
        {
            ViewBag.Achievements = new SelectList(db.Achievements, "AchievementID", "AchievementName");
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult EditRoute(Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageRoute");
            }
            return View(route);
        }

        public ActionResult EditSection(int id = 0)
        {
            ViewBag.Routes = new SelectList(db.Routes, "routeID", "routeName");
            RouteSection rs = db.RouteSections.Find(id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            return View(rs);
        }


        [HttpPost]
        public ActionResult EditSection(RouteSection rs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageSection");
            }
            return View(rs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPOI([Bind(Include = "POI_ID,POI_Name,POI_Information,POI_Picture")] PointOfInterest poi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewPOI");
            }
            return View(poi);
        }


        // GET: Route/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", route.achievementID);
            return View(route);
        }

        // POST: Route/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "routeID,routeName,routeStart,routeEnd,distanceCovered,achievementID")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", route.achievementID);
            return View(route);
        }

        // GET: Route/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Route/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // View POI
        public ActionResult ViewPOI()
        {
            // Not necessary I think
            var POIs = db.PointOfInterests;

            return View(POIs.ToList());
        }

        // View Section
        public ActionResult ViewSection()
        {
            // Not necessary I think
            var sections = db.RouteSections;

            return View(sections.ToList());
        }
    }

}
