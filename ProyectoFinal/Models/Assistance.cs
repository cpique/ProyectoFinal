using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Assistance
    {
        public int AssistanceID { get; set; }
        public DateTime assistanceDate { get; set; }
        public int ClientID { get; set; }
        

    }
}