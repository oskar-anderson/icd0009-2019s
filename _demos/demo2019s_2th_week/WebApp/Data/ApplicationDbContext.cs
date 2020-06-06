using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /*
        dotnet tool install --global dotnet-aspnet-codegenerator
        dotnet tool update --global dotnet-aspnet-codegenerator

        dotnet ef migrations add InitialDbCreation --project WebApp --startup-project WebApp
        dotnet ef database update --project WebApp --startup-project WebApp
        
        run in webApp folder
        dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

        dotnet tool update --global dotnet-ef

        dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
        dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
        dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f

        */

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}