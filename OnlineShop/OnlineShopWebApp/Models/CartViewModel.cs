﻿namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemViewModel> Items { get; set;}
        public decimal Cost => Items?.Sum(x => x.Cost) ?? 0;
        public decimal Amount => Items?.Sum(x => x.Amount) ?? 0;
    }
}
