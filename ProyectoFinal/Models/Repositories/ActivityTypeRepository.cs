using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Repositories
{
    public class ActivityTypeRepository : IActivityTypeRepository, IDisposable
    {
        #region Properties
        public GymContext context;
        private bool disposed = false;
        #endregion

        #region Constructors
        public ActivityTypeRepository(GymContext context)
        {
            this.context = context;
        }
        #endregion

        #region Interface Implementation
        public IEnumerable<ActivityType> GetActivityTypes()
        {
            return context.ActivityTypes.ToList();
        }

        public ActivityType GetActivityTypeByID(int id)
        {
            return context.ActivityTypes.Find(id);
        }

        public void InsertActivityType(ActivityType activityType)
        {
            context.ActivityTypes.Add(activityType);
        }

        public void DeleteActivityType(int id)
        {
            ActivityType activityType = context.ActivityTypes.Find(id);
            context.ActivityTypes.Remove(activityType);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateActivityType(ActivityType activityType)
        {
            context.Entry(activityType).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}