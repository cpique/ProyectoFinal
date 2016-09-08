using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class ActivitySchedule
    {
        public int ActivityScheduleID { get; set; }
        public string Day { get; set; }
        public float HourFrom { get; set; }
        public float HourTo { get; set; }

        [ForeignKey("ActivityID")]
        public Activity Activity { get; set; }
        public int ActivityID { get; set; }
    }
}