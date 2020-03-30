using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MenuRepository :  EFBaseRepository<Menu, AppDbContext>, IMenuRepository
    {
        public MenuRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}