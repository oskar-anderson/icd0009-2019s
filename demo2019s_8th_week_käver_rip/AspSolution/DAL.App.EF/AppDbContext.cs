﻿using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {

        public DbSet<Owner> Owners { get; set; } = default!;
        public DbSet<Animal> Animals { get; set; } = default!;
        public DbSet<OwnerAnimal> OwnerAnimals { get; set; } = default!;
        
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}