using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceRepository :  EFBaseRepository<Invoice, AppDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}