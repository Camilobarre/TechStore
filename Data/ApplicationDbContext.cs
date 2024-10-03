using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Product)
                .WithMany()
                .HasForeignKey(po => po.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(po => po.OrderId);
        }

        public void SeedData()
        {
            // Check if the database is already populated
            if (Products.Any() || Orders.Any() || Customers.Any() || Categories.Any() || Users.Any())
                return;

            var faker = new Faker();

            // Seed Categories
            var categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                categories.Add(new Category
                {
                    Name = faker.Commerce.Department(),
                    Description = faker.Commerce.ProductDescription()
                });
            }
            Categories.AddRange(categories);
            SaveChanges();

            // Seed Products
            var products = new List<Product>();
            for (int i = 0; i < 5; i++)
            {
                products.Add(new Product
                {
                    Name = faker.Commerce.ProductName(),
                    Description = faker.Commerce.ProductDescription(),
                    Price = Convert.ToDecimal(faker.Commerce.Price()),
                    QuantityInStock = faker.Random.Int(1, 100),
                    CategoryId = faker.PickRandom(categories).Id // Link to a random category
                });
            }
            Products.AddRange(products);
            SaveChanges();

            // Seed Customers
            var customers = new List<Customer>();
            for (int i = 0; i < 5; i++)
            {
                customers.Add(new Customer
                {
                    Name = faker.Name.FullName(),
                    Address = faker.Address.FullAddress(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                    Email = faker.Internet.Email()
                });
            }
            Customers.AddRange(customers);
            SaveChanges();

            // Seed Users
            var users = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                users.Add(new User
                {
                    Username = faker.Internet.UserName(),
                    Password = faker.Internet.Password(), // Store this securely in production
                    Role = faker.PickRandom(new[] { "Administrator", "Employee" })
                });
            }
            Users.AddRange(users);
            SaveChanges();

            // Seed Orders
            var orders = new List<Order>();
            for (int i = 0; i < 5; i++)
            {
                var order = new Order
                {
                    CustomerId = faker.PickRandom(customers).Id, // Link to a random customer
                    Status = faker.PickRandom(new[] { "Pending", "Shipped", "Delivered" }),
                    CreatedDate = faker.Date.Recent()
                };

                // Add some products to the order
                for (int j = 0; j < faker.Random.Int(1, 3); j++)
                {
                    order.Products.Add(new ProductOrder
                    {
                        ProductId = faker.PickRandom(products).Id, // Link to a random product
                        Quantity = faker.Random.Int(1, 5)
                    });
                }
                orders.Add(order);
            }
            Orders.AddRange(orders);
            SaveChanges();

        }
    }
}