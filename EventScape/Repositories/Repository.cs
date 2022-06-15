using EventScape.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static EventScape.Core.Repository.IRepository;

namespace EventScape.Repositories
{
    
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }


            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            TEntity entity = dbSet.Find(id);
            Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
