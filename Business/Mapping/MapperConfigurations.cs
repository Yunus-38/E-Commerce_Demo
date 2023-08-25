using Core.Entities.Concrete;
using Core.Utilities.Mapping.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping
{
    public class MapperConfigurations : BaseMapperConfigurations
    {
        public override void Configure()
        {
            AddConfiguration<AddProductDto, Product>().ReverseMap();

            base.Configure();
        }
    }
}
