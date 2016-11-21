using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Services;

namespace Api
{
    public interface IAnimalProcessor
    {
        Animal GetProcessedAnimal(Animal animal);
        List<Animal> GetProcessedAnimals(List<Animal> animals);
    }

    public class AnimalProcessor : IAnimalProcessor
    {
        private readonly ITestableDateTime _testableDateTime;

        public AnimalProcessor(ITestableDateTime testableDateTime)
        {
            _testableDateTime = testableDateTime;
        }
        public Animal GetProcessedAnimal(Animal animal)
        {

            var minuteDIff = _testableDateTime.UtcNow().Subtract(animal.LastAction).Minutes;
            int newHungryLevel = animal.HungryLevel + animal.HungryLevelChange * minuteDIff;
            int newHappyLevel = animal.HappyLevel - animal.HappyLevelChange * minuteDIff;

            return new Animal(animal.Id, newHappyLevel, animal.HappyLevelChange, newHungryLevel, animal.HungryLevelChange, animal.LastAction);
        }

        public List<Animal> GetProcessedAnimals(List<Animal> animals)
        {
            return animals.Select(GetProcessedAnimal).ToList();
        }
    }
}
