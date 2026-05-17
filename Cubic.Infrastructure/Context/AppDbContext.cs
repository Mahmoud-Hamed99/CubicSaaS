using Cubic.Core.Entities;
using Cubic.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Infrastructure.Context
{
    public class AppDbContext: DbContext
    {
        public readonly ITenantContext _tenantContext;
        public IConfiguration _config;
        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantContext tenantContext, IConfiguration config)
        : base(options)
        {
            _tenantContext = tenantContext;
            _config = config;
        }

        public virtual DbSet<Tenant> Tenant { get; set; }    
        public virtual DbSet<User> Users { get; set; }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>()
             .HasOne(e => e.Tenant)
             .WithMany(s => s.Users)
             .HasForeignKey(e => e.TenantId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.TenantId)
                .IsUnique(false);

            // Apply global filter
            modelBuilder.Entity<User>()
              .HasQueryFilter(u =>
                  (!_tenantContext.TenantId.HasValue || u.TenantId == _tenantContext.TenantId)
                  && u.IsActive
              );

            base.OnModelCreating(modelBuilder);
            
        }


    }
}
