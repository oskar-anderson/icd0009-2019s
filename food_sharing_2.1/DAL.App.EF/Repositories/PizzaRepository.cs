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
    public class PizzaRepository :  EFBaseRepository<AppDbContext, Domain.Pizza, DAL.App.DTO.Pizza>, IPizzaRepository
    {
        public PizzaRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Pizza, DAL.App.DTO.Pizza>())
        {
        }

        public async Task<IEnumerable<Pizza>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Size)
                .Include(p => p.PizzaTemplate)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Pizza> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Size)
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
            base.Remove(pizza);
        }

        /*
        public async Task<IEnumerable<PizzaDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(p => new PizzaDTO()
                {
                    Id = p.Id,
                    PizzaTemplateId = p.PizzaTemplateId,
                    PizzaTemplate = new PizzaTemplateDTO()
                    {
                        Id = p.PizzaTemplate.Id,
                        CategoryId = p.PizzaTemplate.CategoryId,
                        Category = new CategoryDTO()
                        {
                            Id = p.PizzaTemplate.Category.Id,
                            Name = p.PizzaTemplate.Category.Name,
                        },
                        Name = p.PizzaTemplate.Name,
                        Picture = p.PizzaTemplate.Picture,
                        Modifications = p.PizzaTemplate.Modifications,
                        Extras = p.PizzaTemplate.Extras,
                        Description = p.PizzaTemplate.Description,
                    },
                    SizeId = p.SizeId,
                    Size = new SizeDTO()
                    {
                        Id = p.Size.Id,
                        Name = p.Size.Name
                    },
                    Name = p.Name,
                })
                .ToListAsync();
        }

        public async Task<PizzaDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            PizzaDTO pizzaDTO = await query
                .Select(p => new PizzaDTO()
                {
                    Id = p.Id,
                    PizzaTemplateId = p.PizzaTemplateId,
                    PizzaTemplate = new PizzaTemplateDTO()
                    {
                        Id = p.PizzaTemplate.Id,
                        CategoryId = p.PizzaTemplate.CategoryId,
                        Category = new CategoryDTO()
                        {
                            Id = p.PizzaTemplate.Category.Id,
                            Name = p.PizzaTemplate.Category.Name,
                        },
                        Name = p.PizzaTemplate.Name,
                        Picture = p.PizzaTemplate.Picture,
                        Modifications = p.PizzaTemplate.Modifications,
                        Extras = p.PizzaTemplate.Extras,
                        Description = p.PizzaTemplate.Description,
                    },
                    SizeId = p.SizeId,
                    Size = new SizeDTO()
                    {
                        Id = p.Size.Id,
                        Name = p.Size.Name
                    },
                    Name = p.Name,
                })
                .FirstOrDefaultAsync();
            
            return pizzaDTO;
        }
        */
    }
}