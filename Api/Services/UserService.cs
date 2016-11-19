using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public class UserService : IUserService
    {
        public User GetUser(int id)
        {
            //TODO implement
            return new User(id);
        }
    }
}
