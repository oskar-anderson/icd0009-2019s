using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class GpsLocationTypeRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.GpsLocationType, DAL.App.DTO.GpsLocationType>,
        IGpsLocationTypeRepository
    {
        public GpsLocationTypeRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<GpsLocationType, DTO.GpsLocationType>())
        {
        }
    }
}