using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MealComponentRepository :  EFBaseRepository<MealComponent, AppDbContext>, IMealComponentRepository
    {
        public MealComponentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}