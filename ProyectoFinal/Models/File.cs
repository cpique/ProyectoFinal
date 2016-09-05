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
        public string DisplayName { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]
        public byte[] FileData { get; set; }
        [Required]
        public long FileSize { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("RoutineID")]
        public Routine Routine { get; set; }
        public int RoutineID { get; set; }
    }
}