using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Activity
    {
        public Activity()
        {
            this.Clients = new HashSet<Client>();
        }

        public int ActivityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Client> Clients { get; set; }

    }
}