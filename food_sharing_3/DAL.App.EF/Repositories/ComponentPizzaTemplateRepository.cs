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
    public class ComponentPizzaTemplateRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.ComponentPizzaTemplate, DTO.ComponentPizzaTemplate>, 
        IComponentPizzaTemplateRepository
    {
        public ComponentPizzaTemplateRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.ComponentPizzaTemplate, DTO.ComponentPizzaTemplate>())
        {
        }
        
        /*
        public async Task<IEnumerable<ComponentPizzaTemplate>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(pc => pc.Component)
                .Include(pc => pc.PizzaTemplate)
                .AsQueryable();

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.ComponentPizzaTemplate> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pc => pc.Component)
                .Include(pc => pc.PizzaTemplate)
                .Where(pc => pc.Id == id)
                .AsQueryable();

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(pc => pc.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var pizzaComponent = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(pizzaComponent.Id);
        }

        */
        public virtual async Task<IEnumerable<ComponentPizzaTemplate>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(cpt => new ComponentPizzaTemplate()
                {
                    Id = cpt.Id,
                    ComponentId = cpt.ComponentId, 
                    Component = new Component()
                    {
                        Name = cpt.Component.Name,
                    },
                    PizzaTemplateId = cpt.PizzaTemplate.Id,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Name = cpt.PizzaTemplate.Name,
                    }
                })
                .ToListAsync();
        }

        public virtual async Task<ComponentPizzaTemplate> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(cpt => cpt.Id == id).AsQueryable();
            
            return await query
                .Select(cpt => new ComponentPizzaTemplate()
                {
                    Id = cpt.Id,
                    ComponentId = cpt.ComponentId, 
                    Component = new Component()
                    {
                        Name = cpt.Component.Name,
                    },
                    PizzaTemplateId = cpt.PizzaTemplate.Id,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Name = cpt.PizzaTemplate.Name,
                    }
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<ComponentPizzaTemplate>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(cpt => new ComponentPizzaTemplate()
                {
                    Id = cpt.Id,
                    ComponentId = cpt.ComponentId, 
                    Component = new Component()
                    {
                        Id = cpt.Component.Id,
                        Name = cpt.Component.Name,
                    },
                    PizzaTemplateId = cpt.PizzaTemplate.Id,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Id = cpt.PizzaTemplate.Id,
                        CategoryId = cpt.PizzaTemplate.CategoryId,
                        Category = new Category()
                        {
                            Id = cpt.PizzaTemplate.Category.Id,
                            Name = cpt.PizzaTemplate.Category.Name,
                        },
                        Name = cpt.PizzaTemplate.Name,
                        Picture = cpt.PizzaTemplate.Picture,
                        Modifications = cpt.PizzaTemplate.Modifications,
                        Extras = cpt.PizzaTemplate.Extras,
                        Description = cpt.PizzaTemplate.Description,
                        VarietyState = cpt.PizzaTemplate.VarietyState,
                    }
                })
                .ToListAsync();
        }

        public virtual async Task<ComponentPizzaTemplate> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(cpt => cpt.Id == id).AsQueryable();
            
            return await query
                .Select(cpt => new ComponentPizzaTemplate()
                {
                    Id = cpt.Id,
                    ComponentId = cpt.ComponentId, 
                    Component = new Component()
                        {
                            Id = cpt.Component.Id,
                            Name = cpt.Component.Name,
                        },
                    PizzaTemplateId = cpt.PizzaTemplate.Id,
                    PizzaTemplate = new PizzaTemplate()
                    {
                        Id = cpt.PizzaTemplate.Id,
                        CategoryId = cpt.PizzaTemplate.CategoryId,
                        Category = new Category()
                        {
                            Id = cpt.PizzaTemplate.Category.Id,
                            Name = cpt.PizzaTemplate.Category.Name,
                        },
                        Name = cpt.PizzaTemplate.Name,
                        Picture = cpt.PizzaTemplate.Picture,
                        Modifications = cpt.PizzaTemplate.Modifications,
                        Extras = cpt.PizzaTemplate.Extras,
                        Description = cpt.PizzaTemplate.Description,
                        VarietyState = cpt.PizzaTemplate.VarietyState,
                    }
                })
                .FirstOrDefaultAsync();
        }
    }
}