using Microsoft.EntityFrameworkCore;
using PS223020Server.DataAccess.Core.Interfaces.DbContext;
using PS223020Server.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS223020Server.DataAccess.DbContext
{
    public class RubicContext : Microsoft.EntityFrameworkCore.DbContext, IRubicContext
    {
        public RubicContext(DbContextOptions<RubicContext> options)
            : base(options)
        {
        }

        public DbSet<UserRto> Users { get; set; }
    }
}
