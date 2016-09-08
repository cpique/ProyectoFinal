using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<Machine> Machines { get; set; }

    }
}