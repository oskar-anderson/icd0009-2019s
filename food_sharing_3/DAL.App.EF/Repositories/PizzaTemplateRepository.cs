using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Domain.App.Identity;
using ee.itcollege.kaande.pitsariina.DAL.Base.EF.Repositories;
using ee.itcollege.kaande.pitsariina.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaTemplateRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.PizzaTemplate, PizzaTemplate>, 
        IPizzaTemplateRepository
    {
        public PizzaTemplateRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.PizzaTemplate, PizzaTemplate>())
        //new DAL.Base.Mappers.BaseMapper<Domain.App.PizzaTemplate, DAL.App.DTO.PizzaTemplate>())
        {
        }
        
        /*
        public override async Task<IEnumerable<PizzaTemplate>> GetAllAsyncBase(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query;
                //.Include(p => p.Category);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<IEnumerable<PizzaTemplate>> GetAllAsyncSpecific_DALAppEF_BLLBase(Guid id, Guid? userId = null, bool noTracking = true)
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
            await base.RemoveAsync(pizzaTemplate.Id);
        }
        

        */
        public virtual async Task<IEnumerable<PizzaTemplate>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(pt => new PizzaTemplate()
                {
                    Id = pt.Id,
                    CategoryId = pt.CategoryId,
                    Category = new Category()
                    {
                        Name = pt.Category.Name,
                    },
                    Name = pt.Name,
                    Picture = pt.Picture,
                    Modifications = pt.Modifications,
                    Extras = pt.Extras,
                    Description = pt.Description,
                }).ToListAsync();
        }
        
        public async Task<PizzaTemplate> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(pt => pt.Id == id).AsQueryable();
            
            return await query
                .Select(pt => new PizzaTemplate()
                {
                    Id = pt.Id,
                    CategoryId = pt.CategoryId,
                    Category = new Category()
                    {
                        Name = pt.Category.Name,
                    },
                    Name = pt.Name,
                    Picture = pt.Picture,
                    Modifications = pt.Modifications,
                    Extras = pt.Extras,
                    Description = pt.Description,
                }).FirstOrDefaultAsync();
        }
        
        public virtual async Task<IEnumerable<PizzaTemplate>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(pt => new PizzaTemplate()
                {
                    Id = pt.Id,
                    CategoryId = pt.CategoryId,
                    Category = new Category()
                    {
                        Id = pt.Category.Id,
                        Name = pt.Category.Name,
                    },
                    Name = pt.Name,
                    Picture = pt.Picture,
                    Modifications = pt.Modifications,
                    Extras = pt.Extras,
                    Description = pt.Description,
                    VarietyState = pt.VarietyState,
                }).ToListAsync();
        }
        
        public async Task<PizzaTemplate> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(pt => pt.Id == id).AsQueryable();
            
            return await query
                .Select(pt => new PizzaTemplate()
                {
                    Id = pt.Id,
                    CategoryId = pt.CategoryId,
                    Category = new Category()
                    {
                        Id = pt.Category.Id,
                        Name = pt.Category.Name,
                    },
                    Name = pt.Name,
                    Picture = pt.Picture,
                    Modifications = pt.Modifications,
                    Extras = pt.Extras,
                    Description = pt.Description,
                    VarietyState = pt.VarietyState,
                }).FirstOrDefaultAsync();
        }
    }
}