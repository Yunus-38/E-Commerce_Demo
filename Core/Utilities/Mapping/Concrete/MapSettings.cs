using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Mapping.Concrete
{
    public class MapSettings
    {
        public MapSettings(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
            SourceProperties = new();
            TargetProperties = new();
        }

        public Type SourceType { get; private set; }
        public Type TargetType { get; private set; }
        public List<PropertyInfo> SourceProperties { get; private set; }
        public List<PropertyInfo> TargetProperties { get; private set; }
        public BaseMapperConfigurations Configurations { get; set; }

        public MapSettings MapProperty(string sourceProperty, string targetProperty)
        {
            SourceProperties.Add(SourceType.GetProperty(sourceProperty));
            TargetProperties.Add(TargetType.GetProperty(targetProperty));
            return this;
        }

        public MapSettings AddDefaultMapSettings<TSource, TTarget>()
        {
            PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
            PropertyInfo[] targetProperties = typeof(TTarget).GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var targetProperty in targetProperties)
                {
                    if (sourceProperty.Name.ToLower() == targetProperty.Name.ToLower())
                    {
                        MapProperty(sourceProperty.Name, targetProperty.Name);
                    }
                }
            }
            return this;
        }
        public MapSettings ReverseMap()
        {
            MapSettings reverseMapSettings = new(TargetType, SourceType);
            reverseMapSettings.SourceProperties = new(TargetProperties);
            reverseMapSettings.TargetProperties = new(SourceProperties);
            Configurations.Configuration.Add(reverseMapSettings);
            return reverseMapSettings;
        }
    }
}
