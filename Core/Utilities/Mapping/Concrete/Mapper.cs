using Core.Utilities.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Mapping.Concrete
{
    public class Mapper : IMapper
    {
        IMapperConfiguration _configuration;

        public Mapper(IMapperConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TTarget Map<TSource, TTarget>(TSource source, TTarget target)
            where TSource : new()
            where TTarget : new()
        {
            TTarget result = new();
            MapSettings mapSettings = _configuration.GetConfiguration<TSource,TTarget>();
            PropertyInfo sourcePropertyInfo;
            PropertyInfo targetPropertyInfo;
            for (int i = 0; i < mapSettings.SourceProperties.Count; i++)
            {
                var sourceProperty = mapSettings.SourceProperties[i].GetValue(source);
                bool isNullOrEmpty = sourceProperty is null || sourceProperty.Equals("") || sourceProperty.Equals(0) || sourceProperty.Equals(0.0);
                sourcePropertyInfo = mapSettings.SourceProperties[i];
                targetPropertyInfo = mapSettings.TargetProperties[i];
                if (isNullOrEmpty)
                {
                    targetPropertyInfo.SetValue(result, targetPropertyInfo.GetValue(target));
                }
                else
                {
                    targetPropertyInfo.SetValue(result, sourcePropertyInfo.GetValue(source));
                }
            }
            return result;
        }

        public TTarget Map<TSource, TTarget>(TSource source)
            where TSource : new()
            where TTarget : new()
        {
            TTarget result = new();
            MapSettings mapSettings = _configuration.GetConfiguration<TSource, TTarget>();
            PropertyInfo sourcePropertyInfo;
            PropertyInfo targetPropertyInfo;
            for (int i = 0; i < mapSettings.SourceProperties.Count; i++)
            {
                var sourceProperty = mapSettings.SourceProperties[i].GetValue(source);
                
                sourcePropertyInfo = mapSettings.SourceProperties[i];
                targetPropertyInfo = mapSettings.TargetProperties[i];
                Type targetPropertyType = targetPropertyInfo.PropertyType;
                var sourcePropertyValue = sourcePropertyInfo.GetValue(source);
                targetPropertyInfo.SetValue(result, sourcePropertyValue);
                
            }
            return result;
        }

        public TSource SelfMap<TSource>(TSource source, TSource target) where TSource : new()
        {
            TSource result = new TSource();

            PropertyInfo[] properties = typeof(TSource).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var sourceProperty = property.GetValue(source, null);
                bool isNullOrEmpty = sourceProperty is null || sourceProperty.Equals("") || sourceProperty.Equals(0) || sourceProperty.Equals(0.0);

                if (isNullOrEmpty)
                {
                    property.SetValue(result, property.GetValue(target));
                }
                else
                {
                    property.SetValue(result, sourceProperty);
                }
            }
            return result;
        }
    }
}
