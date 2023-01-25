using HRLeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistance
{
    public abstract class AuditableDbContext : DbContext
    {
        public AuditableDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        /// <summary>
        /// Save all changes made in this context to the database.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync(string username = "SYSTEM")
        {
            foreach (
                var entry in base.ChangeTracker
                    .Entries<BaseDomainEntity>()
                    .Where(e => e.State == EntityState.Modified || e.State == EntityState.Modified)
            )
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.LastModifiedBy = username;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = username;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
