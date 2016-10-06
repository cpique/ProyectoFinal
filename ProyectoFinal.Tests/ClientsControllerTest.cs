using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using ProyectoFinal.Models.Repositories;
using ProyectoFinal.Models;
using System.Collections.Generic;
using ProyectoFinal.Utils;
using ProyectoFinal.Controllers;
using System.Web.Mvc;
using System.Linq;
using System.Configuration;

namespace ProyectoFinal.Tests
{
    [TestClass]
    public class ClientsControllerTest
    {
        List<Client> clients;
        List<Client> massiveClients;
        Client newClient;
        IClientRepository clientRepository;
        ClientsController controller;

        [TestInitialize]
        public void Init()
        {
            #region Dummy Clients List
            var passwordSalt1 = PasswordUtilities.CreateSalt(16);
            var password1 = PasswordUtilities.GenerateSHA256Hash("12345", passwordSalt1);
            var passwordSalt2 = PasswordUtilities.CreateSalt(16);
            var password2 = PasswordUtilities.GenerateSHA256Hash("335588", passwordSalt2);

            clients = new List<Client>
            {
                new Client { ClientID=1, FirstName = "John", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "john.doe@hotmail.com",
                Password = password1, PasswordSalt = passwordSalt1 },

                new Client { ClientID=2, FirstName = "Cristian", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "cristian.pique@hotmail.com",
                Password = password2, PasswordSalt = passwordSalt2 },

                new Client { ClientID=3, FirstName = "Ted", LastName = "Mosby", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1985, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "ted.mosby@gmail.com",
                Password = password2, PasswordSalt = passwordSalt2 }
            };
            #endregion

            #region Dummy IsolatedClient
            var passwordSalt = PasswordUtilities.CreateSalt(16);
            newClient = new Client
            {
                ClientID = 50,
                FirstName = "John",
                LastName = "Doe",
                DocType = "DNI",
                DocNumber = 34578800,
                BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01),
                Email = "john.doe.test@hotmail.com",
                Password = PasswordUtilities.GenerateSHA256Hash("test", passwordSalt),
                PasswordSalt = passwordSalt
            };
            #endregion

            #region Dummy MassiveClientsListForIndexTests List
            var passwordSalt0 = PasswordUtilities.CreateSalt(16);
            var password0 = PasswordUtilities.GenerateSHA256Hash("12345", passwordSalt0);

            massiveClients = new List<Client>
            {
                new Client { ClientID=1, FirstName = "Alejandra", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "john.doe@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=2, FirstName = "Alberto", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "cristian.pique@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=3, FirstName = "Antonio", LastName = "Mosby", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1985, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "ted.mosby@gmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=1, FirstName = "Cristian", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "john.doe@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=2, FirstName = "Carlos", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "cristian.pique@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=3, FirstName = "Cecilia", LastName = "Mosby", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1985, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "ted.mosby@gmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=1, FirstName = "John", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "john.doe@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=2, FirstName = "Juan", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "cristian.pique@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=3, FirstName = "Ted", LastName = "Mosby", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1985, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "ted.mosby@gmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=1, FirstName = "Maria", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "john.doe@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=2, FirstName = "Martina", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "cristian.pique@hotmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 },

                new Client { ClientID=3, FirstName = "Zurdo", LastName = "Mosby", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1985, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), Email = "ted.mosby@gmail.com",
                Password = passwordSalt0, PasswordSalt = passwordSalt0 }
            };
            #endregion

            #region Repositories
            clientRepository = Mock.Create<IClientRepository>();
            #endregion

            #region Controller creation
            controller = new ClientsController(clientRepository);
            #endregion

            #region JustMock Arranges
            Mock.Arrange(() => clientRepository.GetClients())
                .Returns(clients)
                .MustBeCalled();

            Mock.Arrange(() => clientRepository.GetClientByID(1))
                .Returns(clients.FirstOrDefault())
                .MustBeCalled();

            Mock.Arrange(() => clientRepository.InsertClient(newClient))
                .DoInstead(() => clients.Add(newClient))
                .MustBeCalled();

            Mock.Arrange(() => clientRepository.Save()).DoNothing();

