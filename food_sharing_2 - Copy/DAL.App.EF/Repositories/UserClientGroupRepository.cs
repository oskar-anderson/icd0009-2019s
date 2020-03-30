using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserClientGroupRepository :  EFBaseRepository<UserClientGroup, AppDbContext>, IUserClientGroupRepository
    {
        public UserClientGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}