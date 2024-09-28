using ItemApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ItemApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
