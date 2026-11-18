using B2BWholesale.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.Core.Data
{
    public class B2BContext : DbContext
    {
        public B2BContext(DbContextOptions<B2BContext> options) : base(options) { }

        public DbSet<Item> Items => Set<Item>();
        public DbSet<Agent> Agents => Set<Agent>();
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    }
}