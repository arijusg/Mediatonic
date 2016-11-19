using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Animal> Animals { get; set; }
    }
}