            Mock.Arrange(() => clientRepository.DeleteClient(clients.FirstOrDefault().ClientID))
                .DoInstead(() => clients.Remove(clients.FirstOrDefault()))
                .MustBeCalled();

            Mock.Arrange(() => clientRepository.UpdateClient(new Client())).DoNothing();

            Mock.Arrange(() => clientRepository.IsEmailAlreadyInUse(newClient))
                .Returns(() => this.IsEmailAlreadyInUse(newClient, false))
                .MustBeCalled();
            #endregion
        }

        [TestMethod]
        public void GetClients()
        {
            //Arrange //Act
            ViewResult viewResult = controller.Index(string.Empty,string.Empty, string.Empty, 1) as ViewResult;
            var model = viewResult.Model as IEnumerable<Client>;

            //Assert
            Assert.AreEqual(3, model.Count());
            Assert.AreEqual("John", model.First().FirstName);
            Assert.IsTrue(model.Last().ClientID.GetType() == typeof(int));
        }

        [TestMethod]
        public void GetClientByID()
        {
            ViewResult viewResult = controller.Details(1) as ViewResult;
            var model = viewResult.Model as Client;

            //Assert
            Assert.AreNotEqual(null, model);
            Assert.AreEqual("Doe", model.LastName);
        }

        [TestMethod]
        public void Create()
        {
            int totalClientsBefore = clients.Count;

            ActionResult actionResult = controller.Create(newClient);

            //Assert
            Assert.AreNotEqual(totalClientsBefore, clients.Count);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void DeleteClient()
        {
            //Arrange 
            int totalClientsBefore = clients.Count;
            var idToDelete = clients.FirstOrDefault().ClientID;

            controller.DeleteConfirmed(clients.FirstOrDefault().ClientID);

            //Assert
            Assert.IsTrue(totalClientsBefore > clients.Count);
            Assert.IsFalse(clients.Any(c => c.ClientID == idToDelete));
        }

        [TestMethod]
        public void UpdateClient()
        {
            //Arrange 
            Client clientToUpdate = clients.FirstOrDefault();
            var originalName = clients.FirstOrDefault().FirstName;
            var originalLastName = clients.FirstOrDefault().LastName;

            Mock.Arrange(() => clientRepository.Save()) //Override Save arrange in Init()
                .DoInstead(() => { clientToUpdate.LastName = "Changed"; clientToUpdate.FirstName = "Changed!"; })
                .MustBeCalled();
            
            controller.Edit(clientToUpdate);
            
            //Assert
            Assert.IsFalse(clientToUpdate.FirstName == originalName);
            Assert.IsFalse(clientToUpdate.LastName == originalLastName);
        }

        [TestMethod]
        public void HashPasswordAndCompareWithAttemptedPasswords()
        {
            var passSaltInDb = PasswordUtilities.CreateSalt(16);
            var passHashInDb = PasswordUtilities.GenerateSHA256Hash("TestingPassword", passSaltInDb);
            
            Assert.IsFalse(PasswordUtilities.Compare("TestingPa$$word", passHashInDb, passSaltInDb));
            Assert.IsFalse(PasswordUtilities.Compare("Testing Password", passHashInDb, passSaltInDb));
            Assert.IsFalse(PasswordUtilities.Compare(string.Empty, passHashInDb, passSaltInDb));
            Assert.IsFalse(PasswordUtilities.Compare(null, passHashInDb, passSaltInDb));
            Assert.IsTrue(PasswordUtilities.Compare("TestingPassword", passHashInDb, passSaltInDb));
        }

        [TestMethod]
        public void EmailAlreadyInUseForCreate()
        {
            //Arrange 
            int totalClientsBefore = clients.Count;
            newClient.Email = clients.FirstOrDefault().Email; //Email already in use

            //Act
            controller.Create(newClient);

            //Assert
            Assert.IsTrue(totalClientsBefore  == clients.Count());
        }

        [TestMethod]
        public void EmailNotInUseForCreate()
        {
            //Arrange 
            int totalClientsBefore = clients.Count;

            //Act
            controller.Create(newClient);

            //Assert
            Assert.IsTrue(totalClientsBefore < clients.Count());
        }

        [TestMethod]
        public void EmailAlreadyInUseForEdit()
        {
            //Arrange //Act
            var client = clients.FirstOrDefault();
            var originalName = clients.FirstOrDefault().FirstName;
            var originalLastName = clients.FirstOrDefault().LastName;
            client.Email = clients.Last().Email; //Email already in use
            Mock.Arrange(() => clientRepository.Save()) //Override Save arrange in Init()
                .DoInstead(() => { client.LastName = "Changed"; client.FirstName = "Changed!"; })
                .MustBeCalled();
            Mock.Arrange(() => clientRepository.IsEmailAlreadyInUse(client)) //Override
                .Returns(() => this.IsEmailAlreadyInUse(client, true))
                .MustBeCalled();

            controller.Edit(client);

            Assert.IsTrue(client.FirstName == originalName);
            Assert.IsTrue(client.LastName == originalLastName);
        }

        [TestMethod]
        public void EmailNotInUseForEdit()
        {
            //Arrange //Act
            var client = clients.FirstOrDefault();
            var originalName = clients.FirstOrDefault().FirstName;
            var originalLastName = clients.FirstOrDefault().LastName;
            Mock.Arrange(() => clientRepository.IsEmailAlreadyInUse(client)) //Override
                .Returns(() => this.IsEmailAlreadyInUse(client, true))
                .MustBeCalled();
            Mock.Arrange(() => clientRepository.Save()) //Override
                .DoInstead(() => { client.LastName = "Changed"; client.FirstName = "Changed!"; })
                .MustBeCalled();

            controller.Edit(client);

            Assert.IsFalse(client.FirstName == originalName);
            Assert.IsFalse(client.LastName == originalLastName);
        }

        #region Index: searchString & sortOrder
        [TestMethod]
        public void IndexWithSearch()
        {
            //Arrange //Act
            const string SEARCHSTRING = "Alejandra";
            var itemsFound = massiveClients.Where(c => string.Concat(c.FirstName, " ", c.LastName)
                                                 .ToLower()
                                                 .Contains(SEARCHSTRING.ToLower()));
            Mock.Arrange(() => clientRepository.GetClients()) //Override
                .Returns(massiveClients)
                .MustBeCalled();

            ViewResult viewResult = controller.Index(string.Empty, string.Empty, SEARCHSTRING, 1) as ViewResult;
            var model = viewResult.Model as IEnumerable<Client>;

            //Assert
            Assert.AreEqual(itemsFound.Count(), model.Count());
            Assert.IsFalse(model.Any(c => !string.Concat(c.FirstName, " ", c.LastName).Contains(SEARCHSTRING)));
        }

        [TestMethod]
        public void IndexWithSortOrder()
        {
            //Arrange //Act
            //Estas constantes van de la mano, y si se cambian requieren cambios en linea 389
            const string SORT_ORDER_DESC = "name_desc"; //change for name_asc if necessary
            const string SORT_MODE_DESC = "desc"; //change for asc if necessary

            Mock.Arrange(() => clientRepository.GetClients()) //Override
                .Returns(massiveClients)
                .MustBeCalled();

            ViewResult viewResult = controller.Index(SORT_ORDER_DESC, string.Empty, string.Empty, 1) as ViewResult;
            var model = viewResult.Model as IEnumerable<Client>;

            if (SORT_MODE_DESC == "desc")
                massiveClients = massiveClients.OrderByDescending(c => c.FirstName).ToList();

            //Assert
            Assert.IsNotNull(model);
            Assert.IsTrue(massiveClients.FirstOrDefault().Equals(model.FirstOrDefault()));
            Assert.AreEqual(massiveClients.Take(model.Count()).Last().FirstName, model.Last().FirstName);
        }
        #endregion

        #region Private Methods
        private bool IsEmailAlreadyInUse(Client client, bool IsEditTheCaller)
        {
            bool isEditTheCaller = IsEditTheCaller;

            if (isEditTheCaller) //Si viene de Edit, debo permitirle guardar el email que ya tenía anteriormente
            {
                return clients.Where(c => c.ClientID != client.ClientID)
                              .Any(c => c.Email.ToLower() == client.Email.ToLower());
            }
            else
            {
                return clients.Any(c => c.Email.ToLower() == client.Email.ToLower());
            }
        }
        #endregion
    }
}
