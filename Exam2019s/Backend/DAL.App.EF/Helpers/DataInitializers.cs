using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class DataInitializers
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
            var quizArr = new[]
            {
                new
                {
                    Name = "JavaScript",
                    Description = "The happy world of runtime errors"
                },
                new
                {
                    Name = "Bootstrap",
                    Description = "Quiz about the most popular HTML, CSS, and JS library in the world."
                },
            };

            Dictionary<string, Quiz> quizDBEntryDictionary = new Dictionary<string, Quiz>();

            foreach (var quiz in quizArr)
            {
                Quiz categoryDBEntry = new Quiz()
                {
                    AppUserId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = quiz.Name,
                    Description = quiz.Description,
                    ChangedBy = "DataInitializer"
                };
                context.AddAsync(categoryDBEntry);

                quizDBEntryDictionary.Add(categoryDBEntry.Name, categoryDBEntry);

            }

            context.SaveChanges();


            var javascriptQuestionArr = new[]
            {
                new
                {
                    QuizId = quizDBEntryDictionary["JavaScript"].Id,
                    OrderNumber = 1,
                    QuestionName = "What is TypeOf Nan",
                    Points = 5,
                },
                new
                {
                    QuizId = quizDBEntryDictionary["JavaScript"].Id,
                    OrderNumber = 2,
                    QuestionName = "The external JavaScript file must contain the <script> tag.",
                    Points = 5,
                },
                new
                {
                    QuizId = quizDBEntryDictionary["JavaScript"].Id,
                    OrderNumber = 3,
                    QuestionName = "Is this valid?" +
                                   "\n for (const [index, element] of foobar.entries()) {" +
                                   "\n  console.log(index, element);" +
                                   "\n}",
                    Points = 5,
                },
                new
                {
                    QuizId = quizDBEntryDictionary["JavaScript"].Id,
                    OrderNumber = 4,
                    QuestionName = "JavaScript case-sensitive?",
                    Points = 5,
                },
            };

            var BSQuestionArr = new[]
            {
                new
                {
                    QuizId = quizDBEntryDictionary["Bootstrap"].Id,
                    OrderNumber = 1,
                    QuestionName =
                        "Which of the following bootstrap style of button indicates a successful or positive action?",
                    Points = 5,
                },
                new
                {
                    QuizId = quizDBEntryDictionary["Bootstrap"].Id,
                    OrderNumber = 2,
                    QuestionName = "Which of the following is correct about Bootstrap Mobile First Strategy?",
                    Points = 5,
                },
                new
                {
                    QuizId = quizDBEntryDictionary["Bootstrap"].Id,
                    OrderNumber = 3,
                    QuestionName =
                        "Which of the following bootstrap styles are used to create a justified tabs navigation?",
                    Points = 5,
                },
                new
                {
                    QuizId = quizDBEntryDictionary["Bootstrap"].Id,
                    OrderNumber = 4,
                    QuestionName = "Grid or Flexbox",
                    Points = 0,
                },
            };

            Dictionary<string, Question> questionDBEntryDictionary = new Dictionary<string, Question>();

            foreach (var question in javascriptQuestionArr)
            {
                Question questionDBEntry = new Question()
                {
                    QuizId = question.QuizId,
                    OrderNumber = question.OrderNumber,
                    QuestionName = question.QuestionName,
                    Points = question.Points,
                    ChangedBy = "DataInitializer"
                };
                context.AddAsync(questionDBEntry);

                questionDBEntryDictionary.Add(questionDBEntry.QuestionName, questionDBEntry);

            }

            foreach (var question in BSQuestionArr)
            {
                Question questionDBEntry = new Question()
                {
                    QuizId = question.QuizId,
                    OrderNumber = question.OrderNumber,
                    QuestionName = question.QuestionName,
                    Points = question.Points,
                    ChangedBy = "DataInitializer"
                };
                context.AddAsync(questionDBEntry);

                questionDBEntryDictionary.Add(questionDBEntry.QuestionName, questionDBEntry);

            }

            context.SaveChanges();

            var javascriptChoiceArr = new[]
            {
                new
                {
                    QuestionId = questionDBEntryDictionary["What is TypeOf Nan"].Id,
                    Name = "NaN",
                    GivesPoints = true
                },
                new
                {
                    QuestionId = questionDBEntryDictionary["What is TypeOf Nan"].Id,
                    Name = "Null",
                    GivesPoints = true
                },
                new
                {
                    QuestionId = questionDBEntryDictionary["What is TypeOf Nan"].Id,
                    Name = "Number",
                    GivesPoints = true
                },
                new
                {
                    QuestionId = questionDBEntryDictionary["What is TypeOf Nan"].Id,
                    Name = "throws error",
                    GivesPoints = true
                },
            };

            foreach (var choice in javascriptChoiceArr)
            {
                Choice questionDBEntry = new Choice()
                {
                    QuestionId = choice.QuestionId,
                    Name = choice.Name,
                    GivesPoints = choice.GivesPoints,
                    ChangedBy = "DataInitializer"
                };
                context.AddAsync(questionDBEntry);
            }
            context.SaveChanges();
        }
    }
}