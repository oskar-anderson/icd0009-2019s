using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FriendRepository :  EFBaseRepository<Friend, AppDbContext>, IFriendRepository
    {
        public FriendRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}