using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class AppDbContext : DbContext //IdentityDbContext
    {
        public DbSet<PrimaryOne> PrimaryOnes { get; set; } = default!;
        public DbSet<ChildOne> ChildOnes { get; set; } = default!;

        public DbSet<PrimaryTwo> PrimaryTwos { get; set; } = default!;
        public DbSet<ChildTwo> ChildTwos { get; set; } = default!;

        public DbSet<PrimaryThree> PrimaryThrees { get; set; } = default!;
        public DbSet<ChildThree> ChildThrees { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}