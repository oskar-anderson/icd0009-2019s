using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Domain.Base.App.DTO;
using Domain.Base.EF.Repositories;
using Domain.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.App.EF.Repositories
{
    public class PizzaTemplateRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.PizzaTemplate, DTO.PizzaTemplate>, 
        IPizzaTemplateRepository
    {
        public PizzaTemplateRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.PizzaTemplate, DTO.PizzaTemplate>())
        //new DAL.Base.Mappers.BaseMapper<Domain.App.PizzaTemplate, DAL.App.DTO.PizzaTemplate>())
        {
        }

        public override async Task<IEnumerable<DTO.PizzaTemplate>> GetAllAsyncBase(object userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(p => p.Category)
                .AsQueryable();
            return (await query.ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }

        public virtual async Task<IEnumerable<DTO.PizzaTemplate>> GetAllAsyncSpecific_DALAppEF_BLLBase(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(p => p.Category)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.PizzaTemplate> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            await base.RemoveAsync(pizzaTemplate.Id);
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