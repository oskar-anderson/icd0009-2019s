using System;
using System.Collections.Generic;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
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
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };
            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };
                    
                    var result = roleManager.CreateAsync(role).Result;
                    if (! result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }
            // todo security
            var users = new (string name, string password, string firstName, string lastName, string phone, Guid Id, int role)[]
            {
                ("akaver@akaver.com", "Kala.maja2020", "Andres", "Käver", "123456789", new Guid("00000000-0000-0000-0000-000000000001"), 0),
                ("hish@hush.com", "Hish.hush2020", "Hish", "Hush", "123456789", new Guid("00000000-0000-0000-0000-000000000002"), 1),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByNameAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser
                    {
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        Phone = userInfo.phone,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }
                else
                {
                    Console.WriteLine($"user {userInfo.name} already exists!");
                }

                if (userInfo.role == 0)
                {
                    var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
                    roleResult = userManager.AddToRoleAsync(user, "User").Result;
                }
                else
                {
                    var roleResult = userManager.AddToRoleAsync(user, "User").Result;
                }
            }
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

            
            foreach (var sizeName in new List<string>{"Suur", "Väike"})
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
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(component);
                Console.WriteLine(component.Id);
            }
            
            
            
            context.SaveChanges();
            

        }
    }
}