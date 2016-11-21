using System.Collections.Generic;
using Api.Models;

namespace Api.Interfaces
{
    public interface IAnimalMapper
    {
        Animal Map(DAL.Entities.Animal entity);
        List<Animal> Map(List<DAL.Entities.Animal> entities);
    }
}