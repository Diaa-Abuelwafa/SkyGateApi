using Microsoft.EntityFrameworkCore;
using SkyGateDomainLayer.Entities.BaseEntity;
using SkyGateDomainLayer.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Repositories.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, Tkey>(IQueryable<TEntity> Base, ISpecification<TEntity, Tkey> Spec) where TEntity : BaseEntity<Tkey>
        {
            var Query = Base;

            if(Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }

            foreach(var I in Spec.Includes)
            {
                Query = Query.Include(I);
            }

            if(Spec.OrderByAsec is not null)
            {
                Query = Query.OrderBy(Spec.OrderByAsec);
            }
            else if(Spec.OrderByDsec is not null)
            {
                Query = Query.OrderBy(Spec.OrderByDsec);
            }

            if(Spec.Skip > 0)
            {
                Query = Query.Skip(Spec.Skip);
            }

            if(Spec.Take > 0)
            {
                Query = Query.Take(Spec.Take);
            }

            return Query;
        }
    }
}
