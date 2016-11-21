using Api.DAL.Entities;
using Api.Interfaces;

namespace Api.Mappers
{
    public class UserMapper : IUserMapper
    {
        public Api.Models.User Map(User user)
        {
            return new Api.Models.User(user.ID, user.Name);
        }
    }
}
