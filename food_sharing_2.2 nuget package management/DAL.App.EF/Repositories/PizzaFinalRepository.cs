using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaFinalRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.PizzaFinal, PizzaFinal>, 
        IPizzaFinalRepository
    {
        public PizzaFinalRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.PizzaFinal, PizzaFinal>())
        {
        }

        public async Task<IEnumerable<PizzaFinal>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(pf => pf.Pizza)
                .AsQueryable();

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<PizzaFinal> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pf => pf.Pizza)
                .Where(pf => pf.Id == id)
                .AsQueryable();

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(cm => cm.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var pizzaFinal = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(pizzaFinal.Id);
        }

        /*
        public async Task<IEnumerable<PizzaFinalDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(pf => new PizzaFinalDTO()
                {
                    Id = pf.Id,
                    PizzaId = pf.PizzaId,
                    Pizza = pf.Pizza == null
                        ? null
                        : new PizzaDTO()
                        {
                            Id = pf.PizzaId,
                            PizzaTemplateId = pf.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplateDTO()
                            {
                                Id = pf.Pizza.PizzaTemplate.Id,
                                CategoryId = pf.Pizza.PizzaTemplate.CategoryId,
                                Category = new CategoryDTO()
                                {
                                    Id = pf.Pizza.PizzaTemplate.Category.Id,
                                    Name = pf.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = pf.Pizza.PizzaTemplate.Name,
                                Picture = pf.Pizza.PizzaTemplate.Picture,
                                Modifications = pf.Pizza.PizzaTemplate.Modifications,
                                Extras = pf.Pizza.PizzaTemplate.Extras,
                                Description = pf.Pizza.PizzaTemplate.Description,
                            },
                            SizeId = pf.Pizza.SizeId,
                            Size = new SizeDTO()
                            {
                                Id = pf.Pizza.Size.Id,
                                Name = pf.Pizza.Size.Name
                            },
                            Name = pf.Pizza.Name,
                        },
                    Changes = pf.Changes,
                    Tax = pf.Tax,
                    Gross = pf.Gross
                })
                .ToListAsync(); 
        }

        public async Task<PizzaFinalDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            PizzaFinalDTO PizzaFinalDTO = await query
                .Select(pf => new PizzaFinalDTO()
                {
                    Id = pf.Id,
                    PizzaId = pf.PizzaId,
                    Pizza = pf.Pizza == null
                        ? null
                        : new PizzaDTO()
                        {
                            Id = pf.PizzaId,
                            PizzaTemplateId = pf.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplateDTO()
                            {
                                Id = pf.Pizza.PizzaTemplate.Id,
                                CategoryId = pf.Pizza.PizzaTemplate.CategoryId,
                                Category = new CategoryDTO()
                                {
                                    Id = pf.Pizza.PizzaTemplate.Category.Id,
                                    Name = pf.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = pf.Pizza.PizzaTemplate.Name,
                                Picture = pf.Pizza.PizzaTemplate.Picture,
                                Modifications = pf.Pizza.PizzaTemplate.Modifications,
                                Extras = pf.Pizza.PizzaTemplate.Extras,
                                Description = pf.Pizza.PizzaTemplate.Description,
                            },
                            SizeId = pf.Pizza.SizeId,
                            Size = new SizeDTO()
                            {
                                Id = pf.Pizza.Size.Id,
                                Name = pf.Pizza.Size.Name
                            },
                            Name = pf.Pizza.Name,
                        },
                    Changes = pf.Changes,
                    Tax = pf.Tax,
                    Gross = pf.Gross
                })
                .FirstOrDefaultAsync();
            
            return PizzaFinalDTO;
        }
        */
    }
}