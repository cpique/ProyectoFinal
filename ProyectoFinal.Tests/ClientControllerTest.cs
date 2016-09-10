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

namespace ProyectoFinal.Tests
{
    [TestClass]
    public class ClientControllerTest
    {
        List<Client> clients;

        [TestInitialize]
        public void Init()
        {
            var passwordSalt1 = PasswordUtilities.CreateSalt(16);
            var password1 = PasswordUtilities.GenerateSHA256Hash("12345", passwordSalt1);
            var passwordSalt2 = PasswordUtilities.CreateSalt(16);
            var password2 = PasswordUtilities.GenerateSHA256Hash("335588", passwordSalt2);

            clients = new List<Client>
            {
                new Client { ClientID=1, FirstName = "John", LastName = "Doe", DocType = "DNI", DocNumber = 34578800, BirthDate = new DateTime(1990, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), DateTo = new DateTime(2016, 12, 31), IdentityCard = "34578644", Email = "john.doe@hotmail.com",
                Password = password1, PasswordSalt = passwordSalt1 },

                new Client { ClientID=2, FirstName = "Cristian", LastName = "Piqué", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1989, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), DateTo = new DateTime(2016, 12, 31), IdentityCard = "34578644", Email = "cristian.pique@hotmail.com",
                Password = password2, PasswordSalt = passwordSalt2 },

                new Client { ClientID=3, FirstName = "Ted", LastName = "Mosby", DocType = "DNI", DocNumber = 34578644, BirthDate = new DateTime(1985, 12, 31),
                DateFrom = new DateTime(2016, 09, 01), DateTo = new DateTime(2017, 12, 31), IdentityCard = "30000000", Email = "ted.mosby@gmail.com",
                Password = password2, PasswordSalt = passwordSalt2 }
            };
        }

        [TestMethod]
        public void GetClients()
        {
            //Arrange //Act
            var clientRepository = Mock.Create<IClientRepository>();
            Mock.Arrange(() => clientRepository.GetClients())
                .Returns(clients)
                .MustBeCalled();

            ClientsController controller = new ClientsController(clientRepository);
            ActionResult actionResult = controller.Index();
            ViewResult viewResult = actionResult as ViewResult;
            var model = viewResult.Model as IEnumerable<Client>;

            //Assert
            Assert.AreEqual(3, model.Count());
            Assert.AreEqual("John", model.First().FirstName);
            Assert.IsTrue(model.Last().ClientID.GetType() == typeof(int));
        }

        [TestMethod]
        public void GetClientByID()
        {
            //Arrange //Act
            var clientRepository = Mock.Create<IClientRepository>();
            Mock.Arrange(() => clientRepository.GetClientByID(1))
                .Returns(clients.FirstOrDefault())
                .MustBeCalled();

            ClientsController controller = new ClientsController(clientRepository);
            ActionResult actionResult = controller.Details(1);
            ViewResult viewResult = actionResult as ViewResult;
            var model = viewResult.Model as Client;

            //Assert
            Assert.AreNotEqual(null, model);
            Assert.AreEqual("Doe", model.LastName);
        }

        [TestMethod]
        public void Create()
        {
        }

        [TestMethod]
        public void DeleteClient()
        { }

        [TestMethod]
        public void UpdateClient()
        { }

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
    }
}
