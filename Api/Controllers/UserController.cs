using System;
using System.Collections.Generic;
using System.Web.Http;
using Api.Models;
using Api.Services;

namespace Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IAnimalService _animalService;

        public UserController(IUserService userService, IAnimalService animalService)
        {
            _userService = userService;
            _animalService = animalService;
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var user = _userService.GetUser(id);

            if (user.Id == 0)
                return NotFound();
            
            return Ok(user);
        }

        [Route("{id:int}/animals")]
        [HttpGet]
        public IHttpActionResult GetUserAnimals(int id)
        {
            var user = _userService.GetUser(id);
            return Ok(_animalService.GetUserAnimals(user));
        }
       
    }
}
