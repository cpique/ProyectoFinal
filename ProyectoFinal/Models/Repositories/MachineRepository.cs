using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Repositories
{
    public class MachineRepository : IMachineRepository, IDisposable
    {
        #region Properties
        public GymContext context;
        private bool disposed = false;
        #endregion

        #region Constructors
        public MachineRepository(GymContext context)
        {
            this.context = context;
        }
        #endregion

        #region Interface implementation
        public IEnumerable<Machine> GetMachines()
        {
            return context.Machines.ToList();
        }

        public Machine GetMachineByID(int id)
        {
            return context.Machines.Find(id);
        }

        public void InsertMachine(Machine machine)
        {
            context.Machines.Add(machine);
        }

        public void DeleteMachine(int id)
        {
            Machine machine = context.Machines.Find(id);
            context.Machines.Remove(machine);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateMachine(Machine machine)
        {
            context.Entry(machine).State = EntityState.Modified;
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