using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.BusinessWork
{
    public interface IBusinessRules<TEntity, TManager> where TEntity : class, new()
    {
        TManager Manager { get; set; }
        public IResult Run(TEntity entity, string managerMethod);
    }
}
