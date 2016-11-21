using System.Web.Http;
using Api.Interfaces;
using Api.Models;
using Api.Services;

namespace Api.Controllers
{
    [RoutePrefix("api/animal")]
    public class AnimalController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IAnimalService _animalService;

        public AnimalController(IUserService userService, IAnimalService animalService)
        {
            _userService = userService;
            _animalService = animalService;
        }

        [Route("user/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetUserAnimals(int id)
        {
            var user = _userService.GetUser(id);
            var animals = _animalService.GetUserAnimals(user);

            return Ok(animals);
        }

        [Route("feed")]
        [HttpPost]
        public IHttpActionResult Feed(Animal animal)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            _animalService.Feed(animal);
            var fedAnimal = _animalService.GetAnimal(animal.Id);
            
            return Ok(fedAnimal);
        }

        [Route("pet")]
        [HttpPost]
        public IHttpActionResult Pet(Animal animal)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _animalService.Pet(animal);
            var petAnimal = _animalService.GetAnimal(animal.Id);

            return Ok(petAnimal);
        }

    }
}

