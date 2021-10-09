using mango.product.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.data.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public virtual DbSet<Product>? Products { get; set; }

        public bool IsOpen()
        {
            return this.Database.GetDbConnection().State == System.Data.ConnectionState.Open;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedDatabase.Execute(modelBuilder);
            
        }
    }
}
