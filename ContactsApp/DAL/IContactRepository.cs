using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ContactsApp.DAL
{
    public interface IContactRepository : IDisposable
    {
        Task<List<Contact>> GetContacts();
        Task<Contact> GetContactByID(int? id);
        void InsertContact(Contact contact);
        void DeleteContact(int? id);
        void UpdateContact(Contact contact);
        Task<int> Save();
    }
}