using System;

namespace Api.Models
{
    public class Animal
    {
        public Animal(int id, int happyLevel, int happyLevelChange, int hungryLevel, int hungryLevelChange, DateTime lastAction)
        {
            Id = id;
            HappyLevel = happyLevel;
            HappyLevelChange = happyLevelChange;
            HungryLevel = hungryLevel;
            HungryLevelChange = hungryLevelChange;
            LastAction = lastAction;
        }

        public int Id { get; }

        public int HappyLevel { get; }

        public int HappyLevelChange { get;}

        public int HungryLevel { get; }

        public int HungryLevelChange { get; }

        public DateTime LastAction { get; }
    }
}
