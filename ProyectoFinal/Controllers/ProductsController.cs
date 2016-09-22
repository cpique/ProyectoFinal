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
using System.Configuration;
using ProyectoFinal.Filters;

namespace ProyectoFinal.Controllers
{
    [AuthorizationPrivilege(Role = "Admin")]
    public class ProductsController : Controller
    {
        #region Properties
        private IProductRepository productRepository;
        private ISupplierRepository supplierRepository;
        #endregion

        #region Constructors
        public ProductsController()
        {
            this.productRepository = new ProductRepository(new GymContext());
            this.supplierRepository = new SupplierRepository(new GymContext());
        }

        public ProductsController(IProductRepository ProductRepository, ISupplierRepository supplierRepository)
        {
            
            this.productRepository = ProductRepository;
            this.supplierRepository = supplierRepository;
        }
        #endregion

        // GET: Products
        public ActionResult Index(int? page)
        {
            int pageSize = ConfigurationManager.AppSettings["PageSize"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : 10;
            var products = productRepository.GetProducts();
                                            //.AsPagination(page ?? 1, pageSize);
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product Product = productRepository.GetProductByID((int)id);
            if (Product == null)
            {
                return HttpNotFound();
            }
            return View(Product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName");
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                productRepository.InsertProduct(Product);
                productRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", Product.SupplierID);
            return View(Product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product Product = productRepository.GetProductByID((int)id);
            if (Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", Product.SupplierID);
            return View(Product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Type,Price,Status,PurchaseDate,SupplierID")] Product Product)
        {
            if (ModelState.IsValid)
            {
                productRepository.UpdateProduct(Product);
                productRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(supplierRepository.GetSuppliers(), "SupplierID", "BusinessName", Product.SupplierID);
            return View(Product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product Product = productRepository.GetProductByID((int)id);
            if (Product == null)
            {
                return HttpNotFound();
            }
            return View(Product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product Product = productRepository.GetProductByID((int)id);
            productRepository.DeleteProduct((int)id);
            productRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
