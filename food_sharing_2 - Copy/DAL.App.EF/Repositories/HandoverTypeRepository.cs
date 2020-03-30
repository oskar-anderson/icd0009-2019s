using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class HandoverTypeRepository :  EFBaseRepository<HandoverType, AppDbContext>, IHandoverTypeRepository
    {
        public HandoverTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}