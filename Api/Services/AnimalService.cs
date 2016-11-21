using System.Collections.Generic;
using System.Linq;
using Api.DAL;
using Api.Models;

namespace Api.Services
{
    public interface IAnimalService
    {
        List<Animal> GetUserAnimals(User user);
        void Feed(Animal animal);
        Animal GetAnimal(int id);
        void Pet(Animal animal);
    }
    public class AnimalService : IAnimalService
    {
        private readonly ITestableDateTime _testableDateTime;
        private readonly GameContext _context;
        private readonly IAnimalMapper _animalMapper;
        private readonly IAnimalProcessor _animalProcessor;

        public AnimalService(ITestableDateTime testableDateTime,
            GameContext context, IAnimalMapper animalMapper, IAnimalProcessor animalProcessor)
        {
            _testableDateTime = testableDateTime;
            _context = context;
            _animalMapper = animalMapper;
            _animalProcessor = animalProcessor;
        }

        public List<Animal> GetUserAnimals(User user)
        {
            var entities = _context.Animals.Where(x => x.User.ID == user.Id).ToList();
            var animals = _animalMapper.Map(entities);
 
            return _animalProcessor.GetProcessedAnimals(animals);
        }

        public Animal GetAnimal(int id)
        {
            var entity = _context.Animals.Find(id);
            var animal = _animalMapper.Map(entity);
            return _animalProcessor.GetProcessedAnimal(animal);
        }

        public void Pet(Animal animal)
        {
            var entity = _context.Animals.Find(animal.Id);
            var animalModel = _animalMapper.Map(entity);
            var processedAnimal = _animalProcessor.GetProcessedAnimal(animalModel);
            entity.HappyLevel = processedAnimal.HappyLevel + processedAnimal.HappyLevelChange;
            entity.LastAction = _testableDateTime.UtcNow();
            _context.SaveChanges();
        }

        public void Feed(Animal animal)
        {
            var entity = _context.Animals.Find(animal.Id);
            var animalModel = _animalMapper.Map(entity);
            var processedAnimal = _animalProcessor.GetProcessedAnimal(animalModel);
            entity.HungryLevel = processedAnimal.HungryLevel - processedAnimal.HungryLevelChange;
            entity.LastAction = _testableDateTime.UtcNow();
            _context.SaveChanges();
        }
    }
}
