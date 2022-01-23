using Microsoft.EntityFrameworkCore;
using PS223020Server.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PS223020Server.DataAccess.Core.Interfaces.DbContext
{
    public interface IRubicContext : IDisposable, IAsyncDisposable
    {
        DbSet<UserRto> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
