using System.Collections.Generic;
using Api.Models;

namespace Api.Services
{
    public interface IAnimalService
    {
        List<Animal> GetUserAnimals(User user);
    }
    public class AnimalService : IAnimalService
    {
        private readonly ITestableDateTime _testableDateTime;
        private readonly IAnimalFactory _animalFactory;

        public AnimalService(ITestableDateTime testableDateTime, IAnimalFactory animalFactory)
        {
            _testableDateTime = testableDateTime;
            _animalFactory = animalFactory;
        }

        public List<Animal> GetUserAnimals(User user)
        {
            var animal = _animalFactory.GetAnimal();
           // animal.LastProcess = _testableDateTime.UtcNow();
            //process lvl
            var minuteDIff = _testableDateTime.UtcNow().Subtract(animal.LastProcess).Minutes;
            animal.HungryLevel += animal.HungryIncrement*minuteDIff;
            animal.HappyLevel -= animal.HappyDececrement*minuteDIff;

            return new List<Animal> {animal};
        }
    }
}
