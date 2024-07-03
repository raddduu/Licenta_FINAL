using ManageMe.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ManageMe.DataAccess
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService
            <DbContextOptions<ApplicationDbContext>>()))
            {

                if (context.ApplicationRoles.Any())
                {
                    return;
                }

                context.Roles.AddRange(
                new ApplicationRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new ApplicationRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7211", Name = "Dean", NormalizedName = "Dean".ToUpper() },
                new ApplicationRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7212", Name = "Lector", NormalizedName = "Lector".ToUpper() },
                new ApplicationRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7213", Name = "Assistant", NormalizedName = "Assistant".ToUpper() },
                new ApplicationRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7214", Name = "Student", NormalizedName = "Student".ToUpper() },
                new ApplicationRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7215", Name = "Secretary", NormalizedName = "Secretary".ToUpper() }
                );

                var hasher = new PasswordHasher<ApplicationUser>();
                context.ApplicationUsers.AddRange(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb0", // primary key
                    UserName = "admin@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin1!")
                },
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb1", // primary key
                    UserName = "dean@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "DEAN@TEST.COM",
                    Email = "dean@test.com",
                    NormalizedUserName = "DEAN@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Dean1!")
                },
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb2", // primary key
                    UserName = "lector@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "LECTOR@TEST.COM",
                    Email = "lector@test.com",
                    NormalizedUserName = "lector@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Lector1!")
                },
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb3", // primary key
                    UserName = "assistant@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ASSISTANT@TEST.COM",
                    Email = "assistant@test.com",
                    NormalizedUserName = "ASSISTANT@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Assistant1!")
                },
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb4", // primary key
                    UserName = "student@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "STUDENT@TEST.COM",
                    Email = "student@test.com",
                    NormalizedUserName = "STUDENT@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Student1!")
                },
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb5", // primary key
                    UserName = "secretary@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "SECRETARY@TEST.COM",
                    Email = "secretary@test.com",
                    NormalizedUserName = "SECRETARY@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Secretary1!")
                }
                );
                // ASOCIEREA USER-ROLE
                context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7211",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7213",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb3"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7214",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb4"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7215",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb5"
                }
                );

                context.GradingActivities.AddRange(
                    new GradingActivity
                    {
                        //Id = 1,
                        Name = "Course Activity",
                        ActivityId = 4
                    },
                    new GradingActivity
                    {
                        //Id = 2,
                        Name = "Laboratory Activity",
                        ActivityId = 3
                    },
                    new GradingActivity
                    {
                        //Id = 3,
                        Name = "Seminary Activity",
                        ActivityId = 6
                    },
                    new GradingActivity
                    {
                        //Id = 4,
                        Name = "Course Homeworks",
                        ActivityId = 4
                    },
                    new GradingActivity
                    {
                        //Id = 5,
                        Name = "Laboratory Homeworks",
                        ActivityId = 3
                    },
                    new GradingActivity
                    {
                        //Id = 6,
                        Name = "Seminary Homeworks",
                        ActivityId = 6
                    },
                    new GradingActivity
                    {
                        Name = "Course Exam",
                        ActivityId = 4
                    },
                    new GradingActivity
                    {
                        Name = "Laboratory Exam",
                        ActivityId = 3
                    },
                    new GradingActivity
                    {
                        Name = "Seminary Exam",
                        ActivityId = 6
                    }
                );

                Random random = new Random();

                //for (int numberOfStudents = 0; numberOfStudents < 3000; numberOfStudents++)
                //{
                //    var randomNumber = random.Next(1, 100);
                //    var randomFirstName = context.Prenumes.Where(p => p.Id == randomNumber)
                //                                          .Select(p => p.Prenume1).FirstOrDefault();
                //    randomNumber = random.Next(1, 100);
                //    var randomSecondName = context.NumeFamilies.Where(p => p.Id == randomNumber)
                //                                               .Select(p => p.Nume).FirstOrDefault();
                //    Guid newGuidId = Guid.NewGuid();
                //    string guidStringId = newGuidId.ToString();

                //    context.ApplicationUsers.Add(new ApplicationUser
                //    {
                //        Id = guidStringId, 
                //        UserName = $"{randomFirstName}.{randomSecondName}{numberOfStudents}@s.ro",
                //        EmailConfirmed = true,
                //        NormalizedEmail = $"{randomFirstName}.{randomSecondName}{numberOfStudents}@s.ro".ToUpper(),
                //        Email = $"{randomFirstName}.{randomSecondName}{numberOfStudents}@s.ro",
                //        NormalizedUserName = $"{randomFirstName}.{randomSecondName}{numberOfStudents}@s.ro".ToUpper(),
                //        PasswordHash = hasher.HashPassword(null, "Student1!"),
                //        FirstName = randomFirstName,
                //        LastName = randomSecondName
                //    });

                //    context.UserRoles.Add(
                //        new IdentityUserRole<string>
                //        {
                //            RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7214",
                //            UserId = guidStringId
                //        });
                //}

                //for (int numberOfLectors = 0; numberOfLectors < 120; numberOfLectors++)
                //{
                //    var randomNumber = random.Next(1, 100);
                //    var randomFirstName = context.Prenumes.Where(p => p.Id == randomNumber)
                //                                          .Select(p => p.Prenume1).FirstOrDefault();
                //    randomNumber = random.Next(1, 100);
                //    var randomSecondName = context.NumeFamilies.Where(p => p.Id == randomNumber)
                //                                               .Select(p => p.Nume).FirstOrDefault();
                //    Guid newGuidId = Guid.NewGuid();
                //    string guidStringId = newGuidId.ToString();

                //    context.ApplicationUsers.Add(new ApplicationUser
                //    {
                //        Id = guidStringId,
                //        UserName = $"{randomFirstName}.{randomSecondName}{numberOfLectors}@f.ro",
                //        EmailConfirmed = true,
                //        NormalizedEmail = $"{randomFirstName}.{randomSecondName}{numberOfLectors}@f.ro".ToUpper(),
                //        Email = $"{randomFirstName}.{randomSecondName}{numberOfLectors}@f.ro",
                //        NormalizedUserName = $"{randomFirstName}.{randomSecondName}{numberOfLectors}@f.ro".ToUpper(),
                //        PasswordHash = hasher.HashPassword(null, "Lector1!"),
                //        FirstName = randomFirstName,
                //        LastName = randomSecondName
                //    });

                //    context.UserRoles.Add(
                //        new IdentityUserRole<string>
                //        {
                //            RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                //            UserId = guidStringId
                //        });
                //}

                //for (int numberOfAssistants = 0; numberOfAssistants < 60; numberOfAssistants++)
                //{
                //    var randomNumber = random.Next(1, 100);
                //    var randomFirstName = context.Prenumes.Where(p => p.Id == randomNumber)
                //                                          .Select(p => p.Prenume1).FirstOrDefault();
                //    randomNumber = random.Next(1, 100);
                //    var randomSecondName = context.NumeFamilies.Where(p => p.Id == randomNumber)
                //                                               .Select(p => p.Nume).FirstOrDefault();
                //    Guid newGuidId = Guid.NewGuid();
                //    string guidStringId = newGuidId.ToString();

                //    context.ApplicationUsers.Add(new ApplicationUser
                //    {
                //        Id = guidStringId,
                //        UserName = $"{randomFirstName}.{randomSecondName}{numberOfAssistants}@f.ro",
                //        EmailConfirmed = true,
                //        NormalizedEmail = $"{randomFirstName}.{randomSecondName}{numberOfAssistants}@f.ro".ToUpper(),
                //        Email = $"{randomFirstName}.{randomSecondName}{numberOfAssistants}@f.ro",
                //        NormalizedUserName = $"{randomFirstName}.{randomSecondName}{numberOfAssistants}@f.ro".ToUpper(),
                //        PasswordHash = hasher.HashPassword(null, "Assistant1!"),
                //        FirstName = randomFirstName,
                //        LastName = randomSecondName
                //    });

                //    context.UserRoles.Add(
                //        new IdentityUserRole<string>
                //        {
                //            RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7213",
                //            UserId = guidStringId
                //        });
                //}

                //for (int numberOfSecretaries = 0; numberOfSecretaries < 30; numberOfSecretaries++)
                //{
                //    var randomNumber = random.Next(1, 100);
                //    var randomFirstName = context.Prenumes.Where(p => p.Id == randomNumber)
                //                                          .Select(p => p.Prenume1).FirstOrDefault();
                //    randomNumber = random.Next(1, 100);
                //    var randomSecondName = context.NumeFamilies.Where(p => p.Id == randomNumber)
                //                                               .Select(p => p.Nume).FirstOrDefault();
                //    Guid newGuidId = Guid.NewGuid();
                //    string guidStringId = newGuidId.ToString();

                //    context.ApplicationUsers.Add(new ApplicationUser
                //    {
                //        Id = guidStringId,
                //        UserName = $"{randomFirstName}.{randomSecondName}{numberOfSecretaries}@u.ro",
                //        EmailConfirmed = true,
                //        NormalizedEmail = $"{randomFirstName}.{randomSecondName}{numberOfSecretaries}@u.ro".ToUpper(),
                //        Email = $"{randomFirstName}.{randomSecondName}{numberOfSecretaries}@u.ro",
                //        NormalizedUserName = $"{randomFirstName}.{randomSecondName}{numberOfSecretaries}@u.ro".ToUpper(),
                //        PasswordHash = hasher.HashPassword(null, "Secretary1!"),
                //        FirstName = randomFirstName,
                //        LastName = randomSecondName
                //    });

                //    context.UserRoles.Add(
                //        new IdentityUserRole<string>
                //        {
                //            RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7215",
                //            UserId = guidStringId
                //        });
                //}

                //for (int numberOfDeans = 0; numberOfDeans < 5; numberOfDeans++)
                //{
                //    var randomNumber = random.Next(1, 100);
                //    var randomFirstName = context.Prenumes.Where(p => p.Id == randomNumber)
                //                                          .Select(p => p.Prenume1).FirstOrDefault();
                //    randomNumber = random.Next(1, 100);
                //    var randomSecondName = context.NumeFamilies.Where(p => p.Id == randomNumber)
                //                                               .Select(p => p.Nume).FirstOrDefault();
                //    Guid newGuidId = Guid.NewGuid();
                //    string guidStringId = newGuidId.ToString();

                //    context.ApplicationUsers.Add(new ApplicationUser
                //    {
                //        Id = guidStringId,
                //        UserName = $"{randomFirstName}.{randomSecondName}{numberOfDeans}@f.ro",
                //        EmailConfirmed = true,
                //        NormalizedEmail = $"{randomFirstName}.{randomSecondName}{numberOfDeans}@f.ro".ToUpper(),
                //        Email = $"{randomFirstName}.{randomSecondName}{numberOfDeans}@f.ro",
                //        NormalizedUserName = $"{randomFirstName}.{randomSecondName}{numberOfDeans}@f.ro".ToUpper(),
                //        PasswordHash = hasher.HashPassword(null, "Dean1!"),
                //        FirstName = randomFirstName,
                //        LastName = randomSecondName
                //    });

                //    context.UserRoles.Add(
                //        new IdentityUserRole<string>
                //        {
                //            RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7211",
                //            UserId = guidStringId
                //        });
                //}

                context.SaveChanges();

                var queryString = "INSERT INTO dbo.UserRole (UserId, RoleId) SELECT UserId, RoleId FROM dbo.AspNetUserRoles";
                context.Database.ExecuteSqlRaw(queryString);
            }
        }
    }
}
