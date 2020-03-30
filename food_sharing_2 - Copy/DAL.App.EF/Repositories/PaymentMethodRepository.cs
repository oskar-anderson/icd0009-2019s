using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentMethodRepository :  EFBaseRepository<PaymentMethod, AppDbContext>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}