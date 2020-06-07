namespace ee.itcollege.kaande.pitsariina.BLL.Base.Mappers
{
    /// <summary>
    /// Maps using Automapper. No mapper configuration. Property types and names have to match.
    /// </summary>
    /// <typeparam name="TLeftObject"></typeparam>
    /// <typeparam name="TRightObject"></typeparam>
    public class BaseMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}