using applicationChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace applicationChallenge.Models
{
    public class DBInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            //Add categories
            context.AddRange(
                new Category {Name = "Kleding" },
                new Category {Name = "Voeding", }
            );

            //Add products
            context.AddRange(
                new Product { Name = "Adidas Joggingbroek", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv2", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas JoggingbroekV3", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv4", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv5", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv6", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv7", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas JoggingbroekV8", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv9", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv10", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv11", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv12", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas JoggingbroekV13", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv14", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Adidas Joggingbroekv15", Price = 74.99M, Description = "Prachtige broek voor te sporten", IsActive = true, CategoryID = 1, AmountInStock = 3 },
                new Product { Name = "Fitness shake ofzo", Price = 9.99M, Description = "Shake om te sporten en fitnessen", IsActive = true, CategoryID = 2, AmountInStock = 2 }
            );

            //Add users
            User userJulian = new User()
            {
                FirstName = "Julian",
                LastName = "Koppens",
                Email = "juliankoppens@hotmail.com",
                Password = "test1234",
                IsActive = true,
                IsAdmin = true,
                IsSuperAdmin = true,
            };
            context.Add(userJulian);

            /*//Orders
            Order order1 = new Order()
            {
                DatePlaced = DateTime.Now,
                UserID = 1,
            };

            context.Add(order1);

            //OrderLine
            OrderLine orderLine1 = new OrderLine()
            {
                OrderID = 1,
                ProductID = 1,
                Amount = 10,
            };

            context.Add(orderLine1);*/

            context.SaveChanges();
        }
    }
}
