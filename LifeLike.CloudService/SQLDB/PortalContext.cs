using LifeLike.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLike.CloudService.SQLDB
{
    public class PortalContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
