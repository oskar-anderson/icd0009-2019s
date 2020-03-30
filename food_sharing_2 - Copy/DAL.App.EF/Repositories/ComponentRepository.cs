using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ComponentRepository :  EFBaseRepository<Component, AppDbContext>, IComponentRepository
    {
        public ComponentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}