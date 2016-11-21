namespace Api.Interfaces
{
    public interface IUserMapper
    {
        Api.Models.User Map(Api.Entities.User user);
    }
}