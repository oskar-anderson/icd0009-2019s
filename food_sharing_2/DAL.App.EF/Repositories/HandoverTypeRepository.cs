using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class HandoverTypeRepository : BaseRepository<HandoverType>, IHandoverTypeRepository
    {
        public HandoverTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}