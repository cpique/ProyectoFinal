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

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Assistance> Assistances { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<PaymentTypePrice> PaymentTypePrices { get; set; }

        public DbSet<Product> Products { get; set; }

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
                DateFrom = new DateTime(2016, 09, 01), Email = "john.doe@hotmail.com",
                Password = password1, PasswordSalt = passwordSalt1, Role = Catalog.Roles.Client },

                new Client { FirstName = "Cristian", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "cristian.pique@hotmail.com",
                Password = password2, PasswordSalt = passwordSalt2, Role = Catalog.Roles.Admin }
            };

            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();
            #endregion

            #region Routines
            var routines = new List<Routine>
            {
                new Routine { ClientID = 1, NameFile="CristianPique v1.0", Description = "Rutina personalizada", Files = new List<File>(), CreationDate = DateTime.Now, DaysInWeek=5 },
                new Routine { ClientID = 2, NameFile="CristianPique v1.0", Description = "Rutina gimnasio 5 días a la semana", Files = new List<File>(), CreationDate = DateTime.Now, DaysInWeek=4 }
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

            activities.ForEach(a => context.Activities.Add(a));
            context.SaveChanges();
            #endregion

            #region Files
            var files = new List<File>
            {
                new File { RoutineID=1, ExerciseName="Pecho inclinado", MuscleName="Pecho", Peso="20", Repetitions="10x3" },
                new File { RoutineID=1, ExerciseName="Pecho con mancuernas", MuscleName="Pecho", Peso="8", Repetitions="12x3" },
            };

            files.ForEach(f => context.Files.Add(f));
            context.SaveChanges();
            #endregion

            #region PaymentType
            var paymentType = new List<PaymentType>
            {
                new PaymentType { Description = "Pago mensual", Status = Catalog.Status.Active, ActivityID = 1 },
                new PaymentType { Description = "Pago anual", Status = Catalog.Status.Active, ActivityID = 1 }
            };

            paymentType.ForEach(p => context.PaymentTypes.Add(p));
            context.SaveChanges();
            #endregion

            #region Payment
            var payments = new List<Payment>
            {
                new Payment { ClientID = 1, PaymentTypeID = 1, Status = Catalog.Status.Active, ExpirationDate = new DateTime(2016,12,12) },
                new Payment { ClientID = 2, PaymentTypeID = 2, Status = Catalog.Status.Active, ExpirationDate = new DateTime(2016,12,12) }
            };

            payments.ForEach(p => context.Payments.Add(p));
            context.SaveChanges();
            #endregion

            #region PaymentTypePrices
            var paymentTypePrices = new List<PaymentTypePrice>
            {
                new PaymentTypePrice { DateFrom = new DateTime(2016,01,01), PaymentTypeID = 1, Price = 200 },
                new PaymentTypePrice { DateFrom = new DateTime(2016,05,01), PaymentTypeID = 1, Price = 300 },
                new PaymentTypePrice { DateFrom = new DateTime(2016,01,01), PaymentTypeID = 1, Price = 2000 }
            };

            paymentTypePrices.ForEach(ptp => context.PaymentTypePrices.Add(ptp));
            context.SaveChanges();
            #endregion

            #region Suppliers
            var suppliers = new List<Supplier>
            {
                new Supplier { Address = "Entre Rios 2456", BusinessName = "EuroGym", City="Rosario", Country="Argentina", Email="eurogym@gmail.com", PhoneNumber="(341) 42145580",
                               PostalCode = 2000, WebSite = "http://www.eurogym.com.ar"},
                new Supplier { Address = "Santa Fé Av 1052", BusinessName = "Total Gym", City="Buenos Aires", Country="Argentina", Email="totalgym@productos.com", PhoneNumber="(011) 5445-7189",
                               PostalCode = 1059, WebSite = "http://www.totalgym.com.ar"},
                new Supplier { Address = "V. Cardoso 1401", BusinessName = "e-punto Fitness", City="Ramos Mejia", Country="Argentina", Email="info@e-puntofitness.com.ar", PhoneNumber="(011) 4654-2160",
                               PostalCode = 1059, WebSite = "http://www.e-puntofitness.com.ar/"},
                new Supplier { Address = "La Paz 138", BusinessName = "Distribuidora Boom S.R.L", City="Rosario", Country="Argentina", Email="info@boomventas.com.ar", PhoneNumber="(341) 1564895212",
                               PostalCode = 2000, WebSite = "http://www.boomsrl.com.ar/"},
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();
            #endregion

            #region Products
            var products = new List<Product>
            {
                new Product { Name="Cama de Pilates", Description="Cama de pilates + tabla de salto + tabla de extensión + box Garantía 1 año", Price = 8000, PurchaseDate = new DateTime(2014,05,02),
                              Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=3  },
                new Product { Name="Sillón de cuadriceps", Description="Sillon de cuadriceps c/75kgrs.Linea Exclusive", Price = 25560, PurchaseDate = new DateTime(2015,08,02),
                              Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=3  },
                new Product { Name="Bicicleta Indoor", Description="Transmisión a cadena Volante de inercia de 20Kg. balanceado dinámicamente cromado y pintado en el centro. Asiento prostático. Manubrio regulable en altura.",
                              Price = 6099, PurchaseDate = new DateTime(2015,07,22), Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=3  },
                new Product { Name="Cinta Profesional", Description="Motor 3hp corriente alterna uso industrial 24hs - Velocidad de 0,8 a 16 km/h programable hasta 20km/h- Banda de doble tela antideslizante y antiestática con carbono .",
                              Price = 37550, PurchaseDate = new DateTime(2014,05,02), Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=3  },
                new Product { Name="Complejo Multiestación", Description="El complejo de cuatro estaciones de uso profesional esta compuesto por una Camilla femoral y sillon de cuadriceps con una carga de 50 Kg mas una polea simple alta y baja de 10 regulaciones y 50 Kg de carga mas una pectoralera con 50 Kg de carga y una Dorsalera con 75 kg de carga. ",
                              Price = 97500, PurchaseDate = new DateTime(2014,05,02), Status = Utils.Catalog.ProductStatus.Deteriorated, Type = Utils.Catalog.ProductType.Machine, SupplierID=1  },
                new Product { Name="Polea simple regulable 12 niveles", Description="Polea simple con lingotera de 75 kilos y 12 regulaciones línea profesional Exclusive.",
                              Price = 6512, PurchaseDate = new DateTime(2014,05,02), Status = Utils.Catalog.ProductStatus.Broken, Type = Utils.Catalog.ProductType.Machine, SupplierID=1  },
                new Product { Name="Pectoralera c/75kgrs", Description="Pectoralera mariposa con lingotera de 75 kilos línea profesional Exclusive",
                              Price = 5300, PurchaseDate = new DateTime(2014,05,02), Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=2  },
                new Product { Name="Remo Bajo", Description="Press de pecho con lingotera de 100 kilos línea profesional Exclusive.",
                              Price = 6100, PurchaseDate = new DateTime(2014,05,02), Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=4  },
                new Product { Name="Agua Villavicencio 500ml", Description="Botella de agua mineral Villavicencio", Price = 1050, PurchaseDate = new DateTime(2016,09,02),
                              Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Article, SupplierID=4  },
                new Product { Name="Gatorade 750ml", Description="Gatorade 750ml Naranja", Price = 20, PurchaseDate = new DateTime(2016,09,02),
                              Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Article, SupplierID=4  },
                new Product { Name="Banco Pecho Modulo", Description="Banco regulable que permite ejercicios con barra y camilla de cuadriceps y femoral a discos.",
                              Price = 2300, PurchaseDate = new DateTime(2016,09,02), Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=1  },
                new Product { Name="Banco Pecho Regulable", Description="Banco de pecho regulable standard", Price = 3100, PurchaseDate = new DateTime(2016,09,02),
                              Status = Utils.Catalog.ProductStatus.Ok, Type = Utils.Catalog.ProductType.Machine, SupplierID=2  },
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
            #endregion
        }

    }
}