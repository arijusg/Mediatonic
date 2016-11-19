using Api.Models;

namespace Api.Services
{
    public interface IUserService
    {
        User GetUser(int id);
    }
}
