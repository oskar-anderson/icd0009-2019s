using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaTemplateRepository : EFBaseRepository<AppDbContext, Domain.PizzaTemplate, DAL.App.DTO.PizzaTemplate>, IPizzaTemplateRepository
    {
        public PizzaTemplateRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.PizzaTemplate, DAL.App.DTO.PizzaTemplate>())
        {
        }

        public async Task<IEnumerable<PizzaTemplate>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Category)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<PizzaTemplate> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .AsQueryable();
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet
                .AnyAsync(p => p.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var pizzaTemplate = await FirstOrDefaultAsync(id, userId);
            base.Remove(pizzaTemplate);
        }
        /*
        public async Task<IEnumerable<PizzaTemplateDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(p => new PizzaTemplateDTO()
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    Category = new CategoryDTO()
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name,
                    },
                    Name = p.Name,
                    Picture = p.Picture,
                    Modifications = p.Modifications,
                    Extras = p.Extras,
                    Description = p.Description,
                })
                .ToListAsync();
        }

        public async Task<PizzaTemplateDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            PizzaTemplateDTO pizzaTemplateDTO = await query
                .Select(p => new PizzaTemplateDTO()
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    Category = new CategoryDTO()
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name,
                    },
                    Name = p.Name,
                    Picture = p.Picture,
                    Modifications = p.Modifications,
                    Extras = p.Extras,
                    Description = p.Description,
                })
                .FirstOrDefaultAsync();
            
            return pizzaTemplateDTO;
        }
        */
    }
}