using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
        }


        public static void SeedData(AppDbContext context)
        {
            // insert predefined location types
            var locationTypes = new GpsLocationType[]
            {
                new GpsLocationType()
                {
                    Name = "LOC",
                    Description = "Regular periodic location update",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new GpsLocationType()
                {
                    Name = "WP",
                    Description = "Waypoint - temporary location, used as navigation aid",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new GpsLocationType()
                {
                    Name = "CP",
                    Description = "Checkpoint - found on terrain the location marked on the paper map",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
            };

            foreach (var gpsLocationType in locationTypes)
            {
                if (!context.LocationTypes.Any(l => l.Id == gpsLocationType.Id))
                {
                    context.LocationTypes.Add(gpsLocationType);
                }
            }

            context.SaveChanges();
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
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }

            }


            var users = new (string name, string password, string firstName, string lastName)[]
            {
                ("akaver@akaver.com", "Kala.maja2020", "Andres", "Käver"),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult  = userManager.AddToRoleAsync(user, "user").Result;
            }
            
        }
    }
}