using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContactsApp.Models;
using ContactsApp.DAL;
using ContactsApp.Helpers;
using ContactsApp.ErrorHelper;
using ContactsApp.ActionFilters;

namespace ContactsApp.Controllers
{
    /// <summary>
    /// Contacts Controller
    /// </summary>
    public class ContactsController : ApiController
    {
        private IContactRepository contactRepository;


        /// <summary>
        /// Initialize Controller
        /// </summary>
        public ContactsController()
        {
            this.contactRepository = new ContactRepository(new ContactsAppContext());
        }


        /// <summary>
        /// List contacts.
        /// <returns>All Contacts in Database</returns>
        /// </summary>
        public async Task<List<Contact>> GetContacts()
        {
            //return await contactRepository.GetContacts();
            var contacts = await contactRepository.GetContacts();
            var contactEntities = contacts as List<Contact> ?? contacts.ToList();
            if (contactEntities.Any())
                return contacts;
            throw new ApiDataException(1000, "Contacts not found", HttpStatusCode.NotFound);
        }
        
        /// <summary>
        /// Lookup Contacts Data by id.
        /// </summary>
        /// <param name="id">Id for contact.</param>
        /// <returns>Contact information of provided Id.</returns>
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> GetContact(int? id)
        {
            if (id != null)
            {
                Contact contact = await contactRepository.GetContactByID(id);
                if (contact != null)
                    return Ok(contact);

                throw new ApiDataException(1001, "No Contact found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        /// <summary>
        /// Update Contact.
        /// </summary>
        /// <param name="id">Id of contact to be updated.</param>
        /// <param name="contact"></param>
        /// <returns>Ok</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Model State Invalid.")),
                    ReasonPhrase = "Model State is invalid. Please check model data."
                };
                throw new HttpResponseException(resp);
            }

            if (id != contact.Id)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Invalid contact id")),
                    ReasonPhrase = "Contact ID not valid."
                };
                throw new HttpResponseException(resp);
            }

            try
            {
                contactRepository.UpdateContact(contact);
                await contactRepository.Save();
            }
            catch (Exception ex)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add Contact.
        /// </summary>
        /// <param name="contact">Provide contact information</param>
        /// <returns></returns>
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Model State Invalid.")),
                    ReasonPhrase = "Model State is invalid. Please check data.",
                };
                throw new HttpResponseException(resp);
                //return BadRequest(ModelState);
            }

            contactRepository.InsertContact(contact);
            await contactRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = contact.Id }, contact);
        }

        /// <summary>
        /// Delete Contact.
        /// </summary>
        /// <param name="id">Provide Id that needs to be deleted.</param>
        /// <returns></returns>
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> DeleteContact(int? id)
        {
            if (id != null && id > 0)
            {
                var contact = await contactRepository.GetContactByID(id);
                if (contact == null)
                {
                    throw new ApiDataException(1002, "Contact is already deleted or not exist in system.", HttpStatusCode.NotFound);
                }
                contactRepository.DeleteContact(id);
                await contactRepository.Save();

                return StatusCode(HttpStatusCode.OK);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        /// <summary>
        /// Dispose repository.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contactRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return contactRepository.GetContactByID(id).Id == id; 
        }
    }
}