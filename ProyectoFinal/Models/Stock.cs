using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Stock
    {
        public int StockID { get; set; }
        public int ArticleID { get; set; }
        [Editable(false)]
        public int CantInStock { get; set; }
        public int DesiredStock { get; set; }
    }
}