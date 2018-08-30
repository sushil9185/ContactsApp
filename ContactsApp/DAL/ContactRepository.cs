using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactsApp.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ContactsApp.DAL
{
    /// <summary>
    /// Contact Repository implementing IContactRepository and IDisposable
    /// </summary>
    public class ContactRepository : IContactRepository, IDisposable
    {
        /// <summary>
        /// Context
        /// </summary>
        private ContactsAppContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public ContactRepository(ContactsAppContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Add new contact
        /// </summary>
        /// <param name="contact"></param>
        public void InsertContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Contact> GetContactByID(int? id)
        {
            return _context.Contacts.FindAsync(id);
        }

        /// <summary>
        /// Get All Contacts.
        /// </summary>
        /// <returns></returns>
        public Task<List<Contact>> GetContacts()
        {
            return _context.Contacts.ToListAsync();
        }

        /// <summary>
        /// Update Contacts.
        /// </summary>
        /// <param name="student"></param>
        public void UpdateContact(Contact student)
        {
            _context.Entry(student).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete Contact.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteContact(int? id)
        {
            Contact student = _context.Contacts.Find(id);
            _context.Contacts.Remove(student);
        }

        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }

        private bool disposed = false;
        /// <summary>
        /// Clean Up
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Prevent the GC from calling Object.Finalize on an 
            // object that does not require it
            GC.SuppressFinalize(this);
        }
    }
}