using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Repositories
{
    public class AdminRepository : IAdminRepository, IDisposable
    {
        #region Properties
        public GymContext context;
        private bool disposed = false;
        #endregion

        #region Constructors
        public AdminRepository(GymContext context)
        {
            this.context = context;
        }
        #endregion

        #region Interface Implementation
        public IEnumerable<Admin> GetAdmins()
        {
            return context.Admins.ToList();
        }

        public Admin GetAdminByID(int id)
        {
            return context.Admins.Find(id);
        }

        public void InsertAdmin(Admin admin)
        {
            context.Admins.Add(admin);
        }

        public void DeleteAdmin(int id)
        {
            Admin admin = context.Admins.Find(id);
            context.Admins.Remove(admin);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            context.Entry(admin).State = EntityState.Modified;
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