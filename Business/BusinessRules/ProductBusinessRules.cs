using Business.Abstract;
using Core.Utilities.BusinessWork;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class ProductBusinessRules : BaseBusinessRules<Product, IProductService>
    {

        [BusinessRule("AddWithDto", "Add", "Update")]
        public IResult ProductNameExists(Product product)
        {
            var products = Manager.GetAll().Data.Where(p => p.Name == product.Name);
            if (products.Any())
            {
                return new ErrorResult("Product name already exists");
            }
            else
            {
                return new SuccessResult();
            }
        }
    }
}
