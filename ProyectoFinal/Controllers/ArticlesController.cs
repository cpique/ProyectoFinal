using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.Repositories;

namespace ProyectoFinal.Controllers
{
    public class ArticlesController : Controller
    {
        #region Properties
        private IArticleRepository articleRepository;
        private ISupplierRepository supplierRepository;
        #endregion

        #region Constructors
        public ArticlesController()
        {
            this.articleRepository = new ArticleRepository(new GymContext());
            this.supplierRepository = new SupplierRepository(new GymContext());
        }

        public ArticlesController(IArticleRepository articleRepository, ISupplierRepository supplierRepository)
        {
            this.articleRepository = articleRepository;
            this.supplierRepository = supplierRepository;
        }
        #endregion

        // GET: Articles
        public ActionResult Index()
        {
            var articles = articleRepository.GetArticles();
            return View(articles);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = articleRepository.GetArticleByID((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName");
            return View();
        }

        // POST: Articles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Type,Price,Status,PurchaseDate,SupplierID")] Article article)
        {
            if (ModelState.IsValid)
            {
                articleRepository.InsertArticle(article);
                articleRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", article.SupplierID);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = articleRepository.GetArticleByID((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", article.SupplierID);
            return View(article);
        }

        // POST: Articles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Type,Price,Status,PurchaseDate,SupplierID")] Article article)
        {
            if (ModelState.IsValid)
            {
                articleRepository.UpdateArticle(article);
                articleRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", article.SupplierID);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = articleRepository.GetArticleByID((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = articleRepository.GetArticleByID((int)id);
            articleRepository.DeleteArticle((int)id);
            articleRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                articleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
