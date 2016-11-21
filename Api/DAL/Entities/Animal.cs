using System;

namespace Api.DAL.Entities
{
    public class Animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HappyLevel { get; set; }
        public int HappyLevelChange{ get; set; }
        public int HungryLevel { get; set; }
        public int HungryLevelChange { get; set; }
        public DateTime LastAction { get; set; }
        public virtual User User { get; set; }
    }
}
