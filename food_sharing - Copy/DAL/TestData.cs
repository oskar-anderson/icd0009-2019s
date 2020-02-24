using System;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestData
{
    public class TestData
    {
        public static void Main(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
            
            using ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options);
            
            Restaurant restaurant = new Restaurant()
            {
                Name = "Pitsa Riina",
                Location = "Endla 76",
                Telephone = "5724871",
                OpenTime = "E-P, 12:00-23:00",
                OpenNotification = "closed on 10., 12. aprill, 1., 31. mai",
            };
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
            
            
        }
    }
}