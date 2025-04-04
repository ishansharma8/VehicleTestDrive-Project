﻿using Microsoft.EntityFrameworkCore;
using CustomersApi.Models;

namespace CustomersApi.Data
{
    public class ApiDbContext:DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-O7AKC939\SQLEXPRESS;Database=CustomerApiDb;Trusted_Connection=true;");
        }
    }
}
