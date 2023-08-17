using DotNetDrinks.Controllers;
using DotNetDrinks.Data;
using DotNetDrinks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DotNetDrinks.Tests
{
    public class ProductsControllerTests
    {
        private ApplicationDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void EditGet_ReturnsEditView()
        {
            // Arrange
            using var dbContext = GetDbContext(nameof(EditGet_ReturnsEditView));
            dbContext.Products.Add(new Product { Id = 1, Name = "Test Product" });
            dbContext.SaveChanges();

            var controller = new ProductsController(dbContext);

            // Act
            var result = controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Edit", viewResult.ViewName);
        }

        [Fact]
        public void DeleteConfirmed_RemovesProductFromDatabase()
        {
            // Arrange
            using var dbContext = GetDbContext(nameof(DeleteConfirmed_RemovesProductFromDatabase));
            dbContext.Products.Add(new Product { Id = 1, Name = "Test Product" });
            dbContext.SaveChanges();

            var controller = new ProductsController(dbContext);

            // Act
            controller.DeleteConfirmed(1);

            // Assert
            var product = dbContext.Products.Find(1);
            Assert.Null(product);
        }
    }
}
