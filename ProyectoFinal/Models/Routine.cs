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
    public class Routine
    {
        public int RoutineID { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string NameFile { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Nivel")]
        public Catalog.LevelRoutine Level { get; set; }

        [Required]
        [DisplayName("Estado")]
        public Catalog.Status Status { get; set; }

        [Required]
        [DisplayName("Fecha creación")]
        public DateTime CreationDate { get; set; }

        [Required]
        [DisplayName("Cantidad días")]
        public int DaysInWeek { get; set; }

        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
        public int ClientID { get; set; }

        //Navegation Properties
        public ICollection<File> Files { get; set; }

    }
}