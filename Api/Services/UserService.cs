using Api.DAL;
using Api.Interfaces;
using Api.Models;

namespace Api.Services
{
    public class UserService : IUserService
    {
        private readonly GameContext _context;
        private readonly IUserMapper _userMapper;

        public UserService(GameContext context, IUserMapper userMapper)
        {
            _context = context;
            _userMapper = userMapper;
        }
        public User GetUser(int id)
        {
            var user = _context.Users.Find(id);
            return user == null ? null : _userMapper.Map(user);
        }
    }
}
