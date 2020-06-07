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
    public class PizzaRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Pizza, Pizza>, 
        IPizzaRepository
    {
        public PizzaRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Pizza, Pizza>())
        {
        }
        
        /*
        public async Task<IEnumerable<Pizza>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(p => p.PizzaTemplate)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Pizza> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.PizzaTemplate)
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
            var pizza = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(pizza.Id);
        }


        */
        public virtual async Task<IEnumerable<Pizza>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(p => new Pizza()
                {
                    Id = p.Id,
                    PizzaTemplateId = p.PizzaTemplateId,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Id = p.PizzaTemplate.Id,
                        CategoryId = p.PizzaTemplate.CategoryId,
                        Category = new Category()
                        {
                            Name = p.PizzaTemplate.Category.Name,
                        },
                        Name = p.PizzaTemplate.Name,
                        Picture = p.PizzaTemplate.Picture,
                        Modifications = p.PizzaTemplate.Modifications,
                        Extras = p.PizzaTemplate.Extras,
                        Description = p.PizzaTemplate.Description,
                        VarietyState = p.PizzaTemplate.VarietyState,
                    },
                    SizeNumber = p.SizeNumber,
                    SizeName = p.SizeName,
                    Name = p.Name,
                })
                .ToListAsync();
        }

        public virtual async Task<Pizza> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(p => p.Id == id).AsQueryable();
            
            return await query
                .Select(p => new Pizza()
                {
                    Id = p.Id,
                    PizzaTemplateId = p.PizzaTemplateId,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Id = p.PizzaTemplate.Id,
                        CategoryId = p.PizzaTemplate.CategoryId,
                        Category = new Category()
                        {
                            Name = p.PizzaTemplate.Category.Name,
                        },
                        Name = p.PizzaTemplate.Name,
                        Picture = p.PizzaTemplate.Picture,
                        Modifications = p.PizzaTemplate.Modifications,
                        Extras = p.PizzaTemplate.Extras,
                        Description = p.PizzaTemplate.Description,
                        VarietyState = p.PizzaTemplate.VarietyState,
                    },
                    SizeNumber = p.SizeNumber,
                    SizeName = p.SizeName,
                    Name = p.Name,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Pizza>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(p => new Pizza()
                {
                    Id = p.Id,
                    PizzaTemplateId = p.PizzaTemplateId,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Id = p.PizzaTemplate.Id,
                        CategoryId = p.PizzaTemplate.CategoryId,
                        Category = new Category()
                        {
                            Id = p.PizzaTemplate.Category.Id,
                            Name = p.PizzaTemplate.Category.Name,
                        },
                        Name = p.PizzaTemplate.Name,
                        Picture = p.PizzaTemplate.Picture,
                        Modifications = p.PizzaTemplate.Modifications,
                        Extras = p.PizzaTemplate.Extras,
                        Description = p.PizzaTemplate.Description,
                        VarietyState = p.PizzaTemplate.VarietyState,
                    },
                    SizeNumber = p.SizeNumber,
                    SizeName = p.SizeName,
                    Name = p.Name,
                })
                .ToListAsync();
        }

        public virtual async Task<Pizza> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(p => p.Id == id).AsQueryable();
            
            return await query
                .Select(p => new Pizza()
                {
                    Id = p.Id,
                    PizzaTemplateId = p.PizzaTemplateId,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Id = p.PizzaTemplate.Id,
                        CategoryId = p.PizzaTemplate.CategoryId,
                        Category = new Category()
                        {
                            Id = p.PizzaTemplate.Category.Id,
                            Name = p.PizzaTemplate.Category.Name,
                        },
                        Name = p.PizzaTemplate.Name,
                        Picture = p.PizzaTemplate.Picture,
                        Modifications = p.PizzaTemplate.Modifications,
                        Extras = p.PizzaTemplate.Extras,
                        Description = p.PizzaTemplate.Description,
                        VarietyState = p.PizzaTemplate.VarietyState,
                    },
                    SizeNumber = p.SizeNumber,
                    SizeName = p.SizeName,
                    Name = p.Name,
                })
                .FirstOrDefaultAsync();
        }
    }
}