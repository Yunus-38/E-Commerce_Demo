using Business.Abstract;
using Core.Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IResult Add(Category entity)
        {
            _categoryRepository.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            _categoryRepository.Delete(GetById(id).Data);
            return new SuccessResult();
        }

        public IDataResult<IEnumerable<Category>> GetAll()
        {
            return new SuccessDataResult<IEnumerable<Category>>(_categoryRepository.GetAll());
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryRepository.Get(c => c.Id == id));
        }

        public IResult Update(Category entity)
        {
            _categoryRepository.Update(entity);
            return new SuccessResult();
        }
    }
}
