using System.Linq.Expressions;

namespace EventScape.Core.Repository
{
    public interface IRepository
    {
        public interface IRepository<TEntity> where TEntity : class
        {
            TEntity Get(int id);

            //IEnumerable<TEntity> GetAll(
            //     Expression<Func<TEntity, bool>>? filter=null,
            //    string? includeProperties = null
            //    );

            //TEntity GetFirstOrDefault(
            //    Expression<Func<TEntity, bool>> filter,
            //    string? includeProperties = null
            //    );
            public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
            public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>>? orderBy = null, string? includeProperties = null);
            
                void Add(TEntity entity);
            void Remove(int id);
            void Remove(TEntity entity);
            void RemoveRange(IEnumerable<TEntity> entity);


        }
    }
}
