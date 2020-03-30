﻿using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SizeRepository :  EFBaseRepository<Size, AppDbContext>, ISizeRepository
    {
        public SizeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}