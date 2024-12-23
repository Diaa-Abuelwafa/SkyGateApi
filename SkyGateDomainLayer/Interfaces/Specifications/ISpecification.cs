﻿using SkyGateDomainLayer.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.Specifications
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }
        public Expression<Func<TEntity, object>> OrderByAsec { get; set; }
        public Expression<Func<TEntity, object>> OrderByDsec { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
