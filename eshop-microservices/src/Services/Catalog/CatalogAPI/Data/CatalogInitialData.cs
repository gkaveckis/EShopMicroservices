using Marten.Schema;
using CatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
            {
                return;
            }

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private IEnumerable<Product> GetPreconfiguredProducts()
        {
            var rnd = new Random();
            var sampleCategories = new[] { "Books", "Electronics", "Clothing", "Home", "Toys", "Sports" };

            var products = Enumerable.Range(1, 10).Select(i => new Product
            {
                Id = Guid.NewGuid(),
                Name = $"Product {i}",
                Categories = Enumerable.Range(0, rnd.Next(1, 3))
                    .Select(_ => sampleCategories[rnd.Next(sampleCategories.Length)])
                    .Distinct()
                    .ToList(),
                Description = $"This is a randomly generated description for product {i}.",
                ImageFile = $"product{i}.png",
                Price = Math.Round((decimal)(rnd.NextDouble() * 1000.0 + 1.0), 2)
            }).ToList();

            return products;
        }
    }
}
