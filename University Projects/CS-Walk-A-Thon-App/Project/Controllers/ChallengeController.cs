using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ChallengeController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Challenge/JoinChallenge
        public ActionResult JoinChallenge()
        {
            var challenges = db.Challenges.Include(c => c.Achievement);

            HttpCookie cookie = Request.Cookies["UserID"];

            if (cookie != null)
            {
                ViewData["userID"] = cookie["ID"];
                ViewData["userGender"] = cookie["Gender"];
                ViewData["userAge"] = cookie["Age"];

                //Stephen:
                List<DistanceLog> distances = db.DistanceLogs.ToList();
                double total = 0;
                foreach (DistanceLog distance in distances)
                {
                    total += distance.logDistance;
                }
                ViewData["Distance"] = total.ToString();
                //

                List<UserChallenge> userChallenges = db.UserChallenges.ToList();
                bool isExist = myBoolean(userChallenges, cookie["ID"]);
                ViewData["doesExist"] = isExist;
                string JoinedChallenges = findJoinedChalleges(userChallenges, cookie["ID"]);
                ViewData["joinedChallenges"] = JoinedChallenges;
            }

            return View(challenges.ToList());

        }

        private static string findJoinedChalleges(List<UserChallenge> userChallenges, string ID)
        {
            string challengeIDs = string.Empty;

            foreach (UserChallenge current in userChallenges)
            {
                if (current.userID == int.Parse(ID))
                {
                    challengeIDs += current.challengeID + " ";
                }
            }

            return challengeIDs;
        }

        private static bool myBoolean(List<UserChallenge> userChallenges, string ID)
        {
            foreach (UserChallenge current in userChallenges)
            {
                if (current.userID == int.Parse(ID))
                    return true;
            }
            return false;
        }

        public ActionResult SelectedJoinChallenge(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        [HttpPost, ActionName("SelectedJoinChallenge")]
        [ValidateAntiForgeryToken]
        public ActionResult JoinChallengeConfirmed(int id)
        {
            UserChallenge challenge = new UserChallenge();
            HttpCookie cookie = Request.Cookies["UserID"];

            challenge.challengeID = id;
            challenge.userID = int.Parse(cookie["ID"]);

            User newUser = db.Users.Find(challenge.userID);

            bool isEntered = false;

            db.UserChallenges.OrderBy(item => item.position);
            List<UserChallenge> userChallenges = db.UserChallenges.ToList();

            challenge.position = 1;
            /*if (userChallenges.Count == 0)
                challenge.position = 1;
            else
            {*/
            foreach (UserChallenge current in userChallenges)
            {
                if (current.challengeID == id)
                {
                    User curUser = db.Users.Find(current.userID);
                    if (isEntered)
                    {
                        current.position += 1;
                        continue;
                    }
                    if (curUser.totDistanceTravelled <= newUser.totDistanceTravelled)
                    {
                        challenge.position = current.position;
                        current.position += 1;
                        isEntered = true;
                        continue;
                    }
                    if (curUser.totDistanceTravelled > newUser.totDistanceTravelled)
                    {
                        challenge.position++;
                        continue;
                    }

                }
            }
            //}

            if (ModelState.IsValid)
            {
                db.UserChallenges.Add(challenge);
                db.SaveChanges();
            }

            return RedirectToAction("JoinChallenge");
        }

        // GET: Challenge/JoinGroupChallenge
        public ActionResult JoinGroupChallenge()
        {
            // Not necessary I think
            var groupChallenges = db.GroupChallenges.Include(c => c.Achievement);

            HttpCookie cookie = Request.Cookies["UserID"];

            if (cookie != null)
            {
                ViewData["userID"] = cookie["ID"];
                ViewData["userGender"] = cookie["Gender"];
                ViewData["userAge"] = cookie["Age"];
                
                //Stephen added
                ViewData["distance"] = cookie["Distance"];

                List<UserGroupChallenge> userGroupChallenges = db.UserGroupChallenges.ToList();
                bool isExist = myBooleanTwo(userGroupChallenges, cookie["ID"]);
                ViewData["doesExistGroup"] = isExist;
                string JoinedGroupChallenges = findJoinedChalleges2(userGroupChallenges, cookie["ID"]);
                ViewData["joinedGroupChallenges"] = JoinedGroupChallenges;

            }

            return View(groupChallenges.ToList());
        }

        private static string findJoinedChalleges2(List<UserGroupChallenge> userGroupChallenges, string ID)
        {
            string challengeIDs = string.Empty;

            foreach (UserGroupChallenge current in userGroupChallenges)
            {
                if (current.userID == int.Parse(ID))
                {
                    challengeIDs += current.groupChallengeID + " ";
                }
            }

            return challengeIDs;
        }

        private static bool myBooleanTwo(List<UserGroupChallenge> userGroupChallenges, string ID)
        {
            foreach (UserGroupChallenge current in userGroupChallenges)
            {
                if (current.userID == int.Parse(ID))
                    return true;
            }
            return false;
        }

        public ActionResult SelectedJoinGroupChallenge(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupChallenge challenge = db.GroupChallenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        [HttpPost, ActionName("SelectedJoinGroupChallenge")]
        [ValidateAntiForgeryToken]
        public ActionResult JoinGroupChallengeConfirmed(int id)
        {
            UserGroupChallenge challenge = new UserGroupChallenge();
            HttpCookie cookie = Request.Cookies["UserID"];

            challenge.groupChallengeID = id;
            challenge.userID = int.Parse(cookie["ID"]);

            User newUser = db.Users.Find(challenge.userID);

            bool isEntered = false;

            db.UserGroupChallenges.OrderBy(item => item.positionInGroup);
            List<UserGroupChallenge> userGroupChallenges = db.UserGroupChallenges.ToList();

            challenge.positionInGroup = 1;
            /*if (userGroupChallenges.Count == 0)
                challenge.positionInGroup = 1;
            else
            {*/
            foreach (UserGroupChallenge current in userGroupChallenges)
            {
                if (current.groupChallengeID == id)
                {
                    User curUser = db.Users.Find(current.userID);
                    if (isEntered)
                    {
                        current.positionInGroup += 1;
                        continue;
                    }
                    if (curUser.totDistanceTravelled < newUser.totDistanceTravelled)
                    {
                        challenge.positionInGroup = current.positionInGroup;
                        current.positionInGroup += 1;
                        isEntered = true;
                        continue;
                    }
                    if (curUser.totDistanceTravelled > newUser.totDistanceTravelled)
                    {
                        challenge.positionInGroup++;
                        continue;
                    }
                }
            }
            //}

            if (ModelState.IsValid)
            {
                db.UserGroupChallenges.Add(challenge);
                db.SaveChanges();
            }

            return RedirectToAction("JoinGroupChallenge");
        }

        // GET: Challenge/ManageChallenge
        public ActionResult ManageChallenge()
        {
            // Not necessary I think
            var challenges = db.Challenges.Include(c => c.Achievement);

            return View(challenges.ToList());
        }

        // GET: Challenge/ManageGroupChallenge
        public ActionResult ManageGroupChallenge()
        {
            // Not necessary I think
            var groupChallenges = db.GroupChallenges.Include(c => c.Achievement);

            return View(groupChallenges.ToList());
        }

        public ActionResult GroupDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupChallenge groupChallenge = db.GroupChallenges.Find(id);
            if (groupChallenge == null)
            {
                return HttpNotFound();
            }
            return View(groupChallenge);
        }

        // GET: Challenge/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // GET: Challenge/Create
        public ActionResult Create()
        {
            ViewBag.GenderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName");
            return View();
        }

        // POST: Challenge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "challengeID,challengeName,challengeDescription,challengeDistance,challengeTimeStart,challengeTimeEnd,challengeGender,challengeAgeLowerBound,challengeAgeUpperBound,achievementID")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                //string sql = @"SELECT MAX(challengeID) FROM Challenge";
                //int max = db.Database.SqlQuery<int>(sql).Single();
                //challenge.challengeID = max + 1;

                db.Challenges.Add(challenge);
                db.SaveChanges();
                return RedirectToAction("ManageChallenge");
            }
            ViewBag.genderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", challenge.achievementID);
            return View(challenge);
        }

        // GET: Challenge/CreateGroupChallenge
        public ActionResult CreateGroupChallenge()
        {
            ViewBag.FacultyList = getFaculties();
            ViewBag.UserTypeList = getUserTypes();
            ViewBag.GenderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName");
            return View();
        }

        // POST: Challenge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroupChallenge([Bind(Include = "groupChallengeID,groupChallengeName,groupChallengeDescription,groupChallengeDistance,groupChallengeTimeStart,groupChallengeTimeEnd,groupChallengeGender,groupChallengeAgeLowerBound,groupChallengeAgeUpperBound,groupChallengeDegreeYear,groupChallengeDegreeFaculty,groupChallengeUserType,achievementID")] GroupChallenge challenge)
        {
            if (ModelState.IsValid)
            {
                //string sql = @"SELECT MAX(challengeID) FROM Challenge";
                //int max = db.Database.SqlQuery<int>(sql).Single();
                //challenge.challengeID = max + 1;

                db.GroupChallenges.Add(challenge);
                db.SaveChanges();
                return RedirectToAction("ManageGroupChallenge");
            }
            ViewBag.genderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", challenge.achievementID);
            return View(challenge);
        }

        // GET: Challenge/GroupEdit/5
        public ActionResult GroupEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupChallenge groupChallenge = db.GroupChallenges.Find(id);
            if (groupChallenge == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", groupChallenge.achievementID);
            return View(groupChallenge);
        }

        // POST: Challenge/GroupEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupEdit([Bind(Include = "groupChallengeID,groupChallengeName,groupChallengeDescription,groupChallengeDistance,groupChallengeTimeStart,groupChallengeTimeEnd,groupChallengeGender,groupChallengeAgeLowerBound,groupChallengeAgeUpperBound,groupChallengeDegreeYear,groupChallengeDegreeFaculty,groupChallengeUserType,achievementID")] GroupChallenge groupChallenge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupChallenge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageGroupChallenge");
            }
            ViewBag.GenderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", groupChallenge.achievementID);
            return View(groupChallenge);
        }

        // GET: Challenge/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", challenge.achievementID);
            return View(challenge);
        }

        // POST: Challenge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "challengeID,challengeName,challengeDescription,challengeDistance,challengeTimeStart,challengeTimeEnd,challengeGender,challengeAgeLowerBound,challengeAgeUpperBound,achievementID")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(challenge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageChallenge");
            }
            ViewBag.GenderList = getGenders();
            ViewBag.achievementID = new SelectList(db.Achievements, "achievementID", "achievementName", challenge.achievementID);
            return View(challenge);
        }

        // GET: Challenge/GroupDelete/5
        public ActionResult GroupDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupChallenge groupChallenge = db.GroupChallenges.Find(id);
            if (groupChallenge == null)
            {
                return HttpNotFound();
            }
            return View(groupChallenge);
        }

        // POST: Challenge/Delete/5
        [HttpPost, ActionName("GroupDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult GroupDeleteConfirmed(int id)
        {
            GroupChallenge groupChallenge = db.GroupChallenges.Find(id);
            db.GroupChallenges.Remove(groupChallenge);
            db.SaveChanges();
            return RedirectToAction("ManageGroupChallenge");
        }

        // GET: Challenge/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // POST: Challenge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Challenge challenge = db.Challenges.Find(id);
            db.Challenges.Remove(challenge);
            db.SaveChanges();
            return RedirectToAction("ManageChallenge");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

        public IEnumerable<SelectListItem> getUserTypes()
        {
            List<SelectListItem> usertypes = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Participant", Text = "Participant"},
                new SelectListItem { Value = "Administrator", Text = "Administrator"}
            };
            return usertypes;
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
    }
}
