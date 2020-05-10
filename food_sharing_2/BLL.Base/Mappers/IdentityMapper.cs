namespace BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : Domain.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new() 
        where TLeftObject : class?, new()
    {
    }
}