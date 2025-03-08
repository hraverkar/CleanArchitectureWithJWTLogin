namespace hr.makemystamp.com.application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(bool noTracking = true);
        Task<T> GetByIdAsync<TId>(TId id) where TId : notnull;
        void Insert(T entity);
        void InsertAll(List<T> entities);
        void Delete(T entity);
        void Remove(IEnumerable<T> entitiesToRemove);
        void Update(T entity);

    }
}
