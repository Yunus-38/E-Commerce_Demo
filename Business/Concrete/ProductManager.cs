using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Caching;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.BusinessWork;
using Core.Utilities.Mapping.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
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
        IMapper _mapper;
        IBusinessRules<Product,IProductService> _businessRules;

        public ProductManager(IProductRepository productDal, IMapper mapper, IBusinessRules<Product, IProductService> businessRules)
        {
            _productRepository = productDal;
            _mapper = mapper;
            _businessRules = businessRules;
            _businessRules.Manager = this;
        }

        public IResult Add(Product entity)
        {
            _productRepository.Add(entity);
            return new SuccessResult();
        }

        [LogAspect]
        public IResult AddWithDto(AddProductDto addProductDto)
        {
            Product product = _mapper.Map<AddProductDto, Product>(addProductDto);
            var ruleCheck = _businessRules.Run(product, "AddWithDto");
            if (!ruleCheck.Success)
            {
                return ruleCheck;
            }

            _productRepository.Add(product);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            _productRepository.Delete(GetById(id).Data);
            return new SuccessResult();
        }

        //[CacheAspect]
        [SecuredOperation("admin,products.get")]
        public IDataResult<IEnumerable<Product>> GetAll()
        {
            return new SuccessDataResult<IEnumerable<Product>>(_productRepository.GetAll());
        }
        //[CacheAspect]
        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productRepository.Get(c => c.Id == id));
        }

        public IDataResult<List<ProductDetailsDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailsDto>>(_productRepository.GetProductDetails());
        }

        public IResult Update(Product entity)
        {
            _productRepository.Update(entity);
            return new SuccessResult();
        }
    }
}

