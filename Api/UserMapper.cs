namespace Api
{
    public interface IUserMapper
    {
        Api.Models.User Map(Api.Entities.User user);
    }

    public class UserMapper : IUserMapper
    {
        public Api.Models.User Map(Api.Entities.User user)
        {
            return new Api.Models.User(user.ID, user.Name);
        }
    }
}
