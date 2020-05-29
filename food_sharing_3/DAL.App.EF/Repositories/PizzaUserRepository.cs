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
    public class PizzaUserRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.PizzaUser, PizzaUser>, 
        IPizzaUserRepository
    {
        public PizzaUserRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.PizzaUser, PizzaUser>())
        {
        }

        /*
        public async Task<IEnumerable<PizzaUser>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(pf => pf.Pizza)
                .AsQueryable();

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<PizzaUser> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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

    }
        */
        public virtual async Task<IEnumerable<PizzaUser>> GetAllForViewAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
            return await query
                .Select(pu => new PizzaUser()
                {
                    Id = pu.Id,
                    AppUserId = pu.AppUserId,
                    PizzaId = pu.PizzaId,
                    Pizza = new Pizza()
                        {
                            Name = pu.Pizza.Name,
                        },
                    Changes = pu.Changes,
                })
                .ToListAsync();
        }

        public virtual async Task<PizzaUser> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(pu => pu.Id == id).AsQueryable();
            
            return await query
                .Select(pu => new PizzaUser()
                {
                    Id = pu.Id,
                    AppUserId = pu.AppUserId,
                    PizzaId = pu.PizzaId,
                    Pizza = new Pizza()
                    {
                        Name = pu.Pizza.Name,
                    },
                    Changes = pu.Changes,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<PizzaUser>> GetAllForApiAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
            return await query
                .Select(pu => new PizzaUser()
                {
                    Id = pu.Id,
                    AppUserId = pu.AppUserId,
                    PizzaId = pu.PizzaId,
                    Pizza = new Pizza()
                    {
                        Id = pu.PizzaId,
                        PizzaTemplateId = pu.Pizza.PizzaTemplateId,
                        PizzaTemplate = new PizzaTemplate()
                        {
                            Id = pu.Pizza.PizzaTemplate.Id,
                            CategoryId = pu.Pizza.PizzaTemplate.CategoryId,
                            Category = new Category()
                            {
                                Id = pu.Pizza.PizzaTemplate.Category.Id,
                                Name = pu.Pizza.PizzaTemplate.Category.Name,
                            },
                            Name = pu.Pizza.PizzaTemplate.Name,
                            Picture = pu.Pizza.PizzaTemplate.Picture,
                            Modifications = pu.Pizza.PizzaTemplate.Modifications,
                            Extras = pu.Pizza.PizzaTemplate.Extras,
                            Description = pu.Pizza.PizzaTemplate.Description,
                            VarietyState = pu.Pizza.PizzaTemplate.VarietyState,
                        },
                        SizeNumber = pu.Pizza.SizeNumber,
                        SizeName = pu.Pizza.SizeName,
                        Name = pu.Pizza.Name,
                    },
                    Changes = pu.Changes,
                })
                .ToListAsync();
        }

        public virtual async Task<PizzaUser> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(pu => pu.Id == id).AsQueryable();
            
            return await query
                .Select(pu => new PizzaUser()
                {
                    Id = pu.Id,
                    AppUserId = pu.AppUserId,
                    PizzaId = pu.PizzaId,
                    Pizza = new Pizza()
                    {
                        Id = pu.PizzaId,
                        PizzaTemplateId = pu.Pizza.PizzaTemplateId,
                        PizzaTemplate = new PizzaTemplate()
                        {
                            Id = pu.Pizza.PizzaTemplate.Id,
                            CategoryId = pu.Pizza.PizzaTemplate.CategoryId,
                            Category = new Category()
                            {
                                Id = pu.Pizza.PizzaTemplate.Category.Id,
                                Name = pu.Pizza.PizzaTemplate.Category.Name,
                            },
                            Name = pu.Pizza.PizzaTemplate.Name,
                            Picture = pu.Pizza.PizzaTemplate.Picture,
                            Modifications = pu.Pizza.PizzaTemplate.Modifications,
                            Extras = pu.Pizza.PizzaTemplate.Extras,
                            Description = pu.Pizza.PizzaTemplate.Description,
                            VarietyState = pu.Pizza.PizzaTemplate.VarietyState,
                        },
                        SizeNumber = pu.Pizza.SizeNumber,
                        SizeName = pu.Pizza.SizeName,
                        Name = pu.Pizza.Name,
                    },
                    Changes = pu.Changes,
                })
                .FirstOrDefaultAsync();
        }
    }
}