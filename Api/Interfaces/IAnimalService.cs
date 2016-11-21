using System.Collections.Generic;
using Api.Models;

namespace Api.Interfaces
{
    public interface IAnimalService
    {
        Animal GetAnimal(int id);
        List<Animal> GetUserAnimals(User user);
        void Feed(Animal animal);
        void Pet(Animal animal);
    }
}