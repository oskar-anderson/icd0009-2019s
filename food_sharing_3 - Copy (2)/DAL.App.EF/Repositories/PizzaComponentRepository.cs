using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaComponentRepository : 
        EFBaseRepository<AppDbContext, AppUser, PizzaComponent, DTO.PizzaComponent>, 
        IPizzaComponentRepository
    {
        public PizzaComponentRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<PizzaComponent, DTO.PizzaComponent>())
        {
        }

        public async Task<IEnumerable<DTO.PizzaComponent>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(pc => pc.PizzaFinal)
                .Include(pc => pc.Component)
                .Include(pc => pc.PizzaTemplate)
                .AsQueryable();

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.PizzaComponent> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pc => pc.PizzaFinal)
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

        /*
        public async Task<IEnumerable<PizzaComponentDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(pc => new PizzaComponentDTO()
                {
                    Id = pc.Id,
                    ComponentId = pc.ComponentId, 
                    Component = pc.Component == null
                        ? null
                        : new ComponentDTO()
                        {
                            Id = pc.Component.Id,
                            Name = pc.Component.Name,
                            Max = pc.Component.Max,
                        },
                    PizzaFinalId = pc.PizzaFinalId,
                    PizzaFinal = pc.PizzaFinal == null
                        ? null
                        : new PizzaFinalDTO()
                        {
                            Id = pc.PizzaFinal.Id,
                            PizzaId = pc.PizzaFinal.Pizza.Id,
                            Pizza = new PizzaDTO()
                            {
                                Id = pc.PizzaFinal.Pizza.Id,
                                PizzaTemplateId = pc.PizzaFinal.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplateDTO()
                                {
                                    Id = pc.PizzaFinal.Pizza.PizzaTemplate.Id,
                                    CategoryId = pc.PizzaFinal.Pizza.PizzaTemplate.CategoryId,
                                    Category = new CategoryDTO()
                                    {
                                        Id = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Id,
                                        Name = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Name,
                                    },
                                    Name = pc.PizzaFinal.Pizza.PizzaTemplate.Name,
                                    Picture = pc.PizzaFinal.Pizza.PizzaTemplate.Picture,
                                    Modifications = pc.PizzaFinal.Pizza.PizzaTemplate.Modifications,
                                    Extras = pc.PizzaFinal.Pizza.PizzaTemplate.Extras,
                                    Description = pc.PizzaFinal.Pizza.PizzaTemplate.Description,
                                },
                                SizeId = pc.PizzaFinal.Pizza.SizeId,
                                Size = new SizeDTO()
                                {
                                    Id = pc.PizzaFinal.Pizza.Size.Id,
                                    Name = pc.PizzaFinal.Pizza.Size.Name
                                },
                                Name = pc.PizzaFinal.Pizza.Name,
                            },
                            Changes = pc.PizzaFinal.Changes,
                            Tax = pc.PizzaFinal.Tax,
                            Gross = pc.PizzaFinal.Gross
                        },
                    PizzaTemplateId = pc.PizzaTemplate.Id,
                    PizzaTemplate = new PizzaTemplateDTO()
                    {
                        Id = pc.PizzaFinal.Pizza.PizzaTemplate.Id,
                        CategoryId = pc.PizzaFinal.Pizza.PizzaTemplate.CategoryId,
                        Category = new CategoryDTO()
                        {
                            Id = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Id,
                            Name = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Name,
                        },
                        Name = pc.PizzaFinal.Pizza.PizzaTemplate.Name,
                        Picture = pc.PizzaFinal.Pizza.PizzaTemplate.Picture,
                        Modifications = pc.PizzaFinal.Pizza.PizzaTemplate.Modifications,
                        Extras = pc.PizzaFinal.Pizza.PizzaTemplate.Extras,
                        Description = pc.PizzaFinal.Pizza.PizzaTemplate.Description,
                    }
                })
                .ToListAsync();
        }

        public async Task<PizzaComponentDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            PizzaComponentDTO pizzaComponentDTO = await query
                .Select(pc => new PizzaComponentDTO()
                {
                    Id = pc.Id,
                    ComponentId = pc.ComponentId, 
                    Component = pc.Component == null
                        ? null
                        : new ComponentDTO()
                        {
                            Id = pc.Component.Id,
                            Name = pc.Component.Name,
                            Max = pc.Component.Max,
                        },
                    PizzaFinalId = pc.PizzaFinalId,
                    PizzaFinal = pc.PizzaFinal == null
                        ? null
                        : new PizzaFinalDTO()
                        {
                            Id = pc.PizzaFinal.Id,
                            PizzaId = pc.PizzaFinal.Pizza.Id,
                            Pizza = new PizzaDTO()
                            {
                                Id = pc.PizzaFinal.Pizza.Id,
                                PizzaTemplateId = pc.PizzaFinal.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplateDTO()
                                {
                                    Id = pc.PizzaFinal.Pizza.PizzaTemplate.Id,
                                    CategoryId = pc.PizzaFinal.Pizza.PizzaTemplate.CategoryId,
                                    Category = new CategoryDTO()
                                    {
                                        Id = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Id,
                                        Name = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Name,
                                    },
                                    Name = pc.PizzaFinal.Pizza.PizzaTemplate.Name,
                                    Picture = pc.PizzaFinal.Pizza.PizzaTemplate.Picture,
                                    Modifications = pc.PizzaFinal.Pizza.PizzaTemplate.Modifications,
                                    Extras = pc.PizzaFinal.Pizza.PizzaTemplate.Extras,
                                    Description = pc.PizzaFinal.Pizza.PizzaTemplate.Description,
                                },
                                SizeId = pc.PizzaFinal.Pizza.SizeId,
                                Size = new SizeDTO()
                                {
                                    Id = pc.PizzaFinal.Pizza.Size.Id,
                                    Name = pc.PizzaFinal.Pizza.Size.Name
                                },
                                Name = pc.PizzaFinal.Pizza.Name,
                            },
                            Changes = pc.PizzaFinal.Changes,
                            Tax = pc.PizzaFinal.Tax,
                            Gross = pc.PizzaFinal.Gross
                        },
                    PizzaTemplateId = pc.PizzaTemplate.Id,
                    PizzaTemplate = new PizzaTemplateDTO()
                    {
                        Id = pc.PizzaFinal.Pizza.PizzaTemplate.Id,
                        CategoryId = pc.PizzaFinal.Pizza.PizzaTemplate.CategoryId,
                        Category = new CategoryDTO()
                        {
                            Id = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Id,
                            Name = pc.PizzaFinal.Pizza.PizzaTemplate.Category.Name,
                        },
                        Name = pc.PizzaFinal.Pizza.PizzaTemplate.Name,
                        Picture = pc.PizzaFinal.Pizza.PizzaTemplate.Picture,
                        Modifications = pc.PizzaFinal.Pizza.PizzaTemplate.Modifications,
                        Extras = pc.PizzaFinal.Pizza.PizzaTemplate.Extras,
                        Description = pc.PizzaFinal.Pizza.PizzaTemplate.Description,
                    }
                })
                .FirstOrDefaultAsync();
            
            return pizzaComponentDTO;
        }
        */
    }
}