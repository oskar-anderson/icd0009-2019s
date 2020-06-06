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
                    Name =  "LOC",
                    Description =  "Regular periodic location update",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new GpsLocationType()
                {
                    Name =  "WP",
                    Description =  "Waypoint - temporary location, used as navigation aid",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new GpsLocationType()
                {
                    Name =  "CP",
                    Description =  "Checkpoint - found on terrain the location marked on the paper map",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
            };

            foreach (var gpsLocationType in locationTypes)
            {
                if (!context.GpsLocationTypes.Any(l => l.Id == gpsLocationType.Id))
                {
                    context.GpsLocationTypes.Add(gpsLocationType);
                }
            }

            context.SaveChanges();

            var sessionTypes = new GpsSessionType[]
            {
                new GpsSessionType()
                {
                    Name = "Running - easy",
                    Description =  "Easy normal running-jogging",
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    PaceMin = 6 * 60,
                    PaceMax = 10 * 60
                },
                new GpsSessionType()
                {
                    Name = "Running",
                    Description =  "Running",
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    PaceMin = 5 * 60,
                    PaceMax = 7 * 60
                },
                new GpsSessionType()
                {
                    Name =  "Orienteering - easy",
                    Description =  "Orienteering easy mode - training",
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    PaceMin = 6 * 60,
                    PaceMax = 12 * 60
                },
                new GpsSessionType()
                {
                    Name =  "Orienteering - competition",
                    Description =  "Orienteering competition",
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    PaceMin = 5 * 60,
                    PaceMax = 9 * 60
                },
                new GpsSessionType()
                {
                    Name =  "Bicycle - easy",
                    Description =  "Bicycle easy mode - training",
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    PaceMin = 3 * 60,
                    PaceMax = 6 * 60
                },
                new GpsSessionType()
                {
                    Name =  "Bicycle - competition",
                    Description =  "Bicycle competition",
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    PaceMin = 2 * 60,
                    PaceMax = 5 * 60
                },
            };
            
            foreach (var sessionType in sessionTypes)
            {
                if (!context.GpsSessionTypes.Any(l => l.Id == sessionType.Id))
                {
                    context.GpsSessionTypes.Add(sessionType);
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


            var users = new (string name, string password, string firstName, string lastName, Guid Id)[]
            {
                ("akaver@akaver.com", "Kala.maja2020", "Andres", "Käver", new Guid("00000000-0000-0000-0000-000000000001")),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = userInfo.Id,
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
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }
    }
}