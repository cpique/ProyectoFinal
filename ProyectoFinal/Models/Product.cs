using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static ProyectoFinal.Utils.Catalog;

namespace ProyectoFinal.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public double Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsInOrder { get; set; }

        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        public int SupplierID { get; set; }
    }
}