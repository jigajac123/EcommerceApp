
using Microsoft.EntityFrameworkCore;
using RestFullApi.Models;

namespace RestFullApi.DataBase
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer>  Customers { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }  

    }
}
