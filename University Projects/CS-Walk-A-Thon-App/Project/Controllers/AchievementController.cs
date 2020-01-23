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
    public class AchievementController : Controller
    {
        private DataModel db = new DataModel();

        // GET: Achievement/Manage
        public ActionResult Manage(int? id, string searchString)
        {
            IEnumerable<Achievement> achievements = db.Achievements.ToList();

            if (id != null)
            {
                achievements = achievements.Where(x => x.achievementID == id);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                achievements = achievements.Where(s => s.achievementName.Contains(searchString));
            }

            if(achievements.Count() == 0)
            {
                ViewBag.Message = "No Achievement Found";
            }

            return View(achievements);
        }

        // GET: Achievement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // GET: Achievement/Create
        public ActionResult Create()
        {
            ViewBag.DifficultyList = getDiffculties();
            ViewBag.CategoryList = getCategories();
            return View();
        }

        // POST: Achievement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "achievementID,achievementName,achievementDescription,achievementDifficulty,achievementCategory")] Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                db.Achievements.Add(achievement);
                db.SaveChanges();
                return RedirectToAction("Manage");
            }

            //ViewBag.CreateMessage = "Please check if all information was entered and/or is correct";

            return View(achievement);
        }

        public IEnumerable<SelectListItem> getDiffculties()
        {
            List<SelectListItem> difficulties = new List<SelectListItem>()
            {
                //new SelectListItem { Value = "Platinum", Text = "Platinum Level"},
                new SelectListItem { Value = "Gold", Text = "Gold Level"},
                new SelectListItem { Value = "Silver", Text = "Silver Level"},
                new SelectListItem { Value = "Bronze", Text = "Bronze Level"}
            };
            return difficulties;
        }

        public IEnumerable<SelectListItem> getCategories()
        {
            List<SelectListItem> categories = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Route", Text = "Awarded for completing a route"},
                new SelectListItem { Value = "Group", Text = "Awarded for completing a group challenge"},
                new SelectListItem { Value = "Solo", Text = "Awarded for completing a challenge individually"}
            };
            return categories;
        }

        // GET: Achievement/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DifficultyList = getDiffculties();
            ViewBag.CategoryList = getCategories();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Achievement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "achievementID,achievementName,achievementDescription,achievementDifficulty,achievementCategory")] Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(achievement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage");
            }
            return View(achievement);
        }

        // GET: Achievement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Achievement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achievement achievement = db.Achievements.Find(id);
            db.Achievements.Remove(achievement);
            db.SaveChanges();
            return RedirectToAction("Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
