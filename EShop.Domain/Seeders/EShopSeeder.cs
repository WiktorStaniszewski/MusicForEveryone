﻿using EShop.Domain.Repositories;
using EShopDomain.Models;

namespace EShop.Domain.Seeders;

public class EShopSeeder(DataContext context) : IEShopSeeder
{
    public async Task Seed()
    {
        if (!context.Categories.Any())
        {
            var categs = new List<Category> {
                new Category {Name = "Instrument"},
                new Category {Name = "Płyta"},
                new Category {Name = "akcesorium"}
            };
            context.Categories.AddRange(categs);
            await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
            var students = new List<Product>
            {
                new Product { Name = "Cobi", Ean = "1234"},
                new Product { Name = "Duplo", Ean = "431" },
                new Product { Name = "Lego", Ean = "12212" },
                new Product { Name = "Minecraft", Ean = "98982"}
            };

            context.Products.AddRange(students);
            await context.SaveChangesAsync();
        }
    }
}
