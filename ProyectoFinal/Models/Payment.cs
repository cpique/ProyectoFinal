using ProyectoFinal.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public Catalog.Status Status { get; set; }

        [ForeignKey("ClientID")]
        public Client Client { get; set; }
        public int ClientID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentType PaymentType { get; set; }
        public int PaymentTypeID { get; set; }
    }
}