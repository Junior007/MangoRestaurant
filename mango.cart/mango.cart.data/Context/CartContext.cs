//using mango.cart.data.Models;
using Microsoft.EntityFrameworkCore;


namespace mango.cart.data.Context
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {

        }

        //public virtual DbSet<Product>? Products { get; set; }

        public bool IsOpen()
        {
            return this.Database.GetDbConnection().State == System.Data.ConnectionState.Open;
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedDatabase.Execute(modelBuilder);
            
        }*/
    }
}
