using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : Entity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SqlMoney Price { get; set; }
        public DateTime AddedDate { get; set; }
        public int DiscountPercentage { get; set; } = 0;
        public bool Available { get; set; } = true;
    }
}
