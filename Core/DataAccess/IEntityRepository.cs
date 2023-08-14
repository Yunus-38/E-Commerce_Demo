using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(Expression<Func<TEntity,bool>> expression);
        List<TEntity> GetAll(Expression<Func<TEntity,bool>> expression = null);
        void Delete(TEntity entity);
    }
}
