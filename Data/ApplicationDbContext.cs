using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPX.Models;

namespace SPX.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bucket> Buckets { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<SportCategory> SportCategories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<BucketTeamConnection> BucketTeamConnections { get; set; }
    }
}
