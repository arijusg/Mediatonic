using System.Collections.Generic;
using System.Linq;
using Api.Models;

namespace Api
{
    public interface IAnimalMapper
    {
        Animal Map(Api.Entities.Animal entity);
        List<Animal> Map(List<Api.Entities.Animal> entities);
    }

    public class AnimalMapper : IAnimalMapper
    {
        public Animal Map(Api.Entities.Animal entity)
        {
            return new Animal(entity.ID, entity.HappyLevel, entity.HappyLevelChange, 
                entity.HungryLevel, entity.HungryLevelChange, entity.LastAction);
        }

        public List<Animal> Map(List<Entities.Animal> entities)
        {
            return entities.Select(Map).ToList();
        }
    }
}
