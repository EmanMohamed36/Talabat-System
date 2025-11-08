using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Data
{
    public class StoreDbContext:DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options) 
        { 
        
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
