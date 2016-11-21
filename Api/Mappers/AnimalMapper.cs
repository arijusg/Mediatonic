using System.Collections.Generic;
using System.Linq;
using Api.Interfaces;
using Api.Models;

namespace Api.Mappers
{
    public class AnimalMapper : IAnimalMapper
    {
        public Animal Map(DAL.Entities.Animal entity)
        {
            return new Animal(entity.ID, entity.HappyLevel, entity.HappyLevelChange, 
                entity.HungryLevel, entity.HungryLevelChange, entity.LastAction);
        }

        public List<Animal> Map(List<DAL.Entities.Animal> entities)
        {
            return entities.Select(Map).ToList();
        }
    }
}
