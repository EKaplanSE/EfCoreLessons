using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Querying
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set;}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<ProductPart> ProductParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=ECommerce; Trusted_Connection=true");
        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public ICollection<ProductPart> Parts { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}

public class ProductPart
{
    public int ProductId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
}
public class ProductDetail
{
    public int ProductId { get; set; }
    public decimal ProductPrice { get; set; }
}