using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Routine
    {
        public int RoutineID { get; set; }
        public string Description { get; set; }

        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
        public int ClientID { get; set; }

        //Navegation Properties
        [Required]
        public ICollection<File> Files { get; set; }

    }
}