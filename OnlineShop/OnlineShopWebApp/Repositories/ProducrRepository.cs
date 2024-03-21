﻿using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class ProducrRepository
    {
        private readonly List<Product> products = [];
        public ProducrRepository()
        {
            products.Add(new Product("An Epic Journey to the Center of the Milky Way Galaxy", 1000));
            products.Add(new Product("Journey to the constellation Centaurus", 500));
            products.Add(new Product("Journey to Andromeda", 3000));
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }
    }
}
