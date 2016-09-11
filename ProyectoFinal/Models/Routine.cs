using ProyectoFinal.Utils;
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
        public Catalog.LevelRoutine Level { get; set; }
        public Catalog.Status Status { get; set; }

        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
        public int ClientID { get; set; }

        //Navegation Properties
        public ICollection<File> Files { get; set; }

    }
}