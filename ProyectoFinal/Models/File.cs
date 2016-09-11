using ProyectoFinal.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class File
    {
        public int FileID { get; set; }

        [Required]
        public string NameFile { get; set; }


        public string ExerciseName { get; set; }
        public string Peso { get; set; }
        public string Repetitions { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey("RoutineID")]
        public Routine Routine { get; set; }
        public int RoutineID { get; set; }
    }
}