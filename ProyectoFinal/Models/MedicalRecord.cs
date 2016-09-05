using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class MedicalRecord
    {
        public int MedicalRecordID { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public float Heigth { get; set; }
        [Required]
        public int Age { get; set; }

        [ForeignKey("ClientID")]
        public Client Client { get; set; }
        public int ClientID { get; set; }

    }
}