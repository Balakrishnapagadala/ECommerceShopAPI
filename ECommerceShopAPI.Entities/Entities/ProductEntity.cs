using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Entities;

public partial class ProductEntity
{
    public int ProductId { get; set; }

    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public int ProductType { get; set; }
}
