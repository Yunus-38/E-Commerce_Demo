using Core.Utilities.Mapping.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Mapping.Concrete
{
    public class BaseMapperConfigurations : IMapperConfigurations
    {
        public List<MapSettings> Configuration { get; set; } = new List<MapSettings>();
        public BaseMapperConfigurations()
        {
            Configure();
        }
        public virtual void Configure()
        {

        }
        public MapSettings AddConfiguration<TSouce, TTarget>()
        {
            MapSettings mapSettings = new(typeof(TSouce), typeof(TTarget));
            mapSettings.AddDefaultMapSettings<TSouce,TTarget>();
            mapSettings.Configurations = this;
            Configuration.Add(mapSettings);
            return mapSettings;
        }
        public MapSettings GetConfiguration<TSource, TTarget>()
        {
            var result = Configuration.FirstOrDefault(s => s.SourceType == typeof(TSource) && s.TargetType == typeof(TTarget));
            if (result == null)
            {
                throw new Exception($"No Mapping configuration found for mapping the type {typeof(TSource)} to {typeof(TTarget)}");
            }
            return result;
        }
    }
}
