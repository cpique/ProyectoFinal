using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models.Repositories
{
    public interface IActivityTypeRepository : IDisposable
    {
        IEnumerable<ActivityType> GetActivityTypes();
        ActivityType GetActivityTypeByID(int activityTypeID);
        void InsertActivityType(ActivityType activityType);
        void DeleteActivityType(int activityTypeID);
        void UpdateActivityType(ActivityType activityType);
        void Save();
    }
}
