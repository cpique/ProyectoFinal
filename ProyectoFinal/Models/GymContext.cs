using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class GymContext : DbContext
    {
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public GymContext() : base("GymContext")
        { }
    }

    public class GymInitializer : DropCreateDatabaseAlways<GymContext>
    {
        protected override void Seed(GymContext context)
        {
            var clients = new List<Client>
            {
                new Client { FirstName = "John", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), DateTo = new DateTime(2016, 12, 31), IdentityCard = "34578644", Email = "john.doe@hotmail.com",
                Password = "123456789", PasswordSalt = "123456789" }

            };

            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var routines = new List<Routine>
            {
                new Routine { ClientID = 1, Description = "Rutina personalizada", Files = new List<File>() }
            };

            routines.ForEach(r => context.Routines.Add(r));
            context.SaveChanges();



            var medicalRecords = new List<MedicalRecord>
            {
                new MedicalRecord { ClientID = 1, Age = 26, Gender = 'M', Heigth = (float)1.80, Weight = 72 } 
            };

            medicalRecords.ForEach(m => context.MedicalRecords.Add(m));
            context.SaveChanges();
        }

    }
}