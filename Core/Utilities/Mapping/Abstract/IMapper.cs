using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Mapping.Abstract
{
    public interface IMapper
    {
        /// <summary>
        /// Maps the objects of same type,ignores null values.
        /// </summary>
        /// <param name="source">Source object that will take priority and have it's null values ignored.</param>
        /// <param name="target">Target object that will have its values overriden in result(instance given as parameter will not be modified).</param>
        TSource SelfMap<TSource>(TSource source, TSource target) where TSource : new();
        TTarget Map<TSource,TTarget>(TSource source, TTarget target) 
            where TSource : new()
            where TTarget : new();
        TTarget Map<TSource, TTarget>(TSource source)
            where TSource : new()
            where TTarget : new();
    }
}
