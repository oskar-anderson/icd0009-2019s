using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleNames = new string[] {"User", "Admin"};
            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole();
                    role.Name = roleName;
                    var result = roleManager.CreateAsync(role).Result;
                    if (! result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }

            var userName = "akaver2@akaver.com";
            var passWord = "Kala.maja.2020";
            var firstName = "Andres";
            var lastName = "Käver";
            var phone = "123456789";

            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser();
                user.Email = userName;
                user.UserName = userName;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Phone = phone;
                var result = userManager.CreateAsync(user, passWord).Result;
                if (! result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }
            }
            else
            {
                Console.WriteLine(" user " + userName + " already exists!");
            }

            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "User").Result;

        }

        public static void SeedData(AppDbContext context)
        {
            foreach (var categoryName in new List<string>
            {
                "Salatid",
                "Pastad",
                "Pitsad",
                "Snäkid",
                "Magustoidud",
                "Alkohoolsed joogid",
                "Alkoholi-vabad joogid",
            })
            {
                if (context.Categories.Any(s => s.Name == categoryName))
                {
                    continue;
                }
                Category size = new Category()
                {
                    Name = categoryName,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                
                };
                context.AddAsync(size);
            }
            context.SaveChanges();

            
            foreach (var sizeName in new List<string>{"Suur", "Keskmine", "Väike"})
            {
                if (context.Sizes.Any(s => s.Name == sizeName))
                {
                    continue;
                }
                Size size = new Size()
                {
                    Name = sizeName,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                
                };
                context.AddAsync(size);
            }
            context.SaveChanges();
            
            foreach (var bank in new List<string>{"Swedbank", "SEB", "Coop", "Luminor"})
            {
                if (context.PaymentMethods.Any(s => s.Name == bank))
                {
                    continue;
                }
                PaymentMethod paymentMethod = new PaymentMethod()
                {
                    Name = bank,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                
                };
                context.AddAsync(paymentMethod);
            }
            context.SaveChanges();
            
            
            foreach (var componentName in new List<string>{
                "Ananass",
                "Basiilik",
                "Jalapeno",
                "Mais",
                "Marineeritud kurk",
                "Mustad oliivid",
                "Paprika",
                "Peekon",
                "Punane sibul",
                "Salaami",
                "Sinihallitusjuust",
                "Tomat",
                "Vorst",
                "Hakkliha",
                "Juust",
                "Suitsukana",
                "Šampinjonid",
                "Tex-mex kaste",
                "BBQ kaste",
                "Mozarella juust",
                "Tuunikala",
                "Krevetid"
                })
            {
                if (context.Components.Any(s => s.Name == componentName))
                {
                    continue;
                }
                Component component = new Component()
                {
                    Name = componentName,
                    Max = 3,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(component);
            }
            context.SaveChanges();
            

        }
    }
}