﻿using SkyGateDomainLayer.Entities.BaseEntity;
using SkyGateDomainLayer.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.Generic
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public IQueryable<TEntity> GetAll(ISpecification<TEntity, TKey> Spec);
        public TEntity GetById(ISpecification<TEntity, TKey> Spec);
        public void Add(TEntity Item);
        public void Update(TEntity Item);
        public void Delete(TKey Id);
    }
}
