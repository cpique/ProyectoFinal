using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class PaymentTypePrice
    {
        public int PaymentTypePriceID { get; set; }
        public float Price { get; set; }
        public DateTime DateFrom { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentType PaymentType { get; set; }
        public int PaymentTypeID { get; set; }
    }
}