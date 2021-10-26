using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using NLog;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Controllers
{
    public class AppContextDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }


        public AppContextDB(DbContextOptions<AppContextDB> options) : base(options)
        {
            //Перед созданием миграций закоментить !!!
            //Database.EnsureDeleted(); // <-don't touch this code
            //Database.EnsureCreated(); // <-don't touch this code
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
            new List<Department>
            {
                new Department{ Id = 1, Name = "ИТ"},
                new Department{ Id = 2, Name = "Программисты"},
            });

            modelBuilder.Entity<User>().HasData(
                new List<User>
                {
                    new User { Id = 1, Name = "Иванов", Email="ivanov@mail.ru",PhoneNumber=123, DepartmentId=2},
                    new User { Id = 2, Name = "Петров", Email="petrov@mail.ru",PhoneNumber=456, DepartmentId=1},
                    new User { Id = 3, Name = "Сидоров", Email="sidrov@mail.ru",PhoneNumber=789, DepartmentId=2},
                });
        }
    }
}
