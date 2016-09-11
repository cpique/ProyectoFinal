using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Repositories
{
    public class InstructorRepository : IInstructorRepository, IDisposable
    {
        #region Properties
        public GymContext context;
        private bool disposed = false;
        #endregion

        #region Constructors
        public InstructorRepository(GymContext context)
        {
            this.context = context;
        }
        #endregion

        #region Interface implementation
        public IEnumerable<Instructor> GetInstructors()
        {
            return context.Instructors.Include(i => i.Activity).ToList();
        }

        public Instructor GetInstructorByID(int id)
        {
            return context.Instructors.Find(id);
        }

        public void InsertInstructor(Instructor instructor)
        {
            context.Instructors.Add(instructor);
        }

        public void DeleteInstructor(int id)
        {
            Instructor instructor = context.Instructors.Find(id);
            context.Instructors.Remove(instructor);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateInstructor(Instructor instructor)
        {
            context.Entry(instructor).State = EntityState.Modified;
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