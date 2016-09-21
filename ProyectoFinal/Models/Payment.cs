using ProyectoFinal.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }

        [Required]
        [DisplayName("Estado")]
        public Catalog.Status Status { get; set; }

        [Required]
        [DisplayName("Fecha expiración")]
        public DateTime ExpirationDate { get; set; }

        [ForeignKey("ClientID")]
        public Client Client { get; set; }
        public int ClientID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentType PaymentType { get; set; }
        public int PaymentTypeID { get; set; }
    }
}