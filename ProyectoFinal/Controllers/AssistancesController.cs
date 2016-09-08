using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class AssistancesController : Controller
    {
        private GymContext db = new GymContext();

        // GET: Assistances
        public ActionResult Index()
        {
            return View(db.Assistances.ToList());
        }

        // GET: Assistances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistance assistance = db.Assistances.Find(id);
            if (assistance == null)
            {
                return HttpNotFound();
            }
            return View(assistance);
        }

        // GET: Assistances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assistances/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssistanceID,assistanceDate,ClientID")] Assistance assistance)
        {
            if (ModelState.IsValid)
            {
                db.Assistances.Add(assistance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assistance);
        }

        // GET: Assistances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistance assistance = db.Assistances.Find(id);
            if (assistance == null)
            {
                return HttpNotFound();
            }
            return View(assistance);
        }

        // POST: Assistances/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssistanceID,assistanceDate,ClientID")] Assistance assistance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assistance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assistance);
        }

        // GET: Assistances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistance assistance = db.Assistances.Find(id);
            if (assistance == null)
            {
                return HttpNotFound();
            }
            return View(assistance);
        }

        // POST: Assistances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assistance assistance = db.Assistances.Find(id);
            db.Assistances.Remove(assistance);
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
    }
}
