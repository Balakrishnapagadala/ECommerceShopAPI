using System;
using System.Collections.Generic;

namespace ECommerceShopAPI.Entities.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public bool? IsLoyaltyMembership { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<CustomerShippingDetail> CustomerShippingDetails { get; set; } = new List<CustomerShippingDetail>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
