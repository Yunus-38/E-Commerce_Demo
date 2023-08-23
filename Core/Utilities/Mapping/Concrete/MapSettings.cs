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

        public MapSettings MapProperty(string sourceProperty, string targetProperty)
        {
            SourceProperties.Add(SourceType.GetProperty(sourceProperty));
            TargetProperties.Add(TargetType.GetProperty(targetProperty));
            return this;
        }
    }
}
