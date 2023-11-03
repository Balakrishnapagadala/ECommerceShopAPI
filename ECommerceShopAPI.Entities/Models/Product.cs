using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public int ProductType { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
