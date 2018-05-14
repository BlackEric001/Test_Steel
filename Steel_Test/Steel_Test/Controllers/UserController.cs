using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Steel_Test.DAL;
using Steel_Test.Models;

using System.IO;


namespace Steel_Test.Controllers
{
    public class UserController : Controller
    {
        private TestContext db = new TestContext();

        // GET: User
        public ActionResult Index()
        {
            db.Positions.ToList();
            return View(db.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            PopulatePositionsDropDownList();
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FIO,TableNumber,PositionID")] User user, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileUpload != null)
                        user.Photo = StreamToByteArr(fileUpload.InputStream);
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (DataException e)
            {
                ModelState.AddModelError("", String.Format("Unable to save changes. {0}", e.Message));
            }
            PopulatePositionsDropDownList(user.PositionID);
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            PopulatePositionsDropDownList(user.PositionID);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FIO,TableNumber,PositionID")] User user, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (fileUpload != null)
                        user.Photo = StreamToByteArr(fileUpload.InputStream);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException e)
                {
                    ModelState.AddModelError("", String.Format("Unable to save changes. {0}", e.Message));
                }
            }

            PopulatePositionsDropDownList(user.PositionID);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        private void PopulatePositionsDropDownList(object selectedPosition = null)
        {
            var positionsQuery = from d in db.Positions
                                   orderby d.PositionName
                                   select d;
            ViewBag.PositionID = new SelectList(positionsQuery, "PositionID", "PositionName", selectedPosition);
        }

        public static byte[] StreamToByteArr(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}
