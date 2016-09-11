using ProyectoFinal.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class PaymentType
    {
        public int PaymentTypeID { get; set; }
        public string Description { get; set; }
        public Catalog.Status Status { get; set; }
        public int DurationInMonths { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<PaymentTypePrice> PaymentTypePrices { get; set; }

        [ForeignKey("ActivityID")]
        public virtual Activity Activity { get; set; }
        public int ActivityID { get; set; }
    }
}