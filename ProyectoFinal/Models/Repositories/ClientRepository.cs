﻿using ProyectoFinal.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Repositories
{
    public class ClientRepository : IClientRepository, IDisposable
    {
        #region Properties
        public GymContext context;
        private bool disposed = false;
        #endregion

        #region Constructors
        public ClientRepository(GymContext context)
        {
            this.context = context; 
        }
        #endregion

        #region Interface Implementation
        public IEnumerable<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public Client GetClientByID(int id)
        {
            return context.Clients.Find(id);
        }

        public void InsertClient(Client client)
        {
            context.Clients.Add(client);
        }

        public void DeleteClient(int id)
        {
            Client client = context.Clients.Find(id);
            context.Clients.Remove(client);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
        }

        public void HashPassword(Client client)
        {
            client.PasswordSalt = PasswordUtilities.CreateSalt(16);
            client.Password = PasswordUtilities.GenerateSHA256Hash(client.Password, client.PasswordSalt);
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