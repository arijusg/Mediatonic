using Api.Models;

namespace Api.Interfaces
{
    public interface IUserService
    {
        User GetUser(int id);
    }
}
