using ProyectoFinal.Utils;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public GymContext() : base("GymContext")
        { }

        #region DbSets
        public DbSet<Activity> Activities { get; set; }

        public DbSet<ActivitySchedule> ActivitySchedules { get; set; }

        public DbSet<ActivityType> ActivityTypes { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Assistance> Assistances { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<PaymentTypePrice> PaymentTypePrices { get; set; }

        public DbSet<Routine> Routines { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        #endregion
    }

    public class GymInitializer : DropCreateDatabaseAlways<GymContext>
    {
        protected override void Seed(GymContext context)
        {
            #region Clients
            var passwordSalt1 = PasswordUtilities.CreateSalt(16);
            var password1 = PasswordUtilities.GenerateSHA256Hash("12345", passwordSalt1);
            var passwordSalt2 = PasswordUtilities.CreateSalt(16);
            var password2 = PasswordUtilities.GenerateSHA256Hash("335588", passwordSalt2);

            var clients = new List<Client>
            {
                new Client { FirstName = "John", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), DateTo = new DateTime(2016, 12, 31), IdentityCard = "34578644", Email = "john.doe@hotmail.com",
                Password = password1, PasswordSalt = passwordSalt1 },

                new Client { FirstName = "Cristian", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), DateTo = new DateTime(2016, 12, 31), IdentityCard = "34578644", Email = "cristian.pique@hotmail.com",
                Password = password2, PasswordSalt = passwordSalt2 }
            };

            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();
            #endregion

            #region Routines
            var routines = new List<Routine>
            {
                new Routine { ClientID = 1, Description = "Rutina personalizada", Files = new List<File>() },
                new Routine { ClientID = 2, Description = "Rutina gimnasio 5 días a la semana", Files = new List<File>() }
            };

            routines.ForEach(r => context.Routines.Add(r));
            context.SaveChanges();
            #endregion

            #region MedicalRecords
            var medicalRecords = new List<MedicalRecord>
            {
                new MedicalRecord { ClientID = 1, Age = 26, Gender = 'M', Heigth = (float)1.80, Weight = 72 },
                new MedicalRecord { ClientID = 2, Age = 26, Gender = 'M', Heigth = (float)1.81, Weight = 75 },
            };

            medicalRecords.ForEach(m => context.MedicalRecords.Add(m));
            context.SaveChanges();
            #endregion

            #region Activities
            var clientsForActivities = new List<Client>();
            clientsForActivities.Add(clients.Where(x => x.ClientID == 1).FirstOrDefault());
            clientsForActivities.Add(clients.Where(x => x.ClientID == 1).FirstOrDefault());

            var activities = new List<Activity>
            {
                new Activity { Name = "Gimnasio", Description = "Gimnasio, pesas, bicicletas, máquinas para correr"},
                new Activity { Name = "Pilates", Description = "Sistema de entrenamiento físico y mental"},
                new Activity { Name = "Boxeo", Description = "Deporte de combate"}
            };

            activities[0].Clients.Add(clientsForActivities[0]);
            activities[1].Clients.Add(clientsForActivities[1]);
            activities.ForEach(a => context.Activities.Add(a));
            context.SaveChanges();
            #endregion


            #region Payment
            var paymentType = new List<PaymentType>
            {
                new PaymentType { Description = "Pago mensual", Status = Catalog.Status.Active },
                new PaymentType { Description = "Pago anual", Status = Catalog.Status.Active }
            };

            paymentType.ForEach(p => context.PaymentTypes.Add(p));
            context.SaveChanges();
            #endregion

            #region PaymentType
            var payments = new List<Payment>
            {
                new Payment { ClientID = 1, PaymentTypeID = 1, Status = Catalog.Status.Active },
                new Payment { ClientID = 2, PaymentTypeID = 2, Status = Catalog.Status.Active }
            };

            payments.ForEach(p => context.Payments.Add(p));
            context.SaveChanges();
            #endregion

        }

    }
}