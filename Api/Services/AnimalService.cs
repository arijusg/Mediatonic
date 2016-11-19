using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IAnimalService
    {
        List<Animal> GetUserAnimals(User user);
    }
    public class AnimalService : IAnimalService
    {
        private readonly Func<DateTime> _now;
        public AnimalService(Func<DateTime> now)
        {
            _now = now;
        }

        public AnimalService()
        {
            _now = () => DateTime.UtcNow;
        }
        public List<Animal> GetUserAnimals(User user)
        {
            return new List<Animal> {new Animal(1, 50, 1, 50, 1)};
        }
    }
}
