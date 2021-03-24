using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPX.Data;
using SPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week2_IdentitySystem.Data
{
    public static class DbInitializer
    {
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1;  // should log an error message here

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
                return 2;  // should log an error message here

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  // should log an error message here

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0)
                return 4;  // should log an error message here

            return 0;
        }

        public static async Task<int> SeedData(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            // Seed Sport Categories
            if (!context.SportCategories.Any())
            {
                var categories = new List<SportCategory> {
                    new SportCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Soccer"
                    },
                    new SportCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Basketball"
                    },
                    new SportCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cricket"
                    }
                };
                await context.SportCategories.AddRangeAsync(categories);
                var result = await context.SaveChangesAsync();
            }

            // Seed Leagues 
            if (!context.Leagues.Any())
            {
                var leagues = new List<League> {
                    new League
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bundesliga",
                        Description = "Bundesliga is a professional association football league in Germany. At the top of the German football league system, the Bundesliga is Germany's primary football competition."
                    },
                    new League
                    {
                        Id = Guid.NewGuid(),
                        Name = "La Liga",
                        Description = "La Liga is the men's top professional football division of the Spanish football league system."
                    },
                    new League
                    {
                        Id = Guid.NewGuid(),
                        Name = "NBA",
                        Description = "The National Basketball Association is a professional basketball league in North America. The league is composed of 30 teams and is one of the four major professional sports leagues in the United States and Canada. It is the premier men's professional basketball league in the world."
                    },
                };
                await context.Leagues.AddRangeAsync(leagues);
                await context.SaveChangesAsync();
            }

            // Seed Teams
            if (!context.Teams.Any())
            {
                var laliga_league = (from league in context.Leagues
                                     where league.Name == "La Liga"
                                     select league).FirstOrDefault();
                var teams = new List<Team> {
                    new Team
                    {
                        Id = Guid.NewGuid(),
                        Name = "FC Barcelona",
                        Description = "Barcelona and colloquially known as Barça, is a Spanish professional football club based in Barcelona, that competes in La Liga, the top flight of Spanish football.",
                        League = laliga_league
                    },
                    new Team
                    {
                        Id = Guid.NewGuid(),
                        Name = "FC Bayern Munich",
                        Description = "Bayern Munich is a German professional sports club based in Munich, Bavaria. It is best known for its professional football team, which plays in the Bundesliga, the top tier of the German football league system."
                    },
                    new Team
                    {
                        Id = Guid.NewGuid(),
                        Name = "Borussia Dortmund",
                        Description = "Borussia Dortmund, BVB, or simply Dortmund, is a German professional sports club based in Dortmund, North Rhine-Westphalia."
                    },

                };
                await context.Teams.AddRangeAsync(teams);
                await context.SaveChangesAsync();
            }

            if (!context.Buckets.Any())
            {
                var buckets = new List<Bucket>
                {
                    new Bucket
                    {
                        Id = Guid.NewGuid(),
                        Name = "Top 3 Soccer Teams",
                        Description = "The 3 best performers in the Soccer sport",
                        Price = 2000
                    }
                };
                await context.Buckets.AddRangeAsync(buckets);
                await context.SaveChangesAsync();
            }

            if (!context.BucketTeamConnections.Any())
            {
                var soccer_bucket = (from bucket in context.Buckets
                                     where bucket.Name == "Top 3 Soccer Teams"
                                     select bucket).FirstOrDefault();
                var barca_team = (from team in context.Teams
                             where team.Name == "FC Barcelona"
                             select team).FirstOrDefault();
                var dortmund_team = (from team in context.Teams
                                  where team.Name == "Borussia Dortmund"
                                  select team).FirstOrDefault();
                var bayern_team = (from team in context.Teams
                                     where team.Name == "FC Bayern Munich"
                                     select team).FirstOrDefault();

                var bucketTeamConnections = new List<BucketTeamConnection>
                {
                    new BucketTeamConnection
                    {
                        Id = Guid.NewGuid(),
                        Bucket = soccer_bucket,
                        Team = barca_team
                    },
                    new BucketTeamConnection
                    {
                        Id = Guid.NewGuid(),
                        Bucket = soccer_bucket,
                        Team = dortmund_team
                    },
                    new BucketTeamConnection
                    {
                        Id = Guid.NewGuid(),
                        Bucket = soccer_bucket,
                        Team = bayern_team
                    },
                };

                await context.BucketTeamConnections.AddRangeAsync(bucketTeamConnections);
                var result = await context.SaveChangesAsync();
            }

            return 0;
        }

        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin Role
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Create Member Role
            result = await roleManager.CreateAsync(new IdentityRole("Investor"));
            if (!result.Succeeded)
                return 2;  // should log an error message here

            return 0;
        }

        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Admin User
            var adminUser = new ApplicationUser
            {
                UserName = "the.admin@spx.ca",
                Email = "the.admin@spx.ca",
                FirstName = "The",
                LastName = "Admin",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, "Password!1");
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Assign user to Admin role
            result = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!result.Succeeded)
                return 2;  // should log an error message here

            // Create Member User
            var memberUser = new ApplicationUser
            {
                UserName = "the.investor@spx.ca",
                Email = "the.investor@spx.ca",
                FirstName = "The",
                LastName = "Investor",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(memberUser, "Password!1");
            if (!result.Succeeded)
                return 3;  // should log an error message here

            // Assign user to Member role
            result = await userManager.AddToRoleAsync(memberUser, "Investor");
            if (!result.Succeeded)
                return 4;  // should log an error message here

            return 0;
        }
    }
}
