using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IAnimalFactory
    {
        Animal GetAnimal();
    }
    public class AnimalFacotry : IAnimalFactory
    {
        public Animal GetAnimal()
        {
            return new Animal(1, 50, 1, 50, 1);
        }
    }
}
