using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Activity
    {
        public Activity()
        {
            this.Instructors = new HashSet<Instructor>();
            this.ActivitySchedules = new HashSet<ActivitySchedule>();
        }

        public int ActivityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<ActivitySchedule> ActivitySchedules { get; set; }
        public ICollection<PaymentType> PaymentTypes { get; set; }
    }
}