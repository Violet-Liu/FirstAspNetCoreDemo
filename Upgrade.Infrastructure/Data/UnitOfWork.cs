using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Upgrade.Core.Interfaces;

namespace Upgrade.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UDBContext _uDBContext;

        public UnitOfWork(UDBContext uDBContext)
        {
            _uDBContext = uDBContext;
        }

        public void Dispose() => _uDBContext?.Dispose();

        public bool Save()=> _uDBContext.SaveChanges() >= 0;

        public bool Save(bool acceptAllChangesOnSuccess) => _uDBContext.SaveChanges(acceptAllChangesOnSuccess) >= 0;

        public async Task<bool> SaveAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
            => await _uDBContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken) > 0;

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
            => await _uDBContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
