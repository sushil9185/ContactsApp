using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactsApp.Controllers;
using ContactsApp.Models;
using ContactsApp.DAL;
using System.Web.Http.Results;
using System.Threading.Tasks;
using Moq;
using System.Web.Http;

namespace ContactsApp.Tests.Controllers
{
    /// <summary>
    /// ontactsControllerTest Methods
    /// </summary>
    [TestClass]
    public class ContactsControllerTest
    {
        private IContactRepository contactRepository;

        public ContactsControllerTest()
        {
            this.contactRepository = new ContactRepository(new ContactsAppContext());
        }

        #region Additional test attributes
         [TestCleanup()]
         public void MyTestCleanup() {
             contactRepository = null;
         }
        
        #endregion


         #region Controller Test Mathods
         [TestMethod]
        public void GetAllContacts_ShouldReturnContacts()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactsController();

            //var result = await controller.GetContacts() as List<Contact>;
            var result =  controller.GetContacts().Result;
            Assert.AreEqual(testContacts.Result.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllContactsAsync_ShouldReturnAllContacts()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactsController();

            var result = await controller.GetContacts() as List<Contact>;
            Assert.AreEqual(testContacts.Result.Count, result.Count);
        }

        [TestMethod]
        public void GetContact_ShouldReturnCorrectContact()
        {
            var testProducts = GetTestContacts();
            var controller = new ContactsController();

            var result = controller.GetContact(4).Result as OkNegotiatedContentResult<Contact>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts.Result[1].FirstName, result.Content.FirstName);
        }

        [TestMethod]
        public async Task GetContactAsync_ShouldReturnCorrectContact()
        {
            var testProducts = GetTestContacts();
            var controller = new ContactsController();
            //var result = await controller.GetContacts() as List<Contact>;
            var result = await controller.GetContact(3) as OkNegotiatedContentResult<Contact>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts.Result[0].FirstName, result.Content.FirstName);
        }

        [TestMethod]
        public void GetContact_ShouldNotFindContact()
        {
            var controller = new ContactsController();

            var result = controller.GetContact(4).Result as List<Contact>;
            Assert.IsNull(result);
            //Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        #endregion


        private async Task<List<Contact>> GetTestContacts()
        {
            var data = await contactRepository.GetContacts();
            return data;
        }
    }
}
