using System.Web.Http;
using Api.Services;

namespace Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var user = _userService.GetUser(id);
            
            if (user == null)
                return NotFound();
            
            return Ok(user);
        }
               
    }
}
