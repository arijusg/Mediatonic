using System.Collections.Generic;
using Api.Models;

namespace Api.Interfaces
{
    public interface IAnimalMapper
    {
        Animal Map(Api.Entities.Animal entity);
        List<Animal> Map(List<Api.Entities.Animal> entities);
    }
}