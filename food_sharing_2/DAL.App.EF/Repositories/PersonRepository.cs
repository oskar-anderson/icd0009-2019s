using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Domain.Base.EF.Repositories;
using Domain.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.App.EF.Repositories
{
    public class PersonRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Person, DTO.Person>, 
        IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.Person, DTO.Person>())
        {
        }

        public async Task<IEnumerable<DTO.Person>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(p => p.AppUserId == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.Person> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(p => p.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(p => p.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(p => p.Id == id);
            }

            return await RepoDbSet.AnyAsync((p => p.AppUserId == userId));

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var person = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(person.Id);
        }

        /*
        public async Task<IEnumerable<PersonDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(p => new PersonDTO()
                {
                    Id = p.Id,
                    AppUserId = p.AppUserId,
                    ThisIsMe = p.ThisIsMe,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    NationalIdentificationNumber = p.NationalIdentificationNumber,
                    Since = p.Since,
                    Until = p.Until,
                })
                .ToListAsync();
        }

        public async Task<PersonDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            PersonDTO cartMealDTO = await query
                .Select(p => new PersonDTO()
                {
                    Id = p.Id,
                    AppUserId = p.AppUserId,
                    ThisIsMe = p.ThisIsMe,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    NationalIdentificationNumber = p.NationalIdentificationNumber,
                    Since = p.Since,
                    Until = p.Until,
                })
                .FirstOrDefaultAsync();
            
            return cartMealDTO;
        }
        */
    }
}