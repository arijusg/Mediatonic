using Api.DAL.Entities;

namespace Api.Interfaces
{
    public interface IUserMapper
    {
        Api.Models.User Map(User user);
    }
}