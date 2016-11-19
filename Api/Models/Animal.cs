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
        public int HappyLevel { get; }
        public int HappyDececrement { get; }

        public int HungryLevel { get; }
        public int HungryIncrement { get; }
    }
}
