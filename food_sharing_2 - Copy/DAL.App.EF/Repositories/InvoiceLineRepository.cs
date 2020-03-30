using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceLineRepository :  EFBaseRepository<InvoiceLine, AppDbContext>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}