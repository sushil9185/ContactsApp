namespace ContactsApp.Migrations
{
    using ContactsApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactsApp.Models.ContactsAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactsApp.Models.ContactsAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Contacts.AddOrUpdate(x => x.Id,
                new Contact()
                {
                    Id = 1,
                    FirstName = "Andrew",
                    LastName = "Peters",
                    Phone = "111-222-3333",
                    Email = "apeters@company.com",
                    Status = true
                },
                    new Contact()
                    {
                        Id = 2,
                        FirstName = "Brice",
                        LastName = "Lambson",
                        Phone = "111-222-3334",
                        Email = "bLambson@company.com",
                        Status = true
                    },
                    new Contact()
                    {
                        Id = 2,
                        FirstName = "Rowan",
                        LastName = "Miller",
                        Phone = "111-222-3335",
                        Email = "rmiller@company.com",
                        Status = true
                    }
                );
        }
    }
}
