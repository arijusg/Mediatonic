using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Entities
{
    public class Animal
    {
        public int ID { get; set; }
        public virtual User User { get; set; }
    }
}
