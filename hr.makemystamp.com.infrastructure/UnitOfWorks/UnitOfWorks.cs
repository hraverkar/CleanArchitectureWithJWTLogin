using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.infrastructure.Context;
using hr.makemystamp.com.infrastructure.Repository;

namespace hr.makemystamp.com.infrastructure.UnitOfWorks
{
    public class UnitOfWorks(AppDbContext context) : IUnitOfWorks
    {
        private readonly AppDbContext _context = context;

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
