using Core.Utilities.Mapping.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Mapping.Abstract
{
    public interface IMapperConfigurations
    {
        public List<MapSettings> Configuration { get; set; }
        public void Configure();
        public MapSettings GetConfiguration<TSource, TTarget>();
    }
}
