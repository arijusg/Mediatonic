using System;
using Api.Services;

namespace Api.Models
{
    public class Animal
    {
        public Animal(int id, int happyLevel, int happyDecrement, int hungryLevel, int hungerIncrement)
        {
            Id = id;
            HappyLevel = happyLevel;
            HappyDececrement = happyDecrement;
            HungryLevel = hungryLevel;
            HungryIncrement = hungerIncrement;
        }

        public int Id { get; }
        public int HappyLevel { get; set; }
        public int HappyDececrement { get; }

        public int HungryLevel { get; set; }
        public int HungryIncrement { get; }

        //public DateTime LastProcess { get; set; } = new DateTime(2005, 01, 01, 13, 11, 0, DateTimeKind.Utc);
        public DateTime LastProcess { get; set; }
    }
}
