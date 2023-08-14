using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Entity
    {
        public int Id { get; set; }
        public Entity()
        {
            Id = Id + 1;
        }
    }
}
