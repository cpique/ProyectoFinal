﻿using ProyectoFinal.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DocType { get; set; }
        [Required]
        public int DocNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateFrom { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Editable(false)]
        public string PasswordSalt { get; set; }

        [Required]
        public Catalog.Roles Role { get; set; }

        public ICollection<MedicalRecord> MedicalRecords { get; set; }
        public ICollection<Routine> Routines { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}