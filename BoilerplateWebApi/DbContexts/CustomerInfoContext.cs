using BoilerplateWebApi.Entities;
using BoilerplateWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BoilerplateWebApi.DbContexts
{
    public class CustomerInfoContext : DbContext
    {
        public CustomerInfoContext(DbContextOptions<CustomerInfoContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<CustomerOperation> CustomerOperations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Customer>().HasData(
            //        new Customer("Customer1")
            //        {
            //            Id = 1,
            //            Address = "Address 1",
            //            Email = "customer1@email.com",
            //            Phone = "012345649879",

            //        },
            //        new Customer("Customer2")
            //        {
            //            Id = 2,
            //            Address = "Address 2",
            //            Email = "customer2@email.com",
            //            Phone = "012345649879",
            //        },
            //        new Customer("Customer3")
            //        {
            //            Id = 3,
            //            Address = "Address 3",
            //            Email = "customer1@email.com",
            //            Phone = "012345649879",
            //        }
            //    );

            //modelBuilder.Entity<CustomerOperation>().HasData(
            //        new CustomerOperation("operation 1")
            //        {
            //            Id = 1,
            //            Price = 10,
            //            CustomerId = 1,
            //        },
            //        //new CustomerOperation("operation 2")
            //        //{
            //        //    Id = 2,
            //        //    Price = 20,
            //        //    CustomerId = 1,
            //        //},
            //        new CustomerOperation("operation 3")
            //        {
            //            Id = 3,
            //            Price = 30,
            //            CustomerId = 2,
            //        },
            //        //new CustomerOperation("operation 4")
            //        //{
            //        //    Id = 4,
            //        //    Price = 40,
            //        //    CustomerId = 2,
            //        //},
            //        new CustomerOperation("operation 5")
            //        {
            //            Id = 5,
            //            Price = 50,
            //            CustomerId = 3,
            //        },
            //        //new CustomerOperation("operation 6")
            //        //{
            //        //    Id = 6,
            //        //    Price = 60,
            //        //    CustomerId = 3,
            //        //}
            //    );
            base.OnModelCreating(modelBuilder);
        }
        //another way to implement it
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
