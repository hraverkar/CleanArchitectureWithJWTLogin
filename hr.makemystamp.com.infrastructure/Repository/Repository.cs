using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hr.makemystamp.com.infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }
        public void Delete(T entity)
        {
            _appDbContext.Remove(entity);
        }

        public IQueryable<T> GetAll(bool noTracking = true)
        {
            var query = noTracking ? _dbSet.AsNoTracking() : _dbSet;
            return query;
        }

        public async Task<T> GetByIdAsync<TId>(TId id) where TId : notnull
        {
            return await _dbSet.FindAsync(id);
        }
        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void InsertAll(List<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Remove(IEnumerable<T> entitiesToRemove)
        {
            _dbSet.RemoveRange(entitiesToRemove);
        }

        public void Update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }
    }
}
