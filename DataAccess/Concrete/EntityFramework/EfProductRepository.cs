using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductRepository : EfEntityRepositoryBase<Product, ProjectContext>, IProductRepository
    {
        public List<ProductDetailsDto> GetProductDetails(Expression<Func<ProductDetailsDto, bool>> expression = null)
        {
            using (ProjectContext context = new())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             select new ProductDetailsDto()
                             {
                                 ProductId = p.Id,
                                 ProductName = p.Name,
                                 ProductDescription = p.Description,
                                 CategoryName = c.Name,
                                 ProductPrice = "$" + (p.Price * (decimal)(1 - 0.01 * p.DiscountPercentage)).ToString()
                             };

                return expression == null
                    ? result.ToList<ProductDetailsDto>()
                    : result.Where(expression).ToList<ProductDetailsDto>();

            }
        }
    }
}
