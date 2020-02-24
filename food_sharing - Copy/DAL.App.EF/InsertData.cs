using System.Collections.Generic;
using Domain;

namespace DAL.App.EF
{
    public static class InsertData
    {
        public static void InsertAll()
        {
            // https://stackoverflow.com/questions/38417051/what-goes-into-dbcontextoptions-when-invoking-a-new-dbcontext
            InsertRestaurant();
        }
        
        private static void InsertRestaurant()
        {
            using AppDbContext context = new AppDbContext(AppDbContext.Options);
            
            ICollection<Restaurant> restaurants = new List<Restaurant>();
            
            Restaurant restaurant1 = new Restaurant()
            {
                Name = "Tallinn, Kristiine Pitsa Riina",
                Location = "Endla 76",
                Telephone = "5724871",
                OpenTime = "E-P, 12:00-23:00",
                OpenNotification = "closed on 10., 12. aprill, 1., 31. mai",
            };
            
            Restaurant restaurant2 = new Restaurant()
            {
                Name = "Tallinn, Õismäe Pitsa Riina",
                Location = "Õismäe tee 105",
                Telephone = "5925129",
                OpenTime = "E-N, 12:00-20:00, R-P 12:00-23:00",
                OpenNotification = "closed on 10., 12. aprill, 1., 31. mai",
            };
            
            Restaurant restaurant3 = new Restaurant()
            {
                Name = "Tartu, Fortuuna Pitsa Riina",
                Location = "Fortuuna 1",
                Telephone = "5128737",
                OpenTime = "E-N, 12:00-20:00, R-P 12:00-23:00",
                OpenNotification = "closed on 10., 12. aprill, 1., 31. mai",
            };
            
            restaurants.Add(restaurant1);
            restaurants.Add(restaurant2);
            restaurants.Add(restaurant3);
            
            
            context.Restaurants.Add(restaurant1);
            context.SaveChanges();
        }

        private static void InsertMeal()
        {
            
        }
    }
}