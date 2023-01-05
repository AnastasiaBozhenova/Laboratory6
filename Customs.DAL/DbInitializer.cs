using System;
using System.Linq;
using Customs.DAL;
using Customs.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Lab3_.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CustomsContext db)
        {
            if (db.Employees.Any()) return;

            const string seedUserName = "admin";
            //const string seedUserPassword = "5asfnjaAA909";
            const string seedUserHashedPassword =
                "AQAAAAEAACcQAAAAENdHIYBFh0VNwivT/L8mvPkxuLO7vgat/5RCczb0AidSM/3fXfYXmPh2FwipgdLS1Q==";

            var seedUser = new IdentityUser
            {
                UserName = seedUserName,
                NormalizedUserName = seedUserName.ToUpper(),
                PasswordHash = seedUserHashedPassword
            };

            db.Users.Add(seedUser);
            db.SaveChanges();

            var role = new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var role2 = new IdentityRole
            {
                Name = "user",
                NormalizedName = "USER"
            };

            db.Roles.Add(role);
            db.Roles.Add(role2);
            db.SaveChanges();

            var userRole = new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = seedUser.Id
            };

            db.UserRoles.Add(userRole);
            db.SaveChanges();

            const int employeesNumber = 35;
            const int storageNumber = 35;
            const int productNumber = 300;

            var employees = Enumerable.Range(1, employeesNumber)
                .Select(employeeId => new Employee
                {
                    FirstName = "TestFirstName" + employeeId,
                    LastName = "TestLastName" + employeeId,
                    MiddleName = "TestMiddleName" + employeeId,
                    IdNumber = employeeId.ToString(),
                    Role = "TestRole" + employeeId,
                })
                .ToList();
            db.Employees.AddRange(employees);
            db.SaveChanges();

            var storages = Enumerable.Range(1, storageNumber)
                .Select(storageId => new Storage
                {
                    Name = "TestStorage" + storageId,
                })
                .ToList();
            db.Storages.AddRange(storages);
            db.SaveChanges();

            var products = Enumerable.Range(1, productNumber)
                .Select(productId => new Product
                {
                    Name = "TestProduct" + productId,
                    UnitMeasurement = new Random().Next(1, 100),
                    StorageId = new Random().Next(1, storageNumber)
                })
                .ToList();

            db.Products.AddRange(products);
            db.SaveChanges();
        }
    }
}