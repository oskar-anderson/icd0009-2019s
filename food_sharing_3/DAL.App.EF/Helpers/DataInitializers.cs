using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Component = Domain.App.Component;

namespace DAL.App.EF.Helpers
{
    public static class Helper
    {
        public static string GetName(Enum value)
        // href: https://www.youtube.com/watch?v=cmZN5tA1HuY
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attr.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute)?.Description;
        }
    }

    
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
        enum CategorysEnum
        {
            Salatid = 0,
            Pastad = 1,
            Pitsad = 2,
            Snäkid = 3,
            Magustoidud = 4,
            Joogid = 5,
        }
        
        enum ComponentsEnum
        {
            Ananass = 0,
            Küüslauk = 1,
            Basiilik = 2,
            Soolakurk = 3,
            Tomat = 4,
            Paprika = 5,
            Juust = 6,
            Sibul = 7,
            Šampinjon = 8,
            Suitsukana = 9,
            Sink = 10,
            Sinihallitusjuust = 11,
            Mozarella = 12,
            Peekon = 13,
            Oliivid = 14,
            Jalapeno = 15,
            Hakkliha = 16,
            Pepperoni = 17,
            Salaami = 18,
            Tuunikala = 19,
            Krevetid = 20,
        }

        enum PizzaTemplatesEnum
        {
            Americana = 0,
            Bolognese = 1,
            Siciliana = 2,
            Topolino = 3,
            Pepperoni = 4,
            Hawaii = 5,
            [Description("Sink ja seened")] HamAndMushrooms = 6,
            Margherita = 7,
            [Description("3 juustu pitsa")] ThreeCheeses = 8,
            Mexican = 9,
            [Description("Kana ja peekon")] ChickenAndBacon = 10,
            Vegetarian = 11
        }
        
        enum PizzaEnum
        {
            [Description("Americana - väike")] AmericanaVäike,
            [Description("Americana - suur")] AmericanaSuur,
            [Description("Bolognese - väike")] BologneseVäike,
            [Description("Bolognese - suur")] BologneseSuur,
            [Description("Siciliana - väike")] SicilianaVäike,
            [Description("Siciliana - suur")] SicilianaSuur,
            [Description("Topolino - väike")] TopolinoVäike,
            [Description("Topolino - suur")] TopolinoSuur,
            [Description("Pepperoni - väike")] PepperoniVäike,
            [Description("Pepperoni - suur")] PepperoniSuur,
            [Description("Hawaii - väike")] HawaiiVäike,
            [Description("Hawaii - suur")] HawaiiSuur,
            [Description("Sink ja seened - väike")] HamAndMushroomsVäike,
            [Description("Sink ja seened - suur")] HamAndMushroomsSuur,
            [Description("Margherita - väike")] MargheritaVäike,
            [Description("Margherita - suur")] MargheritaSuur,
            [Description("3 juustu pizza - väike")] ThreeCheesesVäike,
            [Description("3 juustu pizza - suur")] ThreeCheesesSuur,
            [Description("Mexican - väike")] MexicanVäike,
            [Description("Mexican - suur")] MexicanSuur,
            [Description("Kana ja peekon - väike")] ChickenAndBaconVäike,
            [Description("Kana ja peekon - suur")] ChickenAndBaconSuur,
            [Description("Vegetarian - väike")] VegetarianVäike,
            [Description("Vegetarian - suur")] VegetarianSuur,
        }
        
        enum MealsEnum
        {
            Kanatiivad = 0,
            Friikartulid = 1,
            Leivapulgad = 2,
            [Description("Singi baagel")] SingiBaagel = 3,
            Vahvlid = 4,
            Muffin = 5,
            [Description("Crème brulee")] CremeBrulee = 6,
            Mineraalvesi = 7, 
            Õunamahl = 8,
            Cola = 9,
            Fanta = 10,
            Sprite = 11
        }




        enum RestaurantsEnum
        {
            [Description("Ehitajate Pitsa Riina")] EhitajateTiinaRiina = 0,
            [Description("Endla Pitsa Riina")] EndlaTiinaRiina = 1,
            [Description("Laagna Pitsa Riina")] LaagnaTiinaRiina = 2,
        }

        public static void SeedData(AppDbContext context)
        {
            var categoryArr = new []
            {
                new { 
                    Name = Helper.GetName(CategorysEnum.Salatid), 
                    ForMeal = true, 
                    ForPizzaTemplate = false 
                },
                new { 
                    Name = Helper.GetName(CategorysEnum.Pastad),
                    ForMeal = true, 
                    ForPizzaTemplate = false 
                },
                new { 
                    Name = Helper.GetName(CategorysEnum.Pitsad),
                    ForMeal = false, 
                    ForPizzaTemplate = true
                },
                new { 
                    Name = Helper.GetName(CategorysEnum.Snäkid),
                    ForMeal = true, 
                    ForPizzaTemplate = false 
                },
                new { 
                    Name = Helper.GetName(CategorysEnum.Magustoidud),
                    ForMeal = true, 
                    ForPizzaTemplate = false 
                },
                new { 
                    Name = Helper.GetName(CategorysEnum.Joogid),
                    ForMeal = true, 
                    ForPizzaTemplate = false,
                },
            };

            List<Category> categoryDBEntryList = new List<Category>();
            Dictionary<string, Category> categoryDBEntryDictionary = new Dictionary<string, Category>();

            foreach (var category in categoryArr)
            {
                if (context.Categorys.Any(s => s.Name == category.Name))
                {
                    continue;
                }
                Category categoryDBEntry = new Category()
                {
                    Name = category.Name,
                    ForMeal = category.ForMeal,
                    ForPizzaTemplate = category.ForPizzaTemplate,
                    ChangedBy = "System"
                };
                context.AddAsync(categoryDBEntry);
                
                categoryDBEntryList.Add(categoryDBEntry);
                categoryDBEntryDictionary.Add(categoryDBEntry.Name, categoryDBEntry);
                
            }
            context.SaveChanges();
            
            

            
            
            
            
            var componentArr = new []
            {
                new { 
                    Name = Helper.GetName(ComponentsEnum.Ananass),
                    Gross = 0.6m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Küüslauk),
                    Gross = 0.6m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Basiilik),
                    Gross = 0.6m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Soolakurk),
                    Gross = 0.6m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Tomat),
                    Gross = 0.6m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Paprika),
                    Gross = 0.6m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Juust),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Sibul),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Šampinjon),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Suitsukana),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Sink),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Sinihallitusjuust),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Mozarella),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Peekon),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Oliivid),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Jalapeno),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Hakkliha),
                    Gross = 1.0m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Pepperoni),
                    Gross = 1.5m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Salaami),
                    Gross = 1.5m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Tuunikala),
                    Gross = 1.5m,
                },
                new { 
                    Name = Helper.GetName(ComponentsEnum.Krevetid),
                    Gross = 1.5m,
                },
            };
            
            Dictionary<string, Component> componentDBEntryDictionary = new Dictionary<string, Component>();
            List<Component> componentDBEntryList = new List<Component>();
            
            foreach (var componentObject in componentArr)
            {
                if (context.Components.Any(s => s.Name == componentObject.Name))
                {
                    continue;
                }
                Component component = new Component()
                {
                    Name = componentObject.Name,
                    Gross = componentObject.Gross,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(component);
                
                componentDBEntryDictionary.Add(component.Name, component);
                componentDBEntryList.Add(component);
                
                Console.WriteLine(component.Id);
            }
            
            
            
            
            
            var pizzaTemplateArr = new []
            {
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Americana),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 1,
                    Extras = 4,
                    Description = "Juust, oliivid, ananass",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Bolognese),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 2,
                    Extras = 3,
                    Description = "Juust, hakkliha, jalapeno, paprika",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Siciliana),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 2,
                    Extras = 3,
                    Description = "Juust, sink, küüslauk, sibul",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Topolino),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 2,
                    Extras = 3,
                    Description = "Juust, sink, šampinjonid, ananass",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Pepperoni),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 1,
                    Extras = 4,
                    Description = "Tomat, mozzarella, pepperoni",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Hawaii),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 1,
                    Extras = 4,
                    Description = "Sink, mozzarella, ananass",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.HamAndMushrooms),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 2,
                    Extras = 3,
                    Description = "Mozzarella, sink, küüslauk, šampinionid",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Margherita),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 1,
                    Extras = 4,
                    Description = "Tomat, mozzarella, basiilik",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.ThreeCheeses),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 1,
                    Extras = 4,
                    Description = "Juust, mozarella, sinihallitusjuust",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Mexican),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 3,
                    Extras = 2,
                    Description = "Mozzarella, kana, sibul, tomat, paprika, jalapeno",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.ChickenAndBacon),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 2,
                    Extras = 3,
                    Description = "Kana, mozzarella, peekon, küüslauk",
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Pitsad)].Id,
                    Name = Helper.GetName(PizzaTemplatesEnum.Vegetarian),
                    Picture = Pictures.GetRandomPizzaPicture(),
                    Modifications = 3,
                    Extras = 2,
                    Description = "Mozarella, tomatid, sibul, oliivid, šampinionid",
                },
            };
            
            Dictionary<string, PizzaTemplate> pizzaTemplateDBEntryDictionary = new Dictionary<string, PizzaTemplate>();
            List<PizzaTemplate> pizzaTemplatesDBEntryList = new List<PizzaTemplate>();
            
            foreach (var pizzaTemplateObject in pizzaTemplateArr)
            {
                if (context.PizzaTemplates.Any(s => s.Name == pizzaTemplateObject.Name))
                {
                    continue;
                }
                PizzaTemplate pizzaTemplate = new PizzaTemplate()
                {
                    CategoryId = pizzaTemplateObject.CategoryId,
                    Name = pizzaTemplateObject.Name,
                    Picture = pizzaTemplateObject.Picture,
                    Modifications = pizzaTemplateObject.Modifications,
                    Extras = pizzaTemplateObject.Extras,
                    Description = pizzaTemplateObject.Description,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(pizzaTemplate);
                
                pizzaTemplateDBEntryDictionary.Add(pizzaTemplate.Name, pizzaTemplate);
                pizzaTemplatesDBEntryList.Add(pizzaTemplate);
                
                Console.WriteLine(pizzaTemplate.Id);
            }
            
            context.SaveChanges();
            
            
            
            
            
            
            var componentPizzaTemplateArr = new []
            {
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Juust)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Americana)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Oliivid)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Americana)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Ananass)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Americana)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Juust)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Bolognese)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Hakkliha)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Bolognese)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Jalapeno)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Bolognese)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Paprika)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Bolognese)].Id,
                },

                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Juust)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Siciliana)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sink)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Siciliana)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Küüslauk)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Siciliana)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sibul)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Siciliana)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Juust)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Topolino)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sink)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Topolino)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Šampinjon)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Topolino)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Ananass)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Topolino)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Tomat)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Pepperoni)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Pepperoni)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Pepperoni)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Pepperoni)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sink)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Hawaii)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Hawaii)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Ananass)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Hawaii)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sink)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.HamAndMushrooms)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.HamAndMushrooms)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Küüslauk)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.HamAndMushrooms)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Šampinjon)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.HamAndMushrooms)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Tomat)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Margherita)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Margherita)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Basiilik)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Margherita)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Juust)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ThreeCheeses)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ThreeCheeses)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sinihallitusjuust)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ThreeCheeses)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Suitsukana)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sibul)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Tomat)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Paprika)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Jalapeno)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                },

                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Suitsukana)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ChickenAndBacon)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ChickenAndBacon)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Peekon)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ChickenAndBacon)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Küüslauk)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ChickenAndBacon)].Id,
                },
                
                
                
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Mozarella)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Vegetarian)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Tomat)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Vegetarian)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Sibul)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Vegetarian)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Oliivid)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Vegetarian)].Id,
                },
                new { 
                    ComponentId = componentDBEntryDictionary[Helper.GetName(ComponentsEnum.Šampinjon)].Id,
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Vegetarian)].Id,
                },
            };
                
            
            List<ComponentPizzaTemplate> componentPizzaTemplatesDBEntryList = new List<ComponentPizzaTemplate>();
            
            foreach (var componentPizzaTemplateObject in componentPizzaTemplateArr)
            {
                if (context.ComponentPizzaTPLs.Any(s => 
                    s.ComponentId == componentPizzaTemplateObject.ComponentId &&
                    s.PizzaTemplateId == componentPizzaTemplateObject.PizzaTemplateId))
                {
                    continue;
                }
                ComponentPizzaTemplate componentPizzaTemplate = new ComponentPizzaTemplate()
                {
                    ComponentId = componentPizzaTemplateObject.ComponentId,
                    PizzaTemplateId = componentPizzaTemplateObject.PizzaTemplateId,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(componentPizzaTemplate);
                
                componentPizzaTemplatesDBEntryList.Add(componentPizzaTemplate);
                
                Console.WriteLine(componentPizzaTemplate.Id);
            }
        
            context.SaveChanges();
            
            
            
            
            var pizzaArr = new []
            {
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Americana)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.AmericanaVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Americana)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.AmericanaSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Bolognese)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.BologneseVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Bolognese)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.BologneseSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Siciliana)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.SicilianaVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Siciliana)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.SicilianaSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Topolino)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.TopolinoVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Topolino)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.TopolinoSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Pepperoni)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.PepperoniVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Pepperoni)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.PepperoniSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Hawaii)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.HawaiiVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Hawaii)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.HawaiiSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.HamAndMushrooms)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.HamAndMushroomsVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.HamAndMushrooms)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.HamAndMushroomsSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Margherita)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.MargheritaVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Margherita)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.MargheritaSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ThreeCheeses)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.ThreeCheesesVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ThreeCheeses)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.ThreeCheesesSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.MexicanVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Mexican)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.MexicanSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ChickenAndBacon)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.ChickenAndBaconVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.ChickenAndBacon)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.ChickenAndBaconSuur),
                },
                
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Vegetarian)].Id,
                    SizeNumber = 1,
                    SizeName = "Väike",
                    Name = Helper.GetName(PizzaEnum.VegetarianVäike),
                },
                new { 
                    PizzaTemplateId = pizzaTemplateDBEntryDictionary[Helper.GetName(PizzaTemplatesEnum.Vegetarian)].Id,
                    SizeNumber = 3,
                    SizeName = "Suur",
                    Name = Helper.GetName(PizzaEnum.VegetarianSuur),
                },
            };
            
            Dictionary<string, Pizza> PizzaDBEntryDictionary = new Dictionary<string, Pizza>();
            List<Pizza> PizzaDBEntryList = new List<Pizza>();
            
            foreach (var pizzaObject in pizzaArr)
            {
                if (context.Pizzas.Any(s => s.Name == pizzaObject.Name))
                {
                    continue;
                }
                Pizza pizza = new Pizza()
                {
                    PizzaTemplateId = pizzaObject.PizzaTemplateId,
                    SizeNumber = pizzaObject.SizeNumber,
                    SizeName = pizzaObject.SizeName,
                    Name = pizzaObject.Name,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(pizza);
                
                PizzaDBEntryList.Add(pizza);
                PizzaDBEntryDictionary.Add(pizza.Name, pizza);
                
                Console.WriteLine(pizza.Id);
            }
        
            context.SaveChanges();
            
            
            
            var mealArr = new []
            {
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Snäkid)].Id,
                    Name = Helper.GetName(MealsEnum.Kanatiivad),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = "Tiina lemmikud"
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Snäkid)].Id,
                    Name = Helper.GetName(MealsEnum.Friikartulid),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = "Ahju friikartulid"
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Snäkid)].Id,
                    Name = Helper.GetName(MealsEnum.Leivapulgad),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = "Krõbedad leivapulgad"
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Snäkid)].Id,
                    Name = Helper.GetName(MealsEnum.SingiBaagel),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = "Mozarella-singi saiakesed"
                },
                
                
                
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Magustoidud)].Id,
                    Name = Helper.GetName(MealsEnum.Vahvlid),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = "Vanilli jäätis metsmarjadega"
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Magustoidud)].Id,
                    Name = Helper.GetName(MealsEnum.Muffin),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = "Maitsev muffin rosinatega"
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Magustoidud)].Id,
                    Name = Helper.GetName(MealsEnum.CremeBrulee),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = "Krõbedad leivapulgad"
                },
                
                
                
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Joogid)].Id,
                    Name = Helper.GetName(MealsEnum.Mineraalvesi),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = ""
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Joogid)].Id,
                    Name = Helper.GetName(MealsEnum.Õunamahl),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = ""
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Joogid)].Id,
                    Name = Helper.GetName(MealsEnum.Cola),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = ""
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Joogid)].Id,
                    Name = Helper.GetName(MealsEnum.Fanta),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = ""
                },
                new { 
                    CategoryId = categoryDBEntryDictionary[Helper.GetName(CategorysEnum.Joogid)].Id,
                    Name = Helper.GetName(MealsEnum.Sprite),
                    Picture = "This is an invalid link, no image must be displayed",
                    Description = ""
                },
            };
            
            Dictionary<string, Meal> MealDBEntryDictionary = new Dictionary<string, Meal>();
            List<Meal> MealsDBEntryList = new List<Meal>();
            
            foreach (var mealObject in mealArr)
            {
                if (context.Meals.Any(s => s.Name == mealObject.Name))
                {
                    continue;
                }
                Meal meal = new Meal()
                {
                    CategoryId = mealObject.CategoryId,
                    Name = mealObject.Name,
                    Picture = mealObject.Picture,
                    Description = mealObject.Description,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(meal);
                
                MealsDBEntryList.Add(meal);
                MealDBEntryDictionary.Add(meal.Name, meal);
                
                Console.WriteLine(meal.Id);
            }
        
            context.SaveChanges();
            
            
            
            
            
            var restaurantArr = new []
            {
                new { 
                    Name = Helper.GetName(RestaurantsEnum.EndlaTiinaRiina),
                    Location = "Endla 76",
                    Telephone = "5815834",
                    OpenTime = "E-P, 12:00-22:00",
                    OpenNotification = "Oleme suletud 31. mail"
                },
                new { 
                    Name = Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina),
                    Location = "Ehitajate tee 27",
                    Telephone = "5426816",
                    OpenTime = "E-P, 12:00-22:00",
                    OpenNotification = "Oleme suletud 31. mail"
                },
                new { 
                    Name = Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina),
                    Location = "Virbi 12",
                    Telephone = "5724871",
                    OpenTime = "E-P, 12:00-23:00",
                    OpenNotification = "Oleme suletud 31. mail"
                },
            };
            
            Dictionary<string, Restaurant> RestaurantDBEntryDictionary = new Dictionary<string, Restaurant>();
            List<Restaurant> RestaurantsDBEntryList = new List<Restaurant>();
            
            foreach (var restaurantObject in restaurantArr)
            {
                if (context.Restaurants.Any(s => s.Name == restaurantObject.Name))
                {
                    continue;
                }
                Restaurant restaurant = new Restaurant()
                {
                    Name = restaurantObject.Name,
                    Location = restaurantObject.Location,
                    Telephone = restaurantObject.Telephone,
                    OpenTime = restaurantObject.OpenTime,
                    OpenNotification = restaurantObject.OpenNotification,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(restaurant);
                
                RestaurantsDBEntryList.Add(restaurant);
                RestaurantDBEntryDictionary.Add(restaurant.Name, restaurant);
                
                Console.WriteLine(restaurant.Id);
            }
        
            context.SaveChanges();
            
            
            
            List<RestaurantFood> restaurantFoodsList = new List<RestaurantFood>();
            
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Kanatiivad)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Friikartulid)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 3.2m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Leivapulgad)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 0.6m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.SingiBaagel)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 1.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Vahvlid)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 2.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Muffin)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 1.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.CremeBrulee)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Mineraalvesi)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Õunamahl)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Cola)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Fanta)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Sprite)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );

            
            
            
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.AmericanaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.AmericanaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.BologneseVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.BologneseSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.SicilianaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.SicilianaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.TopolinoVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.TopolinoSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.PepperoniVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.PepperoniSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HawaiiVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HawaiiSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HamAndMushroomsVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HamAndMushroomsSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MargheritaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 3.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MargheritaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 6.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ThreeCheesesVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ThreeCheesesSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MexicanVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MexicanSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ChickenAndBaconVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ChickenAndBaconSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.VegetarianVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 3.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.VegetarianSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EhitajateTiinaRiina)].Id,
                    Gross = 6.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            
            
            
            
            
            
            
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Kanatiivad)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Friikartulid)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 3.2m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Leivapulgad)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 0.6m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.SingiBaagel)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 1.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Vahvlid)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 2.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Muffin)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 1.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.CremeBrulee)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Mineraalvesi)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Õunamahl)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Cola)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Fanta)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Sprite)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            
            
            
            
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.AmericanaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.AmericanaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.BologneseVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.BologneseSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.SicilianaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.SicilianaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.TopolinoVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.TopolinoSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.PepperoniVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.PepperoniSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HawaiiVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HawaiiSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HamAndMushroomsVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HamAndMushroomsSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MargheritaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 3.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MargheritaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 6.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ThreeCheesesVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ThreeCheesesSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MexicanVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MexicanSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ChickenAndBaconVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ChickenAndBaconSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.VegetarianVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 3.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.VegetarianSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.EndlaTiinaRiina)].Id,
                    Gross = 6.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            
            
            
            
            
            
            
            
            
            
            
            
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Kanatiivad)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Friikartulid)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 3.2m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Leivapulgad)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 0.6m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.SingiBaagel)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 1.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Vahvlid)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 2.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Muffin)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 1.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.CremeBrulee)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.9m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Mineraalvesi)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Õunamahl)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Cola)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood() {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Fanta)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                });
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = MealDBEntryDictionary[Helper.GetName(MealsEnum.Sprite)].Id,
                    PizzaId = null,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 1.5m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            
            
            
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.AmericanaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.AmericanaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.BologneseVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.BologneseSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.SicilianaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.SicilianaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.TopolinoVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.TopolinoSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.PepperoniVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.PepperoniSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HawaiiVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HawaiiSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HamAndMushroomsVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.HamAndMushroomsSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.4m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MargheritaVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 3.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MargheritaSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 6.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ThreeCheesesVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ThreeCheesesSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MexicanVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.MexicanSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ChickenAndBaconVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 4.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.ChickenAndBaconSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 7.3m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.VegetarianVäike)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 3.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            restaurantFoodsList.Add(
                new RestaurantFood()
                {
                    MealId = null,
                    PizzaId = PizzaDBEntryDictionary[Helper.GetName(PizzaEnum.VegetarianSuur)].Id,
                    RestaurantId = RestaurantDBEntryDictionary[Helper.GetName(RestaurantsEnum.LaagnaTiinaRiina)].Id,
                    Gross = 6.7m,
                    Since = DateTime.Now,
                    Until = new DateTime(9999, 1, 1),
                }
            );
            
            
            List<RestaurantFood> RestaurantFoodsDBEntryList = new List<RestaurantFood>();
            
            foreach (var restaurantFoodObject in restaurantFoodsList)
            {
                RestaurantFood restaurantFood = new RestaurantFood()
                {
                    MealId = restaurantFoodObject.MealId,
                    PizzaId = restaurantFoodObject.PizzaId,
                    RestaurantId = restaurantFoodObject.RestaurantId,
                    Gross = restaurantFoodObject.Gross,
                    Since = restaurantFoodObject.Since,
                    Until = restaurantFoodObject.Until,
                    CreatedAt = DateTime.Now,
                    ChangedAt = new DateTime(2020, 2, 20),
                    ChangedBy = "System"
                };
                context.AddAsync(restaurantFood);
                
                RestaurantFoodsDBEntryList.Add(restaurantFood);

                
                Console.WriteLine(restaurantFood.Id);
            }
        
            context.SaveChanges();

                    
                    
                    
            
            
            
            
            // https://realityratings.com/media/reviews/photos/original/ae/c9/a9/112815904-stock-vector-no-image-available-icon-flat-vector-illustration-42-1563401801.jpg
            
            
            /*
             Americana
             Bolognese
             Siciliana
             Topolino
             Pepperoni
             
             
             Väike Americana
             5€
             juust, salaami, ananass, oliivid
             
             Väike Bolognese
             6€
             hakkliha, sibul, küüslauk, mozarella, juust  
             
             Väike Siciliana
             5€
             juust, sink, küüslauk, sibul
             
             Väike Topolino
             5.5€
             juust, sink, šampinjonid, ananass
             
             Väike Pepperoni
             5€
             juust, kaste, pepperoni, basiilik, mozzarella, oliivid
             
             Keskmine Americana
             7.7€
             juust, salaami, ananass, oliivid
             
             Keskmine Bolognese
             7.7€
             hakkliha, sibul, küüslauk, mozarella, juust  
             
             Keskmine Siciliana
             6.7€
             juust, sink, küüslauk, sibul
             
             Keskmine Topolino
             7€
             juust, sink, šampinjonid, ananass
             
             Keskmine Pepperoni
             6.7€
             juust, kaste, pepperoni, basiilik, mozzarella, oliivid
             
             Suur Americana
             8€
             juust, salaami, ananass, oliivid
             
             Suur Bolognese
             9€
             hakkliha, sibul, küüslauk, mozarella, juust  
             
             Suur Siciliana
             8€
             juust, sink, küüslauk, sibul
             
             Suur Topolino
             8.2€
             juust, sink, šampinjonid, ananass
             
             Suur Pepperoni
             8€
             juust, kaste, pepperoni, basiilik, mozzarella, oliivid
             
             */

        }
    }
}