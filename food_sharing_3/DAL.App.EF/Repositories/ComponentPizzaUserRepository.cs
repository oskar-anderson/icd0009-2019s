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
    public class ComponentPizzaUserRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.ComponentPizzaUser, DTO.ComponentPizzaUser>, 
        IComponentPizzaUserRepository
    {
        public ComponentPizzaUserRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.ComponentPizzaUser, DTO.ComponentPizzaUser>())
        {
        }

        /*
        
        public async Task<IEnumerable<ComponentPizzaUser>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(pc => pc.PizzaUser)
                .Include(pc => pc.Component)
                .AsQueryable();

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.ComponentPizzaUser> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pc => pc.PizzaUser)
                .Include(pc => pc.Component)
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
        public virtual async Task<IEnumerable<ComponentPizzaUser>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(cpu => new ComponentPizzaUser()
                {
                    Id = cpu.Id,
                    ComponentId = cpu.ComponentId, 
                    Component = new Component()
                    {
                        Name = cpu.Component.Name,
                    },
                    PizzaUserId = cpu.PizzaUserId,
                    PizzaUser = new PizzaUser()
                    {
                        Id = cpu.PizzaUser.Id,
                        PizzaId = cpu.PizzaUser.Pizza.Id,
                        AppUserId = cpu.PizzaUser.AppUserId,
                        Pizza = new Pizza()
                        {
                            Name = cpu.PizzaUser.Pizza.Name,
                        },
                        Changes = cpu.PizzaUser.Changes,
                    },
                })
                .ToListAsync();
        }

        public virtual async Task<ComponentPizzaUser> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(cpu => cpu.Id == id).AsQueryable();
            
            return await query
                .Select(cpu => new ComponentPizzaUser()
                {
                    Id = cpu.Id,
                    ComponentId = cpu.ComponentId, 
                    Component = new Component()
                    {
                        Name = cpu.Component.Name,
                    },
                    PizzaUserId = cpu.PizzaUserId,
                    PizzaUser = new PizzaUser()
                    {
                        Id = cpu.PizzaUser.Id,
                        PizzaId = cpu.PizzaUser.Pizza.Id,
                        AppUserId = cpu.PizzaUser.AppUserId,
                        Pizza = new Pizza()
                        {
                            Name = cpu.PizzaUser.Pizza.Name,
                        },
                        Changes = cpu.PizzaUser.Changes,
                    },
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<ComponentPizzaUser>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(cpu => new ComponentPizzaUser()
                {
                    Id = cpu.Id,
                    ComponentId = cpu.ComponentId, 
                    Component = new Component()
                        {
                            Id = cpu.Component.Id,
                            Name = cpu.Component.Name,
                        },
                    PizzaUserId = cpu.PizzaUserId,
                    PizzaUser = new PizzaUser()
                        {
                            Id = cpu.PizzaUser.Id,
                            PizzaId = cpu.PizzaUser.Pizza.Id,
                            AppUserId = cpu.PizzaUser.AppUserId,
                            Pizza = new Pizza()
                            {
                                Id = cpu.PizzaUser.Pizza.Id,
                                PizzaTemplateId = cpu.PizzaUser.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplate()
                                {
                                    Id = cpu.PizzaUser.Pizza.PizzaTemplate.Id,
                                    CategoryId = cpu.PizzaUser.Pizza.PizzaTemplate.CategoryId,
                                    Category = new Category()
                                    {
                                        Id = cpu.PizzaUser.Pizza.PizzaTemplate.Category.Id,
                                        Name = cpu.PizzaUser.Pizza.PizzaTemplate.Category.Name,
                                    },
                                    Name = cpu.PizzaUser.Pizza.PizzaTemplate.Name,
                                    Picture = cpu.PizzaUser.Pizza.PizzaTemplate.Picture,
                                    Modifications = cpu.PizzaUser.Pizza.PizzaTemplate.Modifications,
                                    Extras = cpu.PizzaUser.Pizza.PizzaTemplate.Extras,
                                    Description = cpu.PizzaUser.Pizza.PizzaTemplate.Description,
                                },
                                SizeNumber = cpu.PizzaUser.Pizza.SizeNumber,
                                SizeName = cpu.PizzaUser.Pizza.SizeName,
                                Name = cpu.PizzaUser.Pizza.Name,
                            },
                            Changes = cpu.PizzaUser.Changes,
                        },
                })
                .ToListAsync();
        }

        public virtual async Task<ComponentPizzaUser> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(cpu => cpu.Id == id).AsQueryable();
            
            return await query
                .Select(cpu => new ComponentPizzaUser()
                {
                    Id = cpu.Id,
                    ComponentId = cpu.ComponentId, 
                    Component = new Component()
                        {
                            Id = cpu.Component.Id,
                            Name = cpu.Component.Name,
                        },
                    PizzaUserId = cpu.PizzaUserId,
                    PizzaUser = new PizzaUser()
                        {
                            Id = cpu.PizzaUser.Id,
                            PizzaId = cpu.PizzaUser.Pizza.Id,
                            AppUserId = cpu.PizzaUser.AppUserId,
                            Pizza = new Pizza()
                            {
                                Id = cpu.PizzaUser.Pizza.Id,
                                PizzaTemplateId = cpu.PizzaUser.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplate()
                                {
                                    Id = cpu.PizzaUser.Pizza.PizzaTemplate.Id,
                                    CategoryId = cpu.PizzaUser.Pizza.PizzaTemplate.CategoryId,
                                    Category = new Category()
                                    {
                                        Id = cpu.PizzaUser.Pizza.PizzaTemplate.Category.Id,
                                        Name = cpu.PizzaUser.Pizza.PizzaTemplate.Category.Name,
                                    },
                                    Name = cpu.PizzaUser.Pizza.PizzaTemplate.Name,
                                    Picture = cpu.PizzaUser.Pizza.PizzaTemplate.Picture,
                                    Modifications = cpu.PizzaUser.Pizza.PizzaTemplate.Modifications,
                                    Extras = cpu.PizzaUser.Pizza.PizzaTemplate.Extras,
                                    Description = cpu.PizzaUser.Pizza.PizzaTemplate.Description,
                                },
                                SizeNumber = cpu.PizzaUser.Pizza.SizeNumber,
                                SizeName = cpu.PizzaUser.Pizza.SizeName,
                                Name = cpu.PizzaUser.Pizza.Name,
                            },
                            Changes = cpu.PizzaUser.Changes,
                        },
                })
                .FirstOrDefaultAsync();
        }
    }
}