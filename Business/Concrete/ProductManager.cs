using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductRepository _productRepository;

        public ProductManager(IProductRepository productDal)
        {
            _productRepository = productDal;
        }

        public IResult Add(Product entity)
        {
            _productRepository.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            _productRepository.Delete(GetById(id).Data);
            return new SuccessResult();
        }

        public IDataResult<IEnumerable<Product>> GetAll()
        {
            return new SuccessDataResult<IEnumerable<Product>>(_productRepository.GetAll());
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productRepository.Get(c => c.Id == id));
        }

        public IResult Update(Product entity)
        {
            _productRepository.Update(entity);
            return new SuccessResult();
        }
    }
}

