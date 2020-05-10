using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Domain.Base.App.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Person, Person>, 
        IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Person, Person>())
        {
        }

        public async Task<IEnumerable<Person>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
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

        public async Task<Person> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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