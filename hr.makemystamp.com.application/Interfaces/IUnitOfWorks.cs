using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.makemystamp.com.application.Interfaces
{
    public interface IUnitOfWorks : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task SaveChangesAsync(CancellationToken cancellation);
    }
}
