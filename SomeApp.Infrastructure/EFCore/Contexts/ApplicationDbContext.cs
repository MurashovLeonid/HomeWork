using Microsoft.EntityFrameworkCore;
using SomeApp.Infrastructure.Implementation.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeApp.Infrastructure.Implementation.EFCore.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();// если необходимо оставить тестовые данные после использования программы, этот кусок лучше закомментировать          
        }

        public DbSet<EntryRow> EntryRows { get; set; }

        public DbSet<TaskRow> TaskRows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
