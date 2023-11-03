using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Entities;

public partial class CustomerEntity
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public bool? IsLoyaltyMembership { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CartEntity> Carts { get; set; } = new List<CartEntity>();

    public virtual ICollection<CustomerShippingDetailEntity> CustomerShippingDetails { get; set; } = new List<CustomerShippingDetailEntity>();

    public virtual ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
