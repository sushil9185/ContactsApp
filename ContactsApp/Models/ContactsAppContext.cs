﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactsApp.Models
{
    public class ContactsAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        
        /// <summary>
        /// Contacts App Context
        /// </summary>
        public ContactsAppContext() : base("name=ContactsAppContext")
        {
        }

        /// <summary>
        /// Contact Table
        /// </summary>
        public System.Data.Entity.DbSet<ContactsApp.Models.Contact> Contacts { get; set; }
    
    }
}
