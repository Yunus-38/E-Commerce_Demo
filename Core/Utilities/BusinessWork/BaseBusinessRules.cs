using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.BusinessWork
{
    public class BaseBusinessRules<TEntity, TManager> : IBusinessRules<TEntity, TManager>
         where TEntity : class, new()
    {

        public TManager Manager { get; set; }

        public IResult Run(TEntity entity, string managerMethod)
        {
            var businessRules = this.GetType().GetMethods();
            var applicableBusinessRules = businessRules.Where(r =>
            {
                BusinessRuleAttribute[] b = r.GetCustomAttributes(typeof(BusinessRuleAttribute), false) as BusinessRuleAttribute[];
                BusinessRuleAttribute a;
                if (b != null && b.Length > 0)
                {
                    a = b[0];

                    return a.Methods.Contains(managerMethod);
                }
                return false;
            });
            IResult result;
            foreach (var rule in applicableBusinessRules)
            {
                result = (IResult)rule.Invoke(this, new TEntity[1] { entity });
                if (!result.Success)
                {
                    return result;
                }
            }
            return new SuccessResult();
        }
    }
}
