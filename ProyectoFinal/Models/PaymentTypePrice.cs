using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class PaymentTypePrice
    {
        public int PaymentTypePriceID { get; set; }

        [Required]
        [DisplayName("Precio")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Fecha desde")]
        public DateTime DateFrom { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentType PaymentType { get; set; }
        public int PaymentTypeID { get; set; }
    }
}