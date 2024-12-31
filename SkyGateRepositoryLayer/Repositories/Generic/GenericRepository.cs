using SkyGateDomainLayer.Entities.BaseEntity;
using SkyGateDomainLayer.Interfaces.Generic;
using SkyGateDomainLayer.Interfaces.Specifications;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Repositories.Generic
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext Context;

        public GenericRepository(AppDbContext Context)
        {
            this.Context = Context;
        }
        public List<TEntity> GetAll(ISpecification<TEntity, TKey> Spec)
        {
            return SpecificationEvaluator.GetQuery<TEntity, TKey>(Context.Set<TEntity>(), Spec).ToList();
        }

        public TEntity GetById(ISpecification<TEntity, TKey> Spec)
        {
            return SpecificationEvaluator.GetQuery<TEntity, TKey>(Context.Set<TEntity>(), Spec).FirstOrDefault();
        }
        public void Add(TEntity Item)
        {
            Context.Add(Item);
        }

        public void Update(TKey Id, TEntity Item)
        {
            var Entity = Context.Set<TEntity>().Find(Id);

            Entity = Item;

            Context.Update(Entity);
        }

        public void Delete(TKey Id)
        {
            var Item = Context.Set<TEntity>().Find(Id);

            if(Item is not null)
            {
                Context.Remove(Item);
            }
        }

    }
}
