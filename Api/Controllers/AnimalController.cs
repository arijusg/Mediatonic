using System;
using System.Collections.Generic;
using System.Web.Http;
using Api.Models;
using Api.Services;

namespace Api.Controllers
{
    [RoutePrefix("api/animal")]
    public class AnimalController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IAnimalService _animalService;

        public AnimalController( IUserService userService, IAnimalService animalService)
        {
            _userService = userService;
            _animalService = animalService;
        }

        [Route("feed")]
        [HttpPost]
        public IHttpActionResult Feed(Animal animal)
        {
            var fedAnimal = new Animal(animal.Id, animal.HappyLevel, animal.HappyDececrement,
                animal.HungryLevel - 1, animal.HungryIncrement);
            
            return Ok(fedAnimal);
        }

        [Route("pet")]
        [HttpPost]
        public IHttpActionResult Pet(Animal animal)
        {
            var fedAnimal = new Animal(animal.Id, animal.HappyLevel + 1, animal.HappyDececrement,
                animal.HungryLevel, animal.HungryIncrement);

            return Ok(fedAnimal);
        }

    }
}
