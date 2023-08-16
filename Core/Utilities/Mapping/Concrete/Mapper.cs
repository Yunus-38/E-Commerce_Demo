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
