using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Abstract
{
    public interface IEntityService<TEntity> where TEntity : Entity, new()
    {
        IDataResult<IEnumerable<TEntity>> GetAll();
        IDataResult<TEntity> GetById(int id);
        IResult Add(TEntity entity);
        IResult Update(TEntity entity);
        IResult Delete(int id);
        
    }
}
