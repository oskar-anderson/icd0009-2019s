namespace ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Mappers
{
    public interface IBaseMapper<TLeftObject, TRightObject> : global::ee.itcollege.kaande.pitsariina.Contracts.DAL.Base.Mappers.IBaseMapper<TLeftObject, TRightObject>
        where TLeftObject: class?, new()
        where TRightObject: class?, new()
    {
        
    }
}