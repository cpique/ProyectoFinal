using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static ProyectoFinal.Utils.Catalog;

namespace ProyectoFinal.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime PurchaseDate { get; set; }

        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        public int SupplierID { get; set; }
    }
}