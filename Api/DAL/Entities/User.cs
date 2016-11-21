using System.Collections.Generic;

namespace Api.DAL.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Animal> Animals { get; set; }
    }
}
