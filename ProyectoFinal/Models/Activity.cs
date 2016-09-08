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
            this.Instructors = new HashSet<Instructor>();
            this.ActivitySchedules = new HashSet<ActivitySchedule>();
        }

        public int ActivityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<ActivitySchedule> ActivitySchedules { get; set; }

    }
}